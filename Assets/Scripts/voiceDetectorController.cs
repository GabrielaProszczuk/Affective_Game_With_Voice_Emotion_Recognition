using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class voiceDetectorController : MonoBehaviour
{

    //test
    public Text voiceText;
    public GameObject voiceTestPlate;

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;


    private int sampleWindow = 1028;
    private AudioSource source;

    private AudioClip microphoneClip;
    string microphoneName;
    float[] samples;

    //recording
    internal string FILENAME;
    private int outputRate;
    private int headerSize = 44; //default for uncompressed wav
    private String fileName;
    private bool recOutput = false;
    private FileStream fileStream;
    float[] tempDataSource;

    //prediction
    private PredictionClient client;
    private string prediction;

    private List<string> filenames;

    string FILE_PATH;
    string saved_pred ="";

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("This message will make the console appear in Development Builds");
        client = new PredictionClient();
        prediction = "";
        filenames = new List<string>();
        keywordActions = new Dictionary<string, Action>();
        keywordActions.Add("cast", Cast);
        keywordActions.Add("fight", Fight);
        keywordActions.Add("save", Save);
        keywordActions.Add("take", Take);

        if (PlayerPrefs.GetInt("level") == 1)
        {
            try
            {
                keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray(), ConfidenceLevel.Low);
                keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
                keywordRecognizer.Start();
            }
            catch
            {
                Debug.Log("in catch");
            }
            
        }
        else
        {
            Debug.Log("in else");
            /* keywordRecognizer.Dispose();*/
            try
            {
                keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray(), ConfidenceLevel.Low);
                keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
                keywordRecognizer.Start();
            }
            catch
            {
                Debug.Log("in catch 2&3");
            }


        }
        
        // AudioFromMicrophone();
        microphoneName = Microphone.devices[0];
        PlayerPrefs.SetInt("startRecording", 0);
        PlayerPrefs.SetString("emotionLevel1", "");
        PlayerPrefs.SetString("emotionLevel2", "");
        PlayerPrefs.SetString("emotionLevel3", "");
        PlayerPrefs.SetInt("detect", 0);
        PlayerPrefs.SetInt("resetEmotion", 0);
        PlayerPrefs.SetInt("cast", 0);
        PlayerPrefs.SetInt("monsterTouched", 0);
        PlayerPrefs.SetInt("wizardTouched", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("prediction main: " + prediction);

        if (PlayerPrefs.GetInt("resetPred") == 1)
        {
            prediction = "";
            PlayerPrefs.SetInt("resetPred", 0);
        }
        PlayerPrefs.SetString("emotionLevel1", prediction);
        PlayerPrefs.SetString("emotionLevel2", prediction);
        PlayerPrefs.SetString("emotionLevel3", prediction);

        if (prediction == "")
        {
            PlayerPrefs.SetInt("clean", 1);
        }
        else
        {
            PlayerPrefs.SetInt("clean", 0);
        }

        if(PlayerPrefs.GetInt("newLevelLoaded") == 1)
        {
            prediction = "";
            PlayerPrefs.SetString("emotionLevel1", "");
            PlayerPrefs.SetString("emotionLevel2", "");
            PlayerPrefs.SetString("emotionLevel3", "");
            PlayerPrefs.SetInt("newLevelLoaded", 0);

        }
    }

    public void AudioFromMicrophone()
    {

        microphoneClip = Microphone.Start(microphoneName, true, 10, AudioSettings.outputSampleRate);
    }

    //SPEECH RECOGNITION

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Detected: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void Cast()
    {
        Debug.Log("cast");
        
        
        PlayerPrefs.SetInt("startRecording", 1);
        PlayerPrefs.SetInt("cast", 1);
        PlayerPrefs.SetInt("resetEmotion", 1);
        if (PlayerPrefs.GetInt("level") == 1)
        {
            PlayerPrefs.SetString("emotionLevel1", prediction);
        }
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            PlayerPrefs.SetString("emotionLevel2", prediction);
        }
        else
        {
            PlayerPrefs.SetString("emotionLevel3", prediction);
        }
        prediction = "";
        AudioFromMicrophone();


    }

    private void Take()
    {
        Debug.Log("take");
        PlayerPrefs.SetInt("resetEmotion", 0);
        // voiceText.text = "take";
        StopRecording("take");

    }

    private void Save()
    {
        PlayerPrefs.SetString("selectedEmotion", "");
        PlayerPrefs.SetInt("resetEmotion", 0);
        Debug.Log("save");
        //voiceText.text =  "save";
        StopRecording("save");
    }

    private void Fight()
    {
        PlayerPrefs.SetString("selectedEmotion", "");
        PlayerPrefs.SetInt("resetEmotion", 0);
        Debug.Log("fight");
       // voiceText.text = "fight";
        StopRecording("fight");

    }

    public void StopRecording(string command)
    {
        if (PlayerPrefs.GetInt("startRecording") == 1)
        {
            Debug.Log("save wav");
            PlayerPrefs.SetString("emotionLevel1", "");
            PlayerPrefs.SetString("emotionLevel2", "");
            PlayerPrefs.SetString("emotionLevel3", "");
            string date = System.DateTime.Now.ToString().Replace(" ", "_").Replace(":", "").Replace(".", "");
            if (PlayerPrefs.GetString("selectedEmotion") == "")
            {
                FILENAME = command + "_" + date;
            }
            else
            {
                FILENAME = command + "_" + PlayerPrefs.GetString("selectedEmotion") + "_" + date;
            }
            try
            {
                SavWav.Save("C:/Users/lenovoo/OneDrive/Pulpit/Wszystko/MGR/new/audio_recordings/" + FILENAME, microphoneClip);
            }
            catch
            {
                Debug.Log("error with saving file");
                if (PlayerPrefs.GetString("selectedEmotion") == "")
                {
                    FILENAME = command + "_" + date;
                }
                else
                {
                    FILENAME = command + "_" + PlayerPrefs.GetString("selectedEmotion") + "_" + date;
                }
                SavWav.Save("C:/Users/lenovoo/OneDrive/Pulpit/Wszystko/MGR/new/audio_recordings/" + FILENAME, microphoneClip);
            }
            PlayerPrefs.SetInt("startRecording", 0);
        }
        Predict();
        Debug.Log("in stop recording");
        PlayerPrefs.SetInt("resetEmotion", 0);
        if (PlayerPrefs.GetInt("level") == 1)
        {
            PlayerPrefs.SetString("emotionLevel1", prediction);
        }
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            PlayerPrefs.SetString("emotionLevel2", prediction);
        }
        else
        {
            PlayerPrefs.SetString("emotionLevel3", prediction);
        }

    }

    IEnumerator waitBeforePredict(float waitTime)
    {
        float counter = 0;

        Debug.Log("in wait");
        while (counter < waitTime)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        

    }


    private void Predict()
    {

        // "C:/Users/lenovoo/OneDrive/Pulpit/Wszystko/MGR/new/audio_recordings/"

        string FILE_PATH = "C:/Users/student/Documents" + FILENAME + ".wav";
        Debug.Log("filepath: " + FILE_PATH);
        bool correctPath = File.Exists(FILE_PATH);
        string ex = Path.GetExtension(FILE_PATH);
       // string mime = System.Web.MimeMapping.GetMimeMapping(FILE_PATH);
        if (!filenames.Contains(FILENAME) && correctPath && ex == ".wav")
        {
            if (FILE_PATH != "C:/Users/student/Documents.wav")
            {
                client.Predict(FILE_PATH, output =>
                {
                    var length = output.Length;
                    if (Convert.ToString(output[0]) == "1")
                    {
                        prediction = "99";
                    }
                    else
                    {
                        if (length <= 12)
                        {
                            prediction = Convert.ToString(output[8]);
                        }
                        else
                        {
                            prediction = Convert.ToString(output[8]) + Convert.ToString(output[12]);
                        }
                    }
                    Debug.Log("prediction: " + prediction);

                    filenames = filenames.Append(FILENAME).ToList();
                }, error =>
                {
                    // TODO: when i am not lazy
                    Debug.Log(error);
                });
            }
        }
    }



}
