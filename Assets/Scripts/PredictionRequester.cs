using System;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System.Text;

public class PredictionRequester : RunAbleThread
{
    private RequestSocket client;

    private Action<string> onOutputReceived;
    private Action<Exception> onFail;

    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        using (RequestSocket client = new RequestSocket())
        {
            this.client = client;
            client.Connect("tcp://localhost:5555");
            while (Running)
            {
                //byte[] outputBytes = new byte[0];   change outputBytes to message
                string outputMessage = "";


                bool gotMessage = false;
                while (Running)
                {
                    try
                    {
                        gotMessage = client.TryReceiveFrameString(out outputMessage);
                        if (gotMessage) break;
                    } 
                    catch (Exception e)
                    {
                       // Debug.Log("exception: " + e);
                    }
                }

                if (gotMessage)
                {
                    var message = Encoding.ASCII.GetBytes(outputMessage);

                    var output = new float[message.Length / 4]; 
                    onOutputReceived?.Invoke(outputMessage);
                    gotMessage = false;
                }
            }
        }

        NetMQConfig.Cleanup(); 
    }

    public void SendInput(string input)
    {
        Debug.Log("send input ");
        try
        {

            var message = Encoding.ASCII.GetBytes(input);
            var output = new byte[message.Length];

            Buffer.BlockCopy(message, 0, output, 0, output.Length);
            client.SendFrame(output);
        }
        catch (Exception e)
        {
            onFail(e);
            Debug.Log(e);
        }
    }

    public void SetOnTextReceivedListener(Action<string> onOutputReceived, Action<Exception> fallback)
    {
        this.onOutputReceived = onOutputReceived;
        onFail = fallback;
    }
}
