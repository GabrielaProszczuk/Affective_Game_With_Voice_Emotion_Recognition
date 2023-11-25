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
            Debug.Log("in run");
            while (Running)
            {
                //byte[] outputBytes = new byte[0];   change outputBytes to message
                string outputMessage = "";


                bool gotMessage = false;
                while (Running)
                {
                    try
                    {
                        //gotMessage = client.TryReceiveFrameBytes(out outputBytes); // this returns true if it's successful
                        gotMessage = client.TryReceiveFrameString(out outputMessage);
                       /* Debug.Log("got message: " + gotMessage);
                        Debug.Log("output message: " + outputMessage);*/
                        if (gotMessage) break;
                    } 
                    catch (Exception e)
                    {
                       // client.SendFrame("error");
                        //var reply = client.ReceiveFrameString();
                       // Debug.Log("exception: " + e);
                    }
                }

                if (gotMessage)
                {
                    Debug.Log("in gotmerssaage");
                    //var output = new float[outputBytes.Length / 4];
                    var message = Encoding.ASCII.GetBytes(outputMessage);

                   /* foreach (var o in outputMessage)
                    {
                        Debug.Log("output message: " + o);
                    }*/
                    var output = new float[message.Length / 4];  //Encoding.ASCII.GetBytes(outputBytes);
                                                             //var output = "";
                   /* foreach (var o in output)
                    {
                        Debug.Log("got output: " + o);
                    }*/

                    // Buffer.BlockCopy(outputBytes, 0, output, 0, outputBytes.Length);
                    // Buffer.BlockCopy(message, 0, output, 0, message.Length);
                    //onOutputReceived?.Invoke(output);
                    onOutputReceived?.Invoke(outputMessage);
                    gotMessage = false;
                }
            }
        }

        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }

    public void SendInput(string input)
    {
        Debug.Log("send input ");
        try
        {
            // var byteArray = Encoding.ASCII.GetBytes(input);
            // var byteArray = new byte[input.Length * 4];

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
        Debug.Log("on set on text received listener");
        this.onOutputReceived = onOutputReceived;
        onFail = fallback;
    }
}