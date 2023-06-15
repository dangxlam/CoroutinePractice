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

    protected List<Cell> moveableCells = new List<Cell>();

    public int Health { get;  set; }

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

    private Vector2 originLocation;

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
            
            Vector3 newPos = offsetPosition + new Vector3(location.x * ChessBoard._instance.CELL_SIZE, 0, location.y * ChessBoard._instance.CELL_SIZE);
            this.transform.DOJump(newPos, 0.7f, 1, 0.5f);
        }
    }

    public void SetInfor(CP_Infor infor)
    {
        Location = new Vector2Int(infor.X, infor.Y);
        this.Infor = infor;
        this.transform.position = offsetPosition + new Vector3(infor.X * ChessBoard._instance.CELL_SIZE, 0, infor.Y * ChessBoard._instance.CELL_SIZE);
    }
   

    public abstract void Move(Cell nextCell);
    public abstract void BeSelected();

    public void UnSelected()
    {
        foreach (var cell in moveableCells)
        {
            cell.SetCellState(ECellState.MOVEABLE);
        }
        moveableCells.Clear();
    }
}
