using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using SimpleJSON;

public class BeeperChatManager : Singleton<BeeperChatManager>
{
    [SerializeField] private BeeperChatReceiver receiver;

    private void Awake()
    {
        receiver.OnReceiveFromBeeperChat += SendMessageToPlayer;
    }

    private void Update()
    {
        string message = receiver.Message;
        if(message != null && message != "" && !PlayerManager.Instance.InCinematic)
        {
            receiver.OnReceiveFromBeeperChat(message);
        }
    }

    private void OnEnable()
    {
        receiver.Init();
    }

    private void OnDisable()
    {
        receiver.Abort();
    }

    private void OnApplicationQuit()
    {
        receiver.Abort();
    }

    private void SendMessageToPlayer(string message)
    {
        JSONObject msg = JSONNode.Parse(message) as JSONObject;
        if (msg["ip"] == GameManager.Instance.IP)
        {
            PlayerManager.Instance.ReceiveAnswer(msg["msg"]);
        }
        //BeeperChatMessage msg = JsonUtility.FromJson<BeeperChatMessage>(message);
        //print(msg.Message);
        //print(msg.IP);
    }
}