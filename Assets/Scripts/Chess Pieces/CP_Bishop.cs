using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_Bishop : BaseChessPieces
{
    public override void BeSelected()
    {
        // check X
        Vector2Int cellLocationToCheck = this.Location + new Vector2Int(1,1);
        while (CheckLocation.Check(cellLocationToCheck))
        {
            Cell checkCell = ChessBoard._instance.Cells[cellLocationToCheck.x][cellLocationToCheck.y];

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

            cellLocationToCheck = new Vector2Int(cellLocationToCheck.x + 1, cellLocationToCheck.y + 1);
            


        }

        cellLocationToCheck = this.Location + new Vector2Int(1, -1);
        while (CheckLocation.Check(cellLocationToCheck))
        {
            Cell checkCell = ChessBoard._instance.Cells[cellLocationToCheck.x][cellLocationToCheck.y];

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

            cellLocationToCheck = new Vector2Int(cellLocationToCheck.x + 1, cellLocationToCheck.y - 1);



        }

        cellLocationToCheck = this.Location + new Vector2Int(-1, 1);
        while (CheckLocation.Check(cellLocationToCheck))
        {
            Cell checkCell = ChessBoard._instance.Cells[cellLocationToCheck.x][cellLocationToCheck.y];

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

            cellLocationToCheck = new Vector2Int(cellLocationToCheck.x - 1, cellLocationToCheck.y + 1);



        }

        cellLocationToCheck = this.Location + new Vector2Int(-1, -1);
        while (CheckLocation.Check(cellLocationToCheck))
        {
            Cell checkCell = ChessBoard._instance.Cells[cellLocationToCheck.x][cellLocationToCheck.y];

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

            cellLocationToCheck = new Vector2Int(cellLocationToCheck.x - 1, cellLocationToCheck.y - 1);



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
