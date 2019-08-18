using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;


public class NetPlayer : NetworkBehaviour
{
    public ChessType chessColor;

    protected Button retractBtn;

    private bool isDouble = false;

    protected virtual void Start()
    {
        retractBtn = GameObject.Find("RetractBtn").GetComponent<Button>();
        int boolIsDouble = PlayerPrefs.GetInt("isDouble");
        //Debug.Log("isDouble: " + boolIsDouble);
        isDouble = boolIsDouble == 1 ? true : false;
    }

    protected virtual void FixedUpdate()
    {
        if (ChessBoard.Instance.turn == chessColor && ChessBoard.Instance.timer > 0.3f)
        {
            PlayChess();
        }
        if (ChessBoard.Instance.timer < 0.3f && isDouble)
        {
            retractBtn.interactable = false;
        }
        else
        {
            retractBtn.interactable = true;
        }

    }

    protected virtual void PlayChess()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//表示是否点击到游戏物体
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //print((int)(pos.x + 7.5f) + " " + (int)(pos.y + 7.5f));
            if (ChessBoard.Instance.PlayChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) }))
            {
                ChessBoard.Instance.timer = 0;
            }
        }
    }
}
