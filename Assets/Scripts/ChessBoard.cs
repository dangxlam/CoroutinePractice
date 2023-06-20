using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.Progress;

public class ChessBoard : MonoBehaviour 
{
    public static ChessBoard _instance;
    public GameObject cellPrefab;
    public LayerMask CellLayerMask = -1;
    private Cell currentPointedCell = null;
    private Cell currentTargetCell = null;
    public Cell currentSelectedCell = null;

    public List<BaseChessPieces> listChessPieces;

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
        _instance = this;
        cell_size = CELL_SIZE;
    }

    private void Start()
    {
        
        //startPosition = transform.position;
        InitChessBoard();
        InitChessPieces();

        
        //Cells[listChessPieces[0].Location.x][listChessPieces[0].Location.y].SetCellState(ECellState.SELECTED);
        //listChessPieces[0].BeSelected();
    }

    private void Update()
    {

        //cells[1][2].SetCellState(ECellState.SELECTED);

        if (Game_CTL.Current.GameState == EGameState.PLAYING)
        {
            CheckUserInput();
        }

    }

    

    private void CheckUserInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Cell newPointedCell;
        if (Physics.Raycast(ray, out hit, 1000, CellLayerMask.value))
        {
            //Debug.DrawLine(ray.origin, hit.point);
            //Debug.Log(hit.collider.name);
            newPointedCell = hit.collider.GetComponent<Cell>();

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

        if (Input.GetMouseButtonDown(0))
        {
            if (currentPointedCell != null && Game_CTL.Current.CurrentPlayer == EPlayer.WHITE)
            {
                //Check if can move
                if(currentPointedCell.CellState == ECellState.MOVEABLE)
                {
                    //check if have enemy
                    //bool attack = false;
                    if (currentPointedCell.CurrentPiece != null)
                    {
                        
                        Debug.Log(currentPointedCell.CurrentPiece.Infor.Name + " Health: " + (currentPointedCell.CurrentPiece.Health - 1));
                        currentSelectedCell.CurrentPiece.Move(currentPointedCell, currentPointedCell.CurrentPiece.Health > 1, false, null);
                        
                        //currentSelectedCell.CurrentPiece.Move(currentSelectedCell);
                        if (--currentPointedCell.CurrentPiece.Health < 1)
                        {
                            string deadPiece = currentPointedCell.CurrentPiece.Infor.Name;
                            listChessPieces.Remove(currentPointedCell.CurrentPiece);
                            Debug.Log("DEad: " + deadPiece);
                            //GameObject.Destroy(currentPointedCell.CurrentPiece.gameObject);
                            //currentPointedCell.CurrentPiece = null;
                            cells[currentPointedCell.CellLocation.x][currentPointedCell.CellLocation.y].SetChessPiece(currentSelectedCell.CurrentPiece);
                            currentSelectedCell.SetCellState(ECellState.SELECTED);
                            cells[currentSelectedCell.CellLocation.x][currentSelectedCell.CellLocation.y].SetChessPiece(null);

                            
                            if (listChessPieces.Count == 1)
                                Game_CTL.Current.GameState = EGameState.GAME_OVER;
                        } else
                            currentSelectedCell.SetCellState(ECellState.SELECTED);

                        //currentSelectedCell.CurrentPiece.UnSelected();
                        currentSelectedCell = null;


                    }

                    //move to empty cell
                    //if (currentPointedCell.CurrentPiece == null)
                    else
                    {
                        currentSelectedCell.CurrentPiece.Move(currentPointedCell, false, false, null);
                        //unselect and remove moveable
                        currentSelectedCell.SetCellState(ECellState.SELECTED);

                        //if(attack)
                        // BaseGame_CTL.Current.SwitchTurn();

                        //currentSelectedCell.CurrentPiece.UnSelected();
                        cells[currentPointedCell.CellLocation.x][currentPointedCell.CellLocation.y].SetChessPiece(currentSelectedCell.CurrentPiece);
                        cells[currentSelectedCell.CellLocation.x][currentSelectedCell.CellLocation.y].SetChessPiece(null);
                        currentSelectedCell = null;
                    }

                    Game_CTL.Current.SwitchTurn();




                } else
                {
                    
                    //select a cell not MOVEABLE
                    if (currentSelectedCell != null  || currentSelectedCell == currentPointedCell)
                    {
                        currentSelectedCell.SetCellState(ECellState.SELECTED);
                        currentSelectedCell.CurrentPiece.UnSelected();
                        currentSelectedCell = null;

                    }
                    
                    if (currentSelectedCell != currentPointedCell && currentPointedCell.CurrentPiece!= null && currentPointedCell.CurrentPiece.Player == EPlayer.WHITE)
                    {
                        //if (currentPointedCell.CurrentPiece != null)
                        //{
                            currentSelectedCell = currentPointedCell;
                            currentSelectedCell.SetCellState(ECellState.SELECTED);
                            currentSelectedCell.CurrentPiece.BeSelected();
                        //}

                    }

                    //Debug.Log(currentSelectedCell.CurrentPiece.Infor.Name);
                    //if(currentPointedCell.CurrentPiece != null)
                    //currentSelectedCell.CurrentPiece.BeSelected();
                }


            }
            
                
        }

        if (currentSelectedCell != null && currentSelectedCell.CurrentPiece.Player ==  EPlayer.WHITE)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Vector2Int checkcell = new Vector2Int(currentSelectedCell.CellLocation.x - 1, currentSelectedCell.CellLocation.y);
                if (CheckLocation.Check(checkcell))
                    currentTargetCell = Cells[checkcell.x][checkcell.y];

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                Vector2Int checkcell = new Vector2Int(currentSelectedCell.CellLocation.x, currentSelectedCell.CellLocation.y + 1);
                if (CheckLocation.Check(checkcell))
                    currentTargetCell = Cells[checkcell.x][checkcell.y];

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Vector2Int checkcell = new Vector2Int(currentSelectedCell.CellLocation.x, currentSelectedCell.CellLocation.y - 1);
                if (CheckLocation.Check(checkcell))
                    currentTargetCell = Cells[checkcell.x][checkcell.y];

            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Vector2Int checkcell = new Vector2Int(currentSelectedCell.CellLocation.x + 1, currentSelectedCell.CellLocation.y);
                if (CheckLocation.Check(checkcell))
                    currentTargetCell = Cells[checkcell.x][checkcell.y];

            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector2Int checkcell = new Vector2Int(currentSelectedCell.CellLocation.x - 1, currentSelectedCell.CellLocation.y + 1);
                if (CheckLocation.Check(checkcell))
                    currentTargetCell = Cells[checkcell.x][checkcell.y];

            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Vector2Int checkcell = new Vector2Int(currentSelectedCell.CellLocation.x + 1, currentSelectedCell.CellLocation.y + 1);
                if (CheckLocation.Check(checkcell))
                    currentTargetCell = Cells[checkcell.x][checkcell.y];

            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                Vector2Int checkcell = new Vector2Int(currentSelectedCell.CellLocation.x - 1, currentSelectedCell.CellLocation.y - 1);
                if (CheckLocation.Check(checkcell))
                    currentTargetCell = Cells[checkcell.x][checkcell.y];

            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Vector2Int checkcell = new Vector2Int(currentSelectedCell.CellLocation.x + 1, currentSelectedCell.CellLocation.y - 1);
                if (CheckLocation.Check(checkcell))
                    currentTargetCell = Cells[checkcell.x][checkcell.y];

            }
        }

        

        if (currentTargetCell != null)
        {
            if (listChessPieces[0].moveableCells.Contains(currentTargetCell))
            {
                MoveByKey();
            }
        }

    }

    private void MoveByKey ()
    {
        if (currentTargetCell.CurrentPiece != null)
        {

            //Debug.Log("CHealth: " + (currentTargetCell.CurrentPiece.Health - 1));
            //currentSelectedCell.CurrentPiece.UnSelected();
            Debug.Log(currentTargetCell.CurrentPiece.Infor.Name + " Health: " + (currentTargetCell.CurrentPiece.Health - 1));
            currentSelectedCell.CurrentPiece.Move(currentTargetCell, currentTargetCell.CurrentPiece.Health > 1, false, null);

            //currentSelectedCell.CurrentPiece.Move(currentSelectedCell);
            if (--currentTargetCell.CurrentPiece.Health < 1)
            {
                string deadPiece = currentTargetCell.CurrentPiece.Infor.Name;
                listChessPieces.Remove(currentTargetCell.CurrentPiece);
                Debug.Log("DEad: " + deadPiece);
                //GameObject.Destroy(currentPointedCell.CurrentPiece.gameObject);
                //currentPointedCell.CurrentPiece = null;
                cells[currentTargetCell.CellLocation.x][currentTargetCell.CellLocation.y].SetChessPiece(currentSelectedCell.CurrentPiece);
                currentSelectedCell.SetCellState(ECellState.SELECTED);
                cells[currentSelectedCell.CellLocation.x][currentSelectedCell.CellLocation.y].SetChessPiece(null);

                if (listChessPieces.Count == 1)
                    Game_CTL.Current.GameState = EGameState.GAME_OVER;
            }
            else
                currentSelectedCell.SetCellState(ECellState.SELECTED);

            currentSelectedCell = null;
            currentTargetCell = null;


        }

        //move to empty cell
        //if (currentPointedCell.CurrentPiece == null)
        else
        {
            currentSelectedCell.CurrentPiece.Move(currentTargetCell, false, false, null);
            //unselect and remove moveable
            currentSelectedCell.SetCellState(ECellState.SELECTED);

            //if(attack)
            // BaseGame_CTL.Current.SwitchTurn();


            cells[currentTargetCell.CellLocation.x][currentTargetCell.CellLocation.y].SetChessPiece(currentSelectedCell.CurrentPiece);
            cells[currentSelectedCell.CellLocation.x][currentSelectedCell.CellLocation.y].SetChessPiece(null);
            currentSelectedCell = null;
            currentTargetCell = null;
        }
        Game_CTL.Current.SwitchTurn();
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
                c.transform.parent = this.transform.GetChild(0);
                
                cells[i][j] = c.GetComponent<Cell>();
                cells[i][j].CellLocation = new Vector2Int(i, j);
                if ((i + j)%2 == 0)
                {
                    cells[i][j].CellColor = ECellColor.BLACK;
                } else
                {
                    cells[i][j].CellColor = ECellColor.WHITE;

                }
            }
        }
    }

    [ContextMenu("InitChessPieces")]
    public void InitChessPieces()
    {
        listChessPieces = new List<BaseChessPieces>();

        
        //BaseChessPieces piece;
        List<CP_Infor>  listCP_Infor = new List<CP_Infor>();

        //white
        listCP_Infor.Add(new CP_Infor() { Name = "W_KING", Path = "Models/W_King", X = 3, Y = 0 });
        //listCP_Infor.Add(new CP_Infor() { Name = "W_KING2", Path = "Models/W_King", X = 3, Y = 0 });

        //black
        listCP_Infor.Add(new CP_Infor() { Name = "B_PAWN_1", Path = "Models/B_Pawn", X = 0, Y = 6 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_PAWN_2", Path = "Models/B_Pawn", X = 1, Y = 6 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_PAWN_3", Path = "Models/B_Pawn", X = 2, Y = 6 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_PAWN_4", Path = "Models/B_Pawn", X = 3, Y = 6 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_PAWN_5", Path = "Models/B_Pawn", X = 4, Y = 6 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_PAWN_6", Path = "Models/B_Pawn", X = 5, Y = 6 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_PAWN_7", Path = "Models/B_Pawn", X = 6, Y = 6 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_PAWN_8", Path = "Models/B_Pawn", X = 7, Y = 6 });

        listCP_Infor.Add(new CP_Infor() { Name = "B_ROOK_1", Path = "Models/B_Rook", X = 0, Y = 7 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_ROOK_2", Path = "Models/B_Rook", X = 7, Y = 7 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_CYLINDER_1", Path = "Models/B_Cylinder", X = 1, Y = 7 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_CYLINDER_2", Path = "Models/B_Cylinder", X = 6, Y = 7 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_BISHOP_1", Path = "Models/B_Bishop", X = 2, Y = 7 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_BISHOP_2", Path = "Models/B_Bishop", X = 5, Y = 7 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_KING", Path = "Models/B_King", X = 3, Y = 7 });
        listCP_Infor.Add(new CP_Infor() { Name = "B_QUEEN", Path = "Models/B_Queen", X = 4, Y = 7 });



        foreach (var item in listCP_Infor)
        {   
            GameObject GO = GameObject.Instantiate<GameObject>(ResourcesCTL.Instance.GetGameObject(item.Path));
            GO.transform.parent = this.transform.GetChild(1);

            BaseChessPieces p = GO.GetComponent<BaseChessPieces>();
            p.SetInfor(item);

            if (item == listCP_Infor[0])
                p.Health = 10;
            else
                p.Health = 3;

            listChessPieces.Add(p);

            cells[item.X][item.Y].SetChessPiece(p);


        }

        
        


    }

    public void pawnToQueen(Cell spawnCell)
    {
        CP_Infor newQueen_Infor =  new CP_Infor() { Name = "B_QUEEN", Path = "Models/B_Queen", X = spawnCell.CellLocation.x, Y = spawnCell.CellLocation.y };

        GameObject GO = GameObject.Instantiate<GameObject>(ResourcesCTL.Instance.GetGameObject(newQueen_Infor.Path));
        GO.transform.parent = this.transform.GetChild(1);

        BaseChessPieces p = GO.GetComponent<BaseChessPieces>();
        p.SetInfor(newQueen_Infor);

       p.Health = 3;

        listChessPieces.Add(p);

        cells[newQueen_Infor.X][newQueen_Infor.Y].SetChessPiece(p);
    }

    public Vector3 CalculatePosition(int i, int j)
    {
        //float SIZE = cellPrefab.GetComponent<Cell>().SIZE;
        return startPosition + new Vector3(i * cell_size, 0, j * cell_size);
    }

}
