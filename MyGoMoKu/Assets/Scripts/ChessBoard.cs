using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{

    static ChessBoard _instance;

    public ChessType turn = ChessType.BLACK;
    public int[,] grid;//默认没下棋值为0，黑棋值为1，白棋值为2
    public GameObject[] prefabs;
    public float timer = 0;
    public bool gameStart = true;
    Transform parent;
    public Stack<Transform> chessStack = new Stack<Transform>();

    public static ChessBoard Instance
    {
        get { return _instance; }

    }

    private void Awake()
    {
        if(Instance == null)
        {
            _instance = this;
        }    
    }

    private void Start()
    {
        grid = new int[15, 15];
        parent = GameObject.Find("Parent").transform;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    /// <summary>
    /// 判断是否能下棋以及是否胜利
    /// </summary>
    /// <param name="pos">下棋的位置</param>
    /// <returns>是否能下棋</returns>
    public bool PlayChess(int[] pos)
    {
        if (!gameStart) return false;
        pos[0] = Mathf.Clamp(pos[0], 0, 14);
        pos[1] = Mathf.Clamp(pos[1], 0, 14);

        if (grid[pos[0], pos[1]] != 0) return false;

        if(turn == ChessType.BLACK)
        {
            GameObject go = Instantiate(prefabs[0], new Vector2(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            chessStack.Push(go.transform);
            go.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 1;
            if (CheckWinner(pos))
            {
                GameEnd();
            }
            turn = ChessType.WHITE;
        }
        else if(turn == ChessType.WHITE)
        {
            GameObject go = Instantiate(prefabs[1], new Vector2(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            chessStack.Push(go.transform);
            go.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 2;
            if (CheckWinner(pos))
            {
                GameEnd();
            }
            turn = ChessType.BLACK;
        }

        return true;
    }

    private void GameEnd()
    {
        gameStart = false;
        Debug.Log(turn + "赢了");
    }

    private bool CheckWinner(int []pos)
    {
        if (CheckOneLine(pos, new int[2] { 0, 1 })) return true;
        if (CheckOneLine(pos, new int[2] { 1, 0 })) return true;
        if (CheckOneLine(pos, new int[2] { 1, 1 })) return true;
        if (CheckOneLine(pos, new int[2] { 1, -1 })) return true;
        return false;
    }

    private bool CheckOneLine(int []pos, int[] offset)
    {
        int lineNum = 1;

        //加到右边
        for(int i=pos[0] + offset[0],j=pos[1] + offset[1];(0 <= i && i < 15) && (0 <= j && j < 15);i += offset[0],j += offset[1])
        {
            if (grid[i,j] == (int)turn)
            {
                lineNum++;
            }
            else
            {
                break;
            }

        }

        //加左边
        for (int i = pos[0] - offset[0], j = pos[1] - offset[1]; (0 <= i && i < 15) && (0 <= j && j < 15); i -= offset[0], j -= offset[1])
        {
            if (grid[i, j] == (int)turn)
            {
                lineNum++;
            }
            else
            {
                break;
            }

        }

        if (lineNum > 4)
        {
            return true;
        }
        return false;
    }

    public void RetractChess()
    {
        if(chessStack.Count > 1)
        {
            Transform pos = chessStack.Pop();
            grid[(int)pos.position.x + 7, (int)pos.position.y + 7] = 0;
            Destroy(pos.gameObject);

            pos = chessStack.Pop();
            grid[(int)pos.position.x + 7, (int)pos.position.y + 7] = 0;
            Destroy(pos.gameObject);
        }
    }
}

public enum ChessType
{
    WATCH,
    BLACK,
    WHITE
};
