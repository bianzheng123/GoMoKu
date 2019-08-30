using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;


public class NetPlayer : NetworkBehaviour
{
    public ChessType chessColor = ChessType.BLACK;

    //protected Button retractBtn;

    protected virtual void Start()
    {
        //retractBtn = GameObject.Find("RetractBtn").GetComponent<Button>();
        
        if (isLocalPlayer)
        {
            NetChessBoard.Instance.playerNumber++;
            if(NetChessBoard.Instance.playerNumber == 1)
            {
                chessColor = ChessType.BLACK;
            }else if(NetChessBoard.Instance.playerNumber == 2)
            {
                chessColor = ChessType.WHITE;
            }
            else
            {
                chessColor = ChessType.WATCH;
            }
        }
        
        Debug.Log(NetChessBoard.Instance.playerNumber);
    }

    protected virtual void FixedUpdate()
    {
        if (NetChessBoard.Instance.turn == chessColor && NetChessBoard.Instance.timer > 0.3f)
        {
            PlayChess();
        }
        //if (NetChessBoard.Instance.timer < 0.3f && isDouble)
        //{
        //    retractBtn.interactable = false;
        //}
        //else
        //{
        //    retractBtn.interactable = true;
        //}

    }

    protected virtual void PlayChess()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//表示是否点击到游戏物体
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //print((int)(pos.x + 7.5f) + " " + (int)(pos.y + 7.5f));
            if (NetChessBoard.Instance.PlayChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) }))
            {
                NetChessBoard.Instance.timer = 0;
            }
        }
    }
}
