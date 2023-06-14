using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ChessBoard : MonoBehaviour 
{

    public GameObject cellPrefab;
    public LayerMask CellLayerMask = -1;
    private Cell currentPointedCell = null;
    private Cell currentSelectedCell = null;

    private Cell[][] cells;
    private Vector3 startPosition = Vector3.zero;
    public Cell[][] Cells
    {
        get
        {
            return cells;
        }
        
    }

    private float cell_size = -1;
    public float CELL_SIZE
    {
        get 
        {
            cell_size =  cellPrefab.GetComponent<Cell>().SIZE;
            return cell_size;
        }
    }
    [ContextMenu("CheckBoa")]
    public void Check()
    {
        InitChessBoard();
    }

    private void Awake()
    {
        cell_size = CELL_SIZE;
    }

    private void Start()
    {
        
        //startPosition = transform.position;
        //InitChessBoard();
    }

    private void Update()
    {
        
        if(BaseGame_CTL.Current.GameState == EGameState.PLAYING)
        {
            CheckUserInput();
        }

    }

    

    private void CheckUserInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, CellLayerMask.value))
        {
            //Debug.DrawLine(ray.origin, hit.point);
            //Debug.Log(hit.collider.name);
            Cell newPointedCell = hit.collider.GetComponent<Cell>();

            if (currentPointedCell != newPointedCell)
            {
                if (currentPointedCell != null)
                    currentPointedCell.SetCellState(ECellState.NORMAL);
                currentPointedCell = newPointedCell;
                currentPointedCell.SetCellState(ECellState.POINTED);

            }
            
        }
        else
        {
            if(currentPointedCell != null)
            {
                currentPointedCell.SetCellState(ECellState.NORMAL );
                currentPointedCell = null;
            }
        }
        //Check mouse position

        if(Input.GetMouseButtonDown(0))
        {
            if(currentPointedCell != null)
            {
                
                if (currentSelectedCell != null && currentSelectedCell != currentPointedCell)
                {
                    currentSelectedCell.SetCellState(ECellState.SELECTED);
                    currentSelectedCell = null;
                   
                }
                if (currentSelectedCell == currentPointedCell)
                {
                    //currentSelectedCell = currentPointedCell;
                    //double selected => normal 
                    currentSelectedCell.SetCellState(ECellState.SELECTED);
                    currentSelectedCell = null;
                } else
                {
                    currentSelectedCell = currentPointedCell;
                    currentSelectedCell.SetCellState(ECellState.SELECTED);
                }
                
               
            }
            //Debug.Log(currentSelectedCell.CellState.ToString());
                
        }
    }

    public void InitChessBoard()
    {
        cells = new Cell[8][];
        for (int i = 0; i < 8; i++) 
        {
            cells[i] = new Cell[8];
            for (int j = 0; j < 8; j++)
            {
                GameObject c = GameObject.Instantiate(cellPrefab, CalculatePosition(i, j), Quaternion.identity);
                c.transform.parent = this.transform;
                cells[i][j] = c.GetComponent<Cell>();
                if((i + j)%2 == 0)
                {
                    cells[i][j].CellColor = ECellColor.BLACK;
                } else
                {
                    cells[i][j].CellColor = ECellColor.WHITE;

                }
            }
        }
    }

    public Vector3 CalculatePosition(int i, int j)
    {
        //float SIZE = cellPrefab.GetComponent<Cell>().SIZE;
        return startPosition + new Vector3(i * cell_size, 0, j * cell_size);
    }

}
