using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_Queen : BaseChessPieces
{
    public override void BeSelected()
    {
        //move like bishop
        bishopDirection();

        //move like rook
        rookDirection();
        


        foreach (var cell in moveableCells)
        {
            cell.SetCellState(ECellState.MOVEABLE);
        }
    }

    private void bishopDirection()
    {
        Vector2Int cellLocationToCheck = this.Location + new Vector2Int(1, 1);
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
    }

    private void rookDirection ()
    {
        //Move like rook
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
    }

    public override void RandomMove()
    {
        BeSelected();

        base.RandomMove();
    }


}
