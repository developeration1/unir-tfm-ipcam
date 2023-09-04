using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

[Serializable]
public class BeeperChatReceiver
{

    [SerializeField] private string IP = "127.0.0.1"; // local host
    [SerializeField] private int rxPort = 8500; // port to receive data from Python on

    private UdpClient client;
    private Thread receiveThread;
    private string message = "";

    public string Message
    {
        get
        {
            string temp = message;
            message = "";
            return temp;
        }
    }

    public delegate void ReceiveFromBeeperChat(string message);
    public ReceiveFromBeeperChat OnReceiveFromBeeperChat;

    public void Init()
    {
        client = new UdpClient(rxPort);
        receiveThread = new Thread(new ThreadStart(ReceiveData))
        {
            IsBackground = true
        };
        receiveThread.Start();

        Debug.Log("UDP Comms Initialised");
    }

    public void Abort()
    {
        receiveThread.Abort();
    }

    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                message = Encoding.UTF8.GetString(data);
                Debug.Log(message);
                //OnReceiveFromBeeperChat.Invoke(text);
            }
            catch (Exception err)
            {
                Debug.LogError(err.ToString());
            }
        }
    }
}
