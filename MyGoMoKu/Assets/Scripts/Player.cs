using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour
{
    public ChessType chessColor;

    private void FixedUpdate()
    {
        if(ChessBoard.Instance.turn == chessColor && ChessBoard.Instance.timer > 0.3f)
        {
            PlayChess();
        }

    }

    public void PlayChess()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//表示是否点击到游戏物体
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //print((int)(pos.x + 7.5f) + " " + (int)(pos.y + 7.5f));
            ChessBoard.Instance.PlayChess(new int[2] { (int)(pos.x + 7.5f) , (int)(pos.y + 7.5f)});
            ChessBoard.Instance.timer = 0;
        }
    }
}
