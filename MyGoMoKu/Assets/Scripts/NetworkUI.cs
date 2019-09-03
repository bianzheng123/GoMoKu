using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkUI : NetworkBehaviour {

    public void StartHost()
    {
        NetworkManager.singleton.StartHost();
    }
    public void StartClient()
    {
        NetworkManager.singleton.StartClient();
    }
    public void StopHost()
    {
        NetworkManager.singleton.StopHost();
    }

    public void OfflineSet()
    {
        Debug.Log(GameObject.Find("Host") == null);
        GameObject.Find("Host").GetComponent<Button>().onClick.AddListener(StartHost);
        GameObject.Find("Client").GetComponent<Button>().onClick.AddListener(StartClient);
    }

    public void OnlineSet()
    {
        GameObject.Find("ReturnBtn").GetComponent<Button>().onClick.AddListener(StopHost);
    }
    private void OnEnable()
    {
        Debug.Log("execute");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded");
        switch (scene.buildIndex)
        {
            case 0:
                OfflineSet();
                break;
            case 1:
                OnlineSet();
                break;
            default:
                break;
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
