using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MineSweeper : MonoBehaviour
{
    [SerializeField]
    private int rows = 1;
    [SerializeField]
    private int cols = 1;

    [SerializeField]
    private int mineCount = 1;

    [SerializeField]
    private Cell cellPrefab = null;

    [SerializeField]
    private GridLayoutGroup layoutGroup = null;

    public Cell[,] cells;

    public List<Cell> mine = new List<Cell>();

    private List<Cell> cellList = new List<Cell>();

    public bool z = true;

    private bool gameStart = false;

    public bool gameOver = false;
    void OnValidate()
    {
        if (cols < rows)
        {
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layoutGroup.constraintCount = cols;
        }
        else
        {
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            layoutGroup.constraintCount = rows;
        }
    }

    void Start()
    {
        cells = new Cell[rows, cols];

        for (int i = 0; i < 
            rows; i++)
        {
            for (int k = 0; k < cols; k++)
            {
                var cell = Instantiate(cellPrefab);
                cell.transform.SetParent(layoutGroup.transform);
                cell.x = i;
                cell.y = k;
                cells[i, k] = cell;
            }
        }
    }
    void Update()
    {
        if (!gameStart) return;
        if (gameOver)
        {
            Debug.Log("GameOver");
        }


    }
    public void CreateMine(Cell cellF)
    {
        for (int i = 0; i < mineCount;)
        {
            var row = Random.Range(0, rows);
            var col = Random.Range(0, cols);
            var cell = cells[row, col];
            if (cell.CellStates == CellState.CellStates.Mine && cell == cellF)
            {
                continue;
            }
            else
            {
                cell.CellStates = CellState.CellStates.Mine;
                mine.Add(cell);
                i++;
            }

        }
        for (int i = 0; i < rows; i++)
        {
            for (int k = 0; k < cols; k++)
            {
                var cell = cells[i, k];
                if (cell.CellStates == CellState.CellStates.Mine)
                {
                    continue;
                }
                MineCounts(i, k);
            }
        }
        gameStart = true;
    }
    
    void MineCounts(int x, int y)
    {
        var cell = cells[x, y];
        int xx = x;
        int yy = y;
        int intValue = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int k = -1; k <= 1; k++)
            {
                xx += i;
                yy += k;
                if (xx <= -1 || yy <= -1 || xx >= rows || yy >= cols)
                {
                    xx = x;
                    yy = y;
                }
                else if (cells[xx, yy].CellStates == CellState.CellStates.Mine)
                {
                    intValue++;
                    cell.CellStates = (CellState.CellStates)System.Enum.ToObject(typeof(CellState.CellStates), intValue);
                    xx = x;
                    yy = y;
                }
                else
                {
                    xx = x;
                    yy = y;
                }
            }
        }

    }
    public void Open(Cell cell)
    {
        if (cellList.Contains(cell))
        {
            cellList.Remove(cell);

        }

        var x = cell.x;
        var y = cell.y;
        int xx = x;
        int yy = y;

        for (int i = -1; i <= 1; i++)
        {
            for (int k = -1; k <= 1; k++)
            {
                xx += i;
                yy += k;
                if (xx <= -1 || yy <= -1 || xx >= rows || yy >= cols)
                {
                    xx = x;
                    yy = y;
                }
                else if (cells[xx, yy].CellStates == CellState.CellStates.None && cells[xx, yy].view.enabled == false)
                {
                    if (!cellList.Contains(cells[xx, yy]))
                    {
                        cellList.Add(cells[xx, yy]);
                    }
                    xx = x;
                    yy = y;
                }
                else
                {
                    xx = x;
                    yy = y;
                }
            }
        }
        for (int i = 0; i < cellList.Count; i++)
        {

            if (cellList[i].enabled != false)
            {
                cellList[i].view.enabled = true;
                cellList[i].chek.SetActive(false);
                cellList[i].sec.SetActive(true);           
            }

            Open(cellList[i]);
        }
       
    }

}
