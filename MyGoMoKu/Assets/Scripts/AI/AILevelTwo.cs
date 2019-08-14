using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILevelTwo : AILevelOne
{
    protected override void Start()
    {
        toScore.Add("aa___", 100);                      //眠二
        toScore.Add("a_a__", 100);
        toScore.Add("___aa", 100);
        toScore.Add("__a_a", 100);
        toScore.Add("a__a_", 100);
        toScore.Add("_a__a", 100);
        toScore.Add("a___a", 100);


        toScore.Add("__aa__", 500);                     //活二 "_aa___"
        toScore.Add("_a_a_", 500);
        toScore.Add("_a__a_", 500);

        toScore.Add("_aa__", 500);
        toScore.Add("__aa_", 500);


        toScore.Add("a_a_a", 1000);                     // bool lfirst = true, lstop,rstop = false  int AllNum = 1
        toScore.Add("aa__a", 1000);
        toScore.Add("_aa_a", 1000);
        toScore.Add("a_aa_", 1000);
        toScore.Add("_a_aa", 1000);
        toScore.Add("aa_a_", 1000);
        toScore.Add("aaa__", 1000);                     //眠三

        toScore.Add("_aa_a_", 9000);                    //跳活三
        toScore.Add("_a_aa_", 9000);

        toScore.Add("_aaa_", 10000);                    //活三       


        toScore.Add("a_aaa", 15000);                    //冲四
        toScore.Add("aaa_a", 15000);                    //冲四
        toScore.Add("_aaaa", 15000);                    //冲四
        toScore.Add("aaaa_", 15000);                    //冲四
        toScore.Add("aa_aa", 15000);                    //冲四        


        toScore.Add("_aaaa_", 1000000);                 //活四

        toScore.Add("aaaaa", float.MaxValue);           //连五
        //if(chessColor == ChessType.BLACK)
        //{
        //    Debug.Log(chessColor + " LevelTwo");
        //}
    }

    protected override void CheckOneLine(int[] pos, int[] offset, int chessType)
    {
        bool lfirst = true, lstop = false, rstop = false;
        int allNum = 1;
        string str = "a";
        int li = -offset[0], lj = -offset[1];
        int ri = offset[0], rj = offset[1];
        while (allNum <= 7 && (!lstop || !rstop))
        {
            if (lfirst)//代表先扫左边
            {//扫左边
                if( (0 <= pos[0] + li && pos[0] + li < 15) && (0 <= pos[1] + lj && pos[1] + lj < 15) && !lstop)
                {
                    if(ChessBoard.Instance.grid[pos[0] + li,pos[1] + lj] == chessType)
                    {
                        str = "a" + str;
                        allNum++;
                    }else if(ChessBoard.Instance.grid[pos[0] + li,pos[1] + lj] == 0)//代表没有棋子
                    {
                        str = "_" + str;
                        allNum++;
                        if (!rstop) { lfirst = false; }
                    }
                    else//代表碰到了对方棋子
                    {
                        lstop = true;
                        if (!rstop){ lfirst = false; }
                    }
                    li -= offset[0];
                    lj -= offset[1];
                }
                else//代表碰到了墙壁
                {
                    lstop = true;
                    if (!rstop) { lfirst = false; }
                }
            }
            else
            {//扫右边
                if ((0 <= pos[0] + ri && pos[0] + ri < 15) && (0 <= pos[1] + rj && pos[1] + rj < 15) && !lfirst && !rstop)
                {
                    if (ChessBoard.Instance.grid[pos[0] + ri, pos[1] + rj] == chessType)
                    {
                        str += "a";
                        allNum++;
                    }
                    else if (ChessBoard.Instance.grid[pos[0] + ri, pos[1] + rj] == 0)//代表没有棋子
                    {
                        str += "_";
                        if (!lstop) { lfirst = true; }
                        allNum++;
                    }
                    else//代表碰到了对方棋子
                    {
                        rstop = true;
                        if (!lstop) { lfirst = true; }
                    }

                    ri += offset[0];
                    rj += offset[1];
                }
                else//代表碰到了墙壁
                {
                    rstop = true;
                    if (!lstop) { lfirst = true; }
                }
            }
        }

        string cmpstr = "";
        foreach (var keyInfo in toScore)
        {
            if (str.Contains(keyInfo.Key))
            {
                if(cmpstr != "")
                {
                    if(toScore[keyInfo.Key] > toScore[cmpstr])
                    {
                        cmpstr = keyInfo.Key;
                    }
                }
                else
                {
                    cmpstr = keyInfo.Key;
                }
            }
        }

        if(cmpstr != "")
        {
            score[pos[0],pos[1]] += toScore[cmpstr];
        }
    }

}
