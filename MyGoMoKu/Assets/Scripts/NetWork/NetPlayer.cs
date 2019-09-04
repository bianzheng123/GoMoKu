using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;


public class NetPlayer : NetworkBehaviour
{
    [SyncVar]
    public ChessType chessColor = ChessType.BLACK;

    protected Button retractBtn;

    protected void Start()
    {


        if (isLocalPlayer)
        {
            CmdSetPlayer();
            if(chessColor == ChessType.WATCH)
                return;
            retractBtn = GameObject.Find("RetractBtn").GetComponent<Button>();
            retractBtn.onClick.AddListener(RetractBtn);
        }
        Debug.Log(Network.player.ipAddress);
    }

    protected void FixedUpdate()
    {
        if (NetChessBoard.Instance.turn == chessColor && NetChessBoard.Instance.timer > 0.3f && isLocalPlayer && NetChessBoard.Instance.playerNumber > 1)
        {
            PlayChess();
        }
        if (chessColor != ChessType.WATCH && isLocalPlayer && !NetChessBoard.Instance.gameStart)
        {
            NetChessBoard.Instance.GameEnd();
        }
        if (isLocalPlayer)
        {
            ChangeBtnColor();
        }

    }

    void ChangeBtnColor()
    {
        if(chessColor == ChessType.WATCH)
        {
            retractBtn.interactable = false;
            return;
        }
        if (NetChessBoard.Instance.turn == chessColor)
        {
            retractBtn.interactable = true;
        }
        else
        {
            retractBtn.interactable = false;
        }
    }

    protected void PlayChess()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//表示是否点击到游戏物体
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CmdChess(pos);
        }
    }
    [Command]
    private void CmdChess(Vector2 pos)
    {
        if (NetChessBoard.Instance.PlayChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) }))
        {
            NetChessBoard.Instance.timer = 0;
        }
    }

    [Command]
    private void CmdSetPlayer()
    {
        NetChessBoard.Instance.playerNumber++;
        if (NetChessBoard.Instance.playerNumber == 1)
        {
            chessColor = ChessType.BLACK;
        }
        else if (NetChessBoard.Instance.playerNumber == 2)
        {
            chessColor = ChessType.WHITE;
        }
        else
        {
            chessColor = ChessType.WATCH;
        }
    }

    public void RetractBtn()
    {
        CmdRetractBtn();
    }

    [Command]
    public void CmdRetractBtn()
    {
        NetChessBoard.Instance.RetractChess();
    }
}
