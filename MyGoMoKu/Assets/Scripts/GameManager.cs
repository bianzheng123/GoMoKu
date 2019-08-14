using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public List<Player> players = new List<Player>();

    private void Awake()
    {
        int player1 = PlayerPrefs.GetInt("player1");
        int player2 = PlayerPrefs.GetInt("player2");
        for(int i = 0; i < players.Count; i++)
        {
            if(i == player1)
            {
                players[i].chessColor = ChessType.BLACK;
            }else if(i == player2)
            {
                players[i].chessColor = ChessType.WHITE;
            }
            else
            {
                players[i].chessColor = ChessType.WATCH;
            }
        }
    }

    public void SetPlayer1(int i)
    {
        PlayerPrefs.SetInt("player1",i);
    }

    public void SetPlayer2(int i)
    {
        PlayerPrefs.SetInt("player2", i);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayNetGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ChangePlayer()
    {
        for(int i = 0; i < players.Count; i++)
        {
            if(players[i].chessColor == ChessType.BLACK)
            {
                SetPlayer2(i);
            }else if(players[i].chessColor == ChessType.WHITE)
            {
                SetPlayer1(i);
            }
            else
            {
                players[i].chessColor = ChessType.WATCH;
            }
        }
        SceneManager.LoadScene(1);
    }
}
