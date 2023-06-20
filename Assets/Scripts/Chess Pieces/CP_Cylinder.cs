using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_Cylinder : BaseChessPieces
{
    public override void BeSelected()
    {
        //8 position
        Vector2Int checkcell = new Vector2Int(Location.x + 2, Location.y - 1);
        if(CheckLocation(checkcell))
        {
            if (ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece == null 
                || ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece.Player != Game_CTL.Current.CurrentPlayer) 
            {
                moveableCells.Add(ChessBoard._instance.Cells[checkcell.x][checkcell.y]);
            }
        }

        checkcell = new Vector2Int(Location.x + 2, Location.y + 1);
        if (CheckLocation(checkcell))
        {
            if (ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece == null
                || ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
            {
                moveableCells.Add(ChessBoard._instance.Cells[checkcell.x][checkcell.y]);
            }
        }

        checkcell = new Vector2Int(Location.x - 2, Location.y + 1);
        if (CheckLocation(checkcell))
        {
            if (ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece == null
                || ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
            {
                moveableCells.Add(ChessBoard._instance.Cells[checkcell.x][checkcell.y]);
            }
        }

        checkcell = new Vector2Int(Location.x - 2, Location.y - 1);
        if (CheckLocation(checkcell))
        {
            if (ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece == null
                || ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
            {
                moveableCells.Add(ChessBoard._instance.Cells[checkcell.x][checkcell.y]);
            }
        }

        checkcell = new Vector2Int(Location.x + 1, Location.y + 2);
        if (CheckLocation(checkcell))
        {
            if (ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece == null
                || ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
            {
                moveableCells.Add(ChessBoard._instance.Cells[checkcell.x][checkcell.y]);
            }
        }

        checkcell = new Vector2Int(Location.x - 1, Location.y + 2);
        if (CheckLocation(checkcell))
        {
            if (ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece == null
                || ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
            {
                moveableCells.Add(ChessBoard._instance.Cells[checkcell.x][checkcell.y]);
            }
        }

        checkcell = new Vector2Int(Location.x + 1, Location.y - 2);
        if (CheckLocation(checkcell))
        {
            if (ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece == null
                || ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
            {
                moveableCells.Add(ChessBoard._instance.Cells[checkcell.x][checkcell.y]);
            }
        }

        checkcell = new Vector2Int(Location.x - 1, Location.y - 2);
        if (CheckLocation(checkcell))
        {
            if (ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece == null
                || ChessBoard._instance.Cells[checkcell.x][checkcell.y].CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
            {
                moveableCells.Add(ChessBoard._instance.Cells[checkcell.x][checkcell.y]);
            }
        }

        foreach (var cell in moveableCells)
        {
            cell.SetCellState(ECellState.MOVEABLE);
        }

    }

    //public override void Move(Cell nextCell, bool goBack)
    //{
        
    //}

    private bool CheckLocation (Vector2Int root)
    {
        if(root.x >= 0 && root.x < 8 && root.y >= 0 && root.y < 8) 
            return true;
        return false;
    }

    public override void RandomMove()
    {
        //BeSelected();

        base.RandomMove();
    }

}
