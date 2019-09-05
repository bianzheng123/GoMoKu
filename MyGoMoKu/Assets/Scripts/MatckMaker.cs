using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class MatckMaker : MonoBehaviour {
    NetworkManager manager;
    public string roomName;
    List<GameObject> roomList = new List<GameObject>();
    [SerializeField]
    GameObject btn;
    [SerializeField]
    Transform parent;
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

    public void OnRefreshBtn()
    {
        manager.matchMaker.ListMatches(0,10,"",true,0,0,OnMatchList);
    }

    public void OnMatchList(bool success,string extendedInfo,List<MatchInfoSnapshot> matches)
    {
        if (!success)
        {
            Debug.Log("error");
            return;
        }
        ClearList();
        foreach (MatchInfoSnapshot match in matches)
        {
            GameObject go = Instantiate(btn,parent);
            roomList.Add(go);
        }
    }

    void ClearList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }
}
