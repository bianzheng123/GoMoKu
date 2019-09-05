using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
public class JoinButton : MonoBehaviour {
    NetworkManager manager;

    public Text nameTxt;
    public MatchInfoSnapshot info;

    private void Start()
    {
        manager = NetworkManager.singleton;
        if (manager.matchMaker == null)
        {
            manager.StartMatchMaker();
        }
    }

    //放在MathchMaker类中进行初始化的
    public void SetUp(MatchInfoSnapshot _info)
    {
        info = _info;
        nameTxt.text = info.name;
    }

    //放在加入房间进行初始化的
    public void OnJointBtn()
    {
        manager.matchMaker.JoinMatch(info.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
    }
}
