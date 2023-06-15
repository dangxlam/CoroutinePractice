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
    private Cell currentSelectedCell = null;

    private List<BaseChessPieces> listChessPieces;

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
    }

    private void Update()
    {

        //cells[1][2].SetCellState(ECellState.SELECTED);

        if (BaseGame_CTL.Current.GameState == EGameState.PLAYING)
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
            if (currentPointedCell != null)
            {
                //Check if can move
                if(currentPointedCell.CellState == ECellState.MOVEABLE)
                {
                    //check if have enemy
                    if(currentPointedCell.CurrentPiece != null)
                    {
                        currentPointedCell.CurrentPiece.Health--;
                        Debug.Log("CHealth: " + currentPointedCell.CurrentPiece.Health);
                        if (currentPointedCell.CurrentPiece.Health < 1)
                        {
                            Destroy(currentPointedCell.CurrentPiece);
                            currentPointedCell.CurrentPiece = null;
                            //currentSelectedCell
                        }

                        
                    }

                    //move to empty cell
                    if (currentPointedCell.CurrentPiece == null)
                    {
                        currentSelectedCell.CurrentPiece.Move(currentPointedCell);
                        currentSelectedCell.SetCellState(ECellState.SELECTED);
                        currentSelectedCell.CurrentPiece.UnSelected();


                        cells[currentPointedCell.CellLocation.x][currentPointedCell.CellLocation.y].SetChessPiece(currentSelectedCell.CurrentPiece);
                        cells[currentSelectedCell.CellLocation.x][currentSelectedCell.CellLocation.y].SetChessPiece(null);
                        currentSelectedCell = null;

                    }



                } else
                {
                    //select a cell
                    if (currentSelectedCell != null || currentSelectedCell == currentPointedCell)
                    {
                        currentSelectedCell.SetCellState(ECellState.SELECTED);
                        currentSelectedCell.CurrentPiece.UnSelected();
                        currentSelectedCell = null;

                    }
                    //if (currentSelectedCell == currentPointedCell)
                    //{
                    //    //currentSelectedCell = currentPointedCell;
                    //    //double selected => normal 
                    //    currentSelectedCell.SetCellState(ECellState.SELECTED);
                    //    currentSelectedCell.CurrentPiece.UnSelected();
                    //    currentSelectedCell = null;
                    //} else
                    if (currentSelectedCell != currentPointedCell && currentPointedCell.CurrentPiece!= null && currentPointedCell.CurrentPiece.Player == BaseGame_CTL.Current.CurrentPlayer)
                    {
                        if (currentPointedCell.CurrentPiece != null)
                        {
                            currentSelectedCell = currentPointedCell;
                            currentSelectedCell.SetCellState(ECellState.SELECTED);
                            currentSelectedCell.CurrentPiece.BeSelected();
                        }

                    }

                    //Debug.Log(currentSelectedCell.CurrentPiece.Infor.Name);
                    //if(currentPointedCell.CurrentPiece != null)
                    //currentSelectedCell.CurrentPiece.BeSelected();
                }


            }
            
                
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
        listCP_Infor.Add(new CP_Infor() { Name = "W_KING", Path = "Models/W_King", X = 3, Y = 5 });

        //black
        listCP_Infor.Add(new CP_Infor() { Name = "B_PAWN_1", Path = "Models/B_Pawn", X = 0, Y = 6});
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
        listCP_Infor.Add(new CP_Infor() { Name = "B_QUEEN", Path = "Models/B_Queen", X = 4, Y = 5 });



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

        
        

        //GameObject w_king_GO = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Models/W_King"));
        //w_king_GO.transform.parent = this.transform.GetChild(1);
        //CP_King w_king = w_king_GO.GetComponent<CP_King>();

        //listChessPieces.Add(w_king);
        //w_king.SetOriginLocation(3, 0);


        //GameObject b_rook1_GO = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Models/B_Rook"));
        //GameObject b_rook2_GO = GameObject.Instantiate<GameObject>(b_rook1_GO);

        //GameObject b_cylinder1_GO = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Models/B_Cylinder"));
        //GameObject b_cylinder2_GO = GameObject.Instantiate<GameObject>(b_cylinder1_GO);

        //GameObject b_bishop1_GO = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Models/B_Bishop"));
        //GameObject b_bishop2_GO = GameObject.Instantiate<GameObject>(b_bishop1_GO);

        //GameObject b_king_GO = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Models/B_King"));

        //GameObject b_queen_GO = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Models/B_Queen"));

        //GameObject b_pawn1_GO = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Models/B_Pawn"));
        //GameObject b_pawn2_GO = GameObject.Instantiate<GameObject>(b_pawn1_GO);
        //GameObject b_pawn3_GO = GameObject.Instantiate<GameObject>(b_pawn1_GO);
        //GameObject b_pawn4_GO = GameObject.Instantiate<GameObject>(b_pawn1_GO);
        //GameObject b_pawn5_GO = GameObject.Instantiate<GameObject>(b_pawn1_GO);
        //GameObject b_pawn6_GO = GameObject.Instantiate<GameObject>(b_pawn1_GO);
        //GameObject b_pawn7_GO = GameObject.Instantiate<GameObject>(b_pawn1_GO);
        //GameObject b_pawn8_GO = GameObject.Instantiate<GameObject>(b_pawn1_GO);


        //b_rook1_GO.transform.parent = this.transform.GetChild(1);
        //b_rook2_GO.transform.parent = this.transform.GetChild(1);

        //b_cylinder1_GO.transform.parent = this.transform.GetChild(1);
        //b_cylinder2_GO.transform.parent = this.transform.GetChild(1);

        //b_bishop1_GO.transform.parent = this.transform.GetChild(1);
        //b_bishop2_GO.transform.parent = this.transform.GetChild(1);

        //b_queen_GO.transform.parent = this.transform.GetChild(1);
        //b_king_GO.transform.parent = this.transform.GetChild(1);

        //b_pawn1_GO.transform.parent = this.transform.GetChild(1);
        //b_pawn2_GO.transform.parent = this.transform.GetChild(1);
        //b_pawn3_GO.transform.parent = this.transform.GetChild(1);
        //b_pawn4_GO.transform.parent = this.transform.GetChild(1);
        //b_pawn5_GO.transform.parent = this.transform.GetChild(1);
        //b_pawn6_GO.transform.parent = this.transform.GetChild(1);
        //b_pawn7_GO.transform.parent = this.transform.GetChild(1);
        //b_pawn8_GO.transform.parent = this.transform.GetChild(1);

        //CP_Rook b_Rook1 = b_rook1_GO.GetComponent<CP_Rook>();
        //CP_Rook b_Rook2 = b_rook2_GO.GetComponent<CP_Rook>();

        //CP_Cylinder b_Cylinder1 = b_cylinder1_GO.GetComponent<CP_Cylinder>();
        //CP_Cylinder b_Cylinder2 = b_cylinder2_GO.GetComponent<CP_Cylinder>();

        //CP_Bishop b_Bishop1 = b_bishop1_GO.GetComponent<CP_Bishop>();
        //CP_Bishop b_Bishop2 = b_bishop2_GO.GetComponent<CP_Bishop>();

        //CP_King b_King = b_king_GO.GetComponent<CP_King>();
        //CP_Queen b_Queen = b_queen_GO.GetComponent<CP_Queen>();

        //CP_Pawn b_Pawn1 = b_pawn1_GO.GetComponent<CP_Pawn>();
        //CP_Pawn b_Pawn2 = b_pawn2_GO.GetComponent<CP_Pawn>();
        //CP_Pawn b_Pawn3 = b_pawn3_GO.GetComponent<CP_Pawn>();
        //CP_Pawn b_Pawn4 = b_pawn4_GO.GetComponent<CP_Pawn>();
        //CP_Pawn b_Pawn5 = b_pawn5_GO.GetComponent<CP_Pawn>();
        //CP_Pawn b_Pawn6 = b_pawn6_GO.GetComponent<CP_Pawn>();
        //CP_Pawn b_Pawn7 = b_pawn7_GO.GetComponent<CP_Pawn>();
        //CP_Pawn b_Pawn8 = b_pawn8_GO.GetComponent<CP_Pawn>();

        //listChessPieces.Add(b_Rook1);
        //listChessPieces.Add(b_Rook2);

        //listChessPieces.Add(b_Cylinder1);
        //listChessPieces.Add(b_Cylinder2);

        //listChessPieces.Add(b_Bishop1);
        //listChessPieces.Add(b_Bishop2);

        //listChessPieces.Add(b_King);
        //listChessPieces.Add(b_Queen);

        //listChessPieces.Add(b_Pawn1);
        //listChessPieces.Add(b_Pawn2);
        //listChessPieces.Add(b_Pawn3);
        //listChessPieces.Add(b_Pawn4);
        //listChessPieces.Add(b_Pawn5);
        //listChessPieces.Add(b_Pawn6);
        //listChessPieces.Add(b_Pawn7);
        //listChessPieces.Add(b_Pawn8);

        //b_Rook1.SetOriginLocation(0, 7);
        //b_Rook1.SetOriginLocation(7, 7);

        //b_Cylinder1.SetOriginLocation(1, 7);
        //b_Cylinder2.SetOriginLocation(6, 7);

        //b_Bishop1.SetOriginLocation(2, 7);
        //b_Bishop1.SetOriginLocation(5, 7);

        //b_King.SetOriginLocation(3, 7);
        //b_Queen.SetOriginLocation(4, 7);

        //b_Pawn1.SetOriginLocation(0, 6);
        //b_Pawn2.SetOriginLocation(1, 6);
        //b_Pawn3.SetOriginLocation(2, 6);
        //b_Pawn4.SetOriginLocation(3, 6);
        //b_Pawn5.SetOriginLocation(4, 6);
        //b_Pawn6.SetOriginLocation(5, 6);
        //b_Pawn7.SetOriginLocation(6, 6);
        //b_Pawn8.SetOriginLocation(7, 6);

    }

    public Vector3 CalculatePosition(int i, int j)
    {
        //float SIZE = cellPrefab.GetComponent<Cell>().SIZE;
        return startPosition + new Vector3(i * cell_size, 0, j * cell_size);
    }

}
