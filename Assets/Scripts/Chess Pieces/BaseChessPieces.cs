using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public abstract class BaseChessPieces : MonoBehaviour
{
    [SerializeField]
    private Vector3 offsetPosition;
    public CP_Infor Infor { get; private set; }

    public List<Cell> moveableCells = new List<Cell>();

    private int health;
    public int Health { 
        get { return health; }
        set 
        {
            health = value;
            if (health < 1)
            {
                GameObject.Destroy(gameObject);
            }
        } 
    }

    private bool isMove = false;
    public bool IsMoved { 
        get
        {
            return isMove;
        } 
        set { isMove = value; }
    }

    [SerializeField]
    protected EPlayer player;

    public EPlayer Player {
        get
        {
            return player;
        }
        protected set
        {
            player = value;
        }
    }

    private Vector2Int originLocation;

    [SerializeField]
    Vector2Int location;

    public Vector2Int Location {
        get
        {
            return location;
        }
        set
        {
            location = value;
            //StartCoroutine(Move());
            //Vector3 newPos = offsetPosition + new Vector3(location.x * ChessBoard._instance.CELL_SIZE, 0, location.y * ChessBoard._instance.CELL_SIZE);
            //this.transform.DOJump(newPos, 0.7f, 1, 0.3f);
        }
    }

    public void SetInfor(CP_Infor infor)
    {
        Location = new Vector2Int(infor.X, infor.Y);
        this.Infor = infor;
        this.transform.position = offsetPosition + new Vector3(infor.X * ChessBoard._instance.CELL_SIZE, 0, infor.Y * ChessBoard._instance.CELL_SIZE);
    }


    public virtual void Move(Cell nextCell, bool goBack, bool evolve, Cell evolveCell)
    {
        //originLocation = Location;
        ////Move
        //Location = nextCell.CellLocation;
        ////Switch Turn
        //BaseGame_CTL.Current.SwitchTurn();

        //UnSelected();

        //if (goBack)
        //{
        //    Location = originLocation
        //}
        StartCoroutine(CoMove(nextCell, goBack, evolve, evolveCell));

    }

    public virtual void RandomMove()
    {
        
        

        bool isAttack = false;

        foreach (Cell cell in moveableCells)
        {
            if(cell.CurrentPiece != null)
            {
                if (--cell.CurrentPiece.Health == 0)
                {
                    string deadPiece = cell.CurrentPiece.Infor.Name;
                    ChessBoard._instance.listChessPieces.Remove(cell.CurrentPiece);
                    //Move(cell, false);
                    ChessBoard._instance.Cells[Location.x][Location.y].SetChessPiece(null);
                    
                    Move(cell, false, false, null);
                    cell.SetChessPiece(this);
                    Debug.Log("DEad: " + deadPiece);
                    if (deadPiece.Equals("W_KING"))
                        Game_CTL.Current.GameState = EGameState.GAME_OVER;


                }
                else
                {
                    Debug.Log("My Health: " + (cell.CurrentPiece.Health));
                    //cell.CurrentPiece.Health--;
                    Move(cell, true, false,null);
                }
                isAttack = true;
                break;
            }
        }

        if(!isAttack)
        {
            System.Random rd = new System.Random();


            Cell randomCell = moveableCells[rd.Next(0, moveableCells.Count)];
            //Debug.Log("Name: " + this.name);
            if(this.name.Equals("B_Pawn(Clone)") && randomCell.CellLocation.y == 0)
            {
                Move(randomCell, false, true, randomCell);
                //ChessBoard._instance.pawnToQueen(randomCell);
                //this.Health = 0;
            } else
            {
                
                Move(randomCell, false,false,null);
                ChessBoard._instance.Cells[Location.x][Location.y].SetChessPiece(null);
                randomCell.SetChessPiece(this);
            }

            
        }
        //UnSelected();
        //if (randomCell.CurrentPiece == null )
        //{
        //    ChessBoard._instance.Cells[Location.x][Location.y].SetChessPiece(null);
        //    Move(randomCell, false);
        //    randomCell.SetChessPiece(this);
        //} else
        //{
        //    if (--randomCell.CurrentPiece.Health == 0)
        //    {
        //        string deadPiece = randomCell.CurrentPiece.Infor.Name;
        //        ChessBoard._instance.listChessPieces.Remove(randomCell.CurrentPiece);
        //        Move(randomCell, false);
        //        Debug.Log("DEad: " + deadPiece);
        //        if (deadPiece.Equals("W_KING") || deadPiece.Equals("B_KING"))
        //            Game_CTL.Current.GameState = EGameState.GAME_OVER;

        //    }
        //    else
        //    {
        //        randomCell.CurrentPiece.Health--;
        //        Move(randomCell, true);
        //    }
            
        //}

        //this.UnSelected();
    }

    public abstract void BeSelected();

    public void UnSelected()
    {
        //Debug.Log("unselected");
        foreach (var cell in moveableCells)
        {
            cell.SetCellState(ECellState.MOVEABLE);
            //Debug.Log("bo 1 2 3");
        }
        moveableCells.Clear();
    }

    IEnumerator CoMove(Cell nextCell, bool goBack, bool evolve, Cell evolveCell)
    {
        UnSelected();
        Vector3 oldPos = offsetPosition + new Vector3(location.x * ChessBoard._instance.CELL_SIZE, 0, location.y * ChessBoard._instance.CELL_SIZE);
        originLocation = Location;
        Location = nextCell.CellLocation;
        Vector3 newPos = offsetPosition + new Vector3(location.x * ChessBoard._instance.CELL_SIZE, 0, location.y * ChessBoard._instance.CELL_SIZE);
        this.transform.DOJump(newPos, 0.7f, 1, 0.3f);
        //Game_CTL.Current.SwitchTurn();
        
        yield return new WaitForSeconds(0.3f);
        if (goBack) 
        {
            Location = originLocation;
            this.transform.DOJump(oldPos, 0.7f, 1, 0.3f);
        }
        yield return new WaitForSeconds(0.5f);
        if (evolve)
        {
            ChessBoard._instance.pawnToQueen(evolveCell);
            this.Health = 0;
        }
        //Game_CTL.Current.SwitchTurn();
    }
}
