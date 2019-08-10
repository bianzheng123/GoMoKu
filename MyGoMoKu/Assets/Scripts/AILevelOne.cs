using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILevelOne : Player
{
    Dictionary<string, float> toScore = new Dictionary<string, float>();
    float[,] score = new float[15, 15];

    private void Start()
    {
        toScore.Add("_aa_", 100);
        toScore.Add("_aa", 50);
        toScore.Add("aa_", 50);

        toScore.Add("_aaa_", 1000);
        toScore.Add("_aaa", 500);
        toScore.Add("aaa_", 500);

        toScore.Add("_aaaa_", 10000);
        toScore.Add("_aaaa", 5000);
        toScore.Add("aaaa_", 5000);

        toScore.Add("_aaaaa_", float.MaxValue);
        toScore.Add("_aaaaa", float.MaxValue);
        toScore.Add("aaaaa_", float.MaxValue);
        toScore.Add("aaaaa", float.MaxValue);
    }

    private void CheckOneLine(int[] pos, int[] offset,int chessType)
    {
        string str = "a";

        //加到右边
        for (int i = pos[0] + offset[0], j = pos[1] + offset[1]; (0 <= i && i < 15) && (0 <= j && j < 15); i += offset[0], j += offset[1])
        {
            if (ChessBoard.Instance.grid[i, j] == chessType)
            {
                str += "a";
            }else if (ChessBoard.Instance.grid[i,j] == 0)//这个位置上没有棋子
            {
                str += "_";
                break;
            }
            else//这个位置有对方的棋子
            {
                break;
            }

        }

        //加左边
        for (int i = pos[0] - offset[0], j = pos[1] - offset[1]; (0 <= i && i < 15) && (0 <= j && j < 15); i -= offset[0], j -= offset[1])
        {
            if (ChessBoard.Instance.grid[i, j] == chessType)
            {
                str = "a" + str;
            }
            else if(ChessBoard.Instance.grid[i,j] == 0)
            {
                str = "_" + str;
                break;
            }
            else
            {
                break;
            }

        }

        if (toScore.ContainsKey(str))
        {
            score[pos[0], pos[1]] += toScore[str]; 
        }
        
    }

    private void setScore(int[] pos)
    {
        score[pos[0], pos[1]] = 0;
        CheckOneLine(pos,new int[2] { 0,1},1);
        CheckOneLine(pos, new int[2] { 1, 0 },1);//代表黑棋的决策
        CheckOneLine(pos, new int[2] { 1, 1 },1);
        CheckOneLine(pos, new int[2] { 1, -1 },1);

        CheckOneLine(pos, new int[2] { 0, 1 }, 2);//代表白棋的打分机制
        CheckOneLine(pos, new int[2] { 1, 0 }, 2);
        CheckOneLine(pos, new int[2] { 1, 1 }, 2);
        CheckOneLine(pos, new int[2] { 1, -1 }, 2);
    }

    protected override void PlayChess()
    {
        if(ChessBoard.Instance.chessStack.Count == 0)
        {
            if (ChessBoard.Instance.PlayChess(new int[2] { 7, 7 }))
            {
                ChessBoard.Instance.timer = 0;
            }
            return;
        }

        float maxScore = 0;
        int []maxPos = new int[2] { 0, 0 };
        for(int i = 0; i < 15; i++)
        {
            for(int j = 0; j < 15; j++)
            {
                if(ChessBoard.Instance.grid[i,j] == 0)
                {
                    setScore(new int[2] { i, j });
                    if(score[i,j] >= maxScore)
                    {
                        maxScore = score[i, j];
                        maxPos[0] = i;
                        maxPos[1] = j;
                    }
                }
                
            }
        }

        if (ChessBoard.Instance.PlayChess(maxPos))
        {
            ChessBoard.Instance.timer = 0;
        }


    }

}
