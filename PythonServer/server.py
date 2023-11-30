import time
import zmq
from keras.models import load_model
import numpy as np
import librosa
import soundfile
import glob
import os


model = load_model('Emotion_Voice_Detection_Model_with_selected_emotions.h5')

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")


while True:
    try:
        filename = socket.recv()
    except:
        pass
    print("Filename request: %s" % filename)
    # time.sleep(5)

    try:
        with soundfile.SoundFile(filename) as sound_file:
            X = sound_file.read(dtype="float32")
            stft = np.abs(librosa.stft(X))
            result = np.array([])
            mfccs = np.mean(librosa.feature.mfcc(y=X, sr=48000, n_mfcc=40).T, axis=0)
            result = np.hstack((result, mfccs))
            chroma = np.mean(librosa.feature.chroma_stft(S=stft, sr=48000).T,axis=0)
            result = np.hstack((result, chroma))
            mel = np.mean(librosa.feature.melspectrogram(X, sr=48000).T,axis=0)
            result = np.hstack((result, mel))

        print(result)

        pred = model.predict(np.array([result]))[0]
        pred = np.round(pred,decimals = 4)
        if('fight' in str(filename)):
            expected_emotion = "angry"
        elif('save' in str(filename)):
            expected_emotion = "happy"
        else:
            expected_emotion = str(filename).split('_')[2]

        all_emotions = ["notused", "neutral", "notused", "happy", "sad", "angry"]
        max_pred = float(np.array(str(np.max(pred))))
        max_pred_to_send = np.array(str(np.max(pred)))
        ind = np.where(pred == max_pred)[0][0]

        for i in range(len(all_emotions)):
            if(all_emotions[i] == expected_emotion):
                emotion_ind = i

        needed_pred = pred[emotion_ind]

        print('All predictions: ' + str(pred))
        print('Expected emotion: ' + str(expected_emotion) + ', achieved: ' + str(needed_pred))
        print('Detected emotion: ' + all_emotions[ind] + ' with pred: ' + str(max_pred))


        pred_to_send = np.array(str(needed_pred))
    except:
        pred_to_send = np.array("0.1000")
    bytes_to_send = pred_to_send.tobytes()
    socket.send(bytes_to_send)



