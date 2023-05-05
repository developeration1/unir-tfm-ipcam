using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using TMPro;
using System.Collections;

public class PythonListener : MonoBehaviour
{
    [SerializeField] string IP = "127.0.0.1"; // local host
    [SerializeField] int rxPort = 8500; // port to receive data from Python on
    [SerializeField] TMP_Text text;
    [SerializeField] float refreshTime = 0.5f;

    UdpClient client;
    Thread receiveThread; // Receiving Thread
    string mssg = "";

    private void Awake()
    {
        // Create local client
        client = new UdpClient(rxPort);

        // local endpoint define (where messages are received)
        // Create a new thread for reception of incoming messages
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        // Initialize (seen in comments window)
        print("UDP Comms Initialised");
    }

    private IEnumerator Start()
    {
        while (true)
        {
            text.text = mssg;
            yield return new WaitForSeconds(refreshTime);
        }
    }

    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);
                print(">> " + text);
                // this.text.text = text;
                mssg = text;
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }
}
