using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MiniMaxNode
{
    public int chess;//代表在这个点决策的类型
    public int[] pos;//代表决策的位置
    public List<MiniMaxNode> child;//子节点
    public float value;//这个决策产生的值
}

public class AILevelThree : Player
{
    private Dictionary<string, float> toScore = new Dictionary<string, float>();

    private void Start()
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
    }

    private float CheckOneLine(int[,] grid, int[] pos, int[] offset, int chessType)
    {
        float score = 0;
        bool lfirst = true, lstop = false, rstop = false;
        int allNum = 1;
        string str = "a";
        int li = -offset[0], lj = -offset[1];
        int ri = offset[0], rj = offset[1];
        while (allNum <= 7 && (!lstop || !rstop))
        {
            if (lfirst)//代表先扫左边
            {//扫左边
                if ((0 <= pos[0] + li && pos[0] + li < 15) && (0 <= pos[1] + lj && pos[1] + lj < 15) && !lstop)
                {
                    if (grid[pos[0] + li, pos[1] + lj] == chessType)
                    {
                        str = "a" + str;
                        allNum++;
                    }
                    else if (grid[pos[0] + li, pos[1] + lj] == 0)//代表没有棋子
                    {
                        str = "_" + str;
                        allNum++;
                        if (!rstop) { lfirst = false; }
                    }
                    else//代表碰到了对方棋子
                    {
                        lstop = true;
                        if (!rstop) { lfirst = false; }
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
                    if (grid[pos[0] + ri, pos[1] + rj] == chessType)
                    {
                        str += "a";
                        allNum++;
                    }
                    else if (grid[pos[0] + ri, pos[1] + rj] == 0)//代表没有棋子
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
                if (cmpstr != "")
                {
                    if (toScore[keyInfo.Key] > toScore[cmpstr])
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

        if (cmpstr != "")
        {
            score += toScore[cmpstr];
        }
        return score;
    }

    private float GetScore(int[,] grid, int[] pos)
    {
        float score = 0;
        score += CheckOneLine(grid, pos, new int[2] { 0, 1 }, 1);
        score += CheckOneLine(grid, pos, new int[2] { 1, 0 }, 1);//代表黑棋的决策
        score += CheckOneLine(grid, pos, new int[2] { 1, 1 }, 1);
        score += CheckOneLine(grid, pos, new int[2] { 1, -1 }, 1);

        score += CheckOneLine(grid, pos, new int[2] { 0, 1 }, 2);//代表白棋的打分机制
        score += CheckOneLine(grid, pos, new int[2] { 1, 0 }, 2);
        score += CheckOneLine(grid, pos, new int[2] { 1, 1 }, 2);
        score += CheckOneLine(grid, pos, new int[2] { 1, -1 }, 2);
        return score;
    }

    protected override void PlayChess()
    {
        if (ChessBoard.Instance.chessStack.Count == 0)
        {
            if (ChessBoard.Instance.PlayChess(new int[2] { 7, 7 }))
            {
                ChessBoard.Instance.timer = 0;
            }
            return;
        }
        MiniMaxNode node = null;
        foreach (MiniMaxNode item in GetList(ChessBoard.Instance.grid, (int)chessColor, true))
        {
            CreateTree(item, 3, (int[,])ChessBoard.Instance.grid.Clone(), false);
            item.value += AlphaBeta(item, 3, float.MinValue, float.MaxValue, false);//其实每一步都有一个决策的分数，最终的分数算的是总和
            if (node != null)
            {
                if (node.value < item.value)
                {
                    node = item;
                }
            }
            else
            {
                node = item;
            }
        }
        ChessBoard.Instance.PlayChess(node.pos);

    }

    List<MiniMaxNode> GetList(int[,] grid, int chess, bool mySelf)
    {
        List<MiniMaxNode> list = new List<MiniMaxNode>();
        MiniMaxNode node;
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                int[] pos = new int[2] { i, j };
                if (grid[pos[0], pos[1]] != 0) continue;
                node = new MiniMaxNode();
                node.chess = chess;
                node.pos = pos;
                if (mySelf)
                    node.value = GetScore(grid, pos);
                else
                    node.value = -GetScore(grid, pos);
                if (list.Count < 4)
                {
                    list.Add(node);
                }
                else
                {
                    foreach (MiniMaxNode item in list)
                    {
                        if (mySelf && item.value < node.value)//极大点
                        {
                            list.Remove(item);
                            list.Add(node);
                            break;
                        }
                        if (!mySelf && item.value > node.value)//极小点
                        {
                            list.Remove(item);
                            list.Add(node);
                            break;
                        }
                    }

                }
            }
        }
        return list;
    }

    public void CreateTree(MiniMaxNode node, int deep, int[,] grid, bool mySelf)
    {
        if (deep == 0 || node.value == float.MaxValue)
        {
            return;
        }
        grid[node.pos[0], node.pos[1]] = node.chess;
        node.child = GetList(grid, node.chess, !mySelf);
        foreach (MiniMaxNode item in node.child)
        {
            CreateTree(item, deep - 1, (int[,])grid.Clone(), !mySelf);
        }
    }

    /// <summary>
    /// 通过alpha和beta来进行筛选,这里的alpha和beta控制的是范围
    /// 每一层递归，alpha和beta的位置都进行了一次互换，所以返回的值都是alpha和beta
    /// </summary>
    /// <param name="node"></param>
    /// <param name="deep"></param>
    /// <param name="alpha"></param>
    /// <param name="beta"></param>
    /// <param name="mySelf"></param>
    /// <returns>当前决策树最佳的策略的结果</returns>
    public float AlphaBeta(MiniMaxNode node, int deep, float alpha, float beta, bool mySelf)
    {
        if (deep == 0 || (node.value == float.MaxValue) || (node.value == float.MinValue))
        {
            return node.value;
        }
        if (mySelf)
        {
            foreach (MiniMaxNode item in node.child)
            {
                alpha = Mathf.Max(alpha, AlphaBeta(item, deep - 1, alpha, beta, !mySelf));//通过遍历子节点找到最优解，从而抬高alpha的值
                if (alpha >= beta)
                {
                    return alpha;
                }
            }
            return alpha;
        }
        else
        {
            foreach (MiniMaxNode item in node.child)
            {
                beta = Mathf.Min(beta, AlphaBeta(item, deep - 1, alpha, beta, !mySelf));//通过遍历子节点找到最优解，从而抬高alpha的值
                if (alpha >= beta)
                {
                    return beta;
                }
            }
            return beta;
        }
    }

}
