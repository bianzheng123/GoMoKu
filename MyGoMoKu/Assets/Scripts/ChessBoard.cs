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

    public bool PlayChess(int[] pos)
    {
        if (!gameStart) return false;
        pos[0] = Mathf.Clamp(pos[0], 0, 14);
        pos[1] = Mathf.Clamp(pos[1], 0, 14);

        if (grid[pos[0], pos[1]] != 0) return false;

        if(turn == ChessType.BLACK)
        {
            GameObject go = Instantiate(prefabs[0], new Vector2(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            go.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 1;
            if (CheckWinner(pos))
            {

            }
            turn = ChessType.WHITE;
        }
        else if(turn == ChessType.WHITE)
        {
            GameObject go = Instantiate(prefabs[1], new Vector2(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            go.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 2;
            if (CheckWinner(pos))
            {

            }
            turn = ChessType.BLACK;
        }

        return true;
    }

    public bool CheckWinner(int []pos)
    {
        return false;
    }
}

public enum ChessType
{
    WATCH,
    BLACK,
    WHITE
};
