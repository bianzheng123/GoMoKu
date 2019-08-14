using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIFollow : MonoBehaviour
{
    void Update()
    {
        if (ChessBoard.Instance.chessStack.Count > 0)
        {
            transform.position = ChessBoard.Instance.chessStack.Peek().position;
        }
    }

    public void ReturnBtn()
    {
        SceneManager.LoadScene("01_Start");
    }

    public void ReplayBtn()
    {
        SceneManager.LoadScene("02_Game");
    }
}
