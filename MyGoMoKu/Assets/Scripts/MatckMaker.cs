using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class MatckMaker : MonoBehaviour {
    NetworkManager manager;
    public string roomName;
    private void Start()
    {
        manager = NetworkManager.singleton;
        if(manager.matchMaker == null)
        {
            manager.StartMatchMaker();
        }
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    }

    public void OnCreateNameBtn()
    {
        manager.matchMaker.CreateMatch(roomName,3,true,"","","",0,0,manager.OnMatchCreate);
    }
}
