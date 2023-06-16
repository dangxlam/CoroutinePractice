using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_Rook : BaseChessPieces
{
    public override void BeSelected()
    {
        //int i = Location.x; int j = Location.y;
        for (int i = Location.x - 1; i >= 0; i--)
        {
            Cell checkCell = ChessBoard._instance.Cells[i][(int)Location.y];
            if (checkCell.CurrentPiece == null)
            {
                moveableCells.Add(checkCell);

            }
            else
            {
                if (checkCell.CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
                {
                    moveableCells.Add(checkCell);
                }
                
                break;
            }
        }

        for (int i = Location.x + 1; i < 8; i++)
        {
            Cell checkCell = ChessBoard._instance.Cells[i][(int)Location.y];
            if (checkCell.CurrentPiece == null)
            {
                moveableCells.Add(checkCell);

            }
            else
            {
                if (checkCell.CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
                {
                    moveableCells.Add(checkCell);
                }
                
                break;
            }
        }

        for (int i = Location.y - 1; i >= 0; i--)
        {
            Cell checkCell = ChessBoard._instance.Cells[Location.x][i];
            if (checkCell.CurrentPiece == null)
            {
                moveableCells.Add(checkCell);

            }
            else
            {
                if (checkCell.CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
                {
                    moveableCells.Add(checkCell);
                }
                
                break;
            }
        }

        for (int i = Location.y + 1; i < 8; i++)
        {
            Cell checkCell = ChessBoard._instance.Cells[Location.x][i];
            if (checkCell.CurrentPiece == null)
            {
                moveableCells.Add(checkCell);

            }
            else
            {
                if (checkCell.CurrentPiece.Player != Game_CTL.Current.CurrentPlayer)
                {
                    moveableCells.Add(checkCell);
                }
                
                break;
            }
        }

        foreach (var cell in moveableCells)
        {
            cell.SetCellState(ECellState.MOVEABLE);
        }
    }

    public override void RandomMove()
    {
        BeSelected();
        
        base.RandomMove();
    }




}
