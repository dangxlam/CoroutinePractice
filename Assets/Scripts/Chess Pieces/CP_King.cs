using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_King : BaseChessPieces
{
    

    public override void BeSelected()
    {
        List<Vector2Int> listLocation = new List<Vector2Int>();

        Vector2Int cellLocationToCheck = this.Location + new Vector2Int(1, 1);
        if (CheckLocation.Check(cellLocationToCheck))
            listLocation.Add(cellLocationToCheck);


        cellLocationToCheck = this.Location + new Vector2Int(1, -1);
        if (CheckLocation.Check(cellLocationToCheck))
            listLocation.Add(cellLocationToCheck);



        cellLocationToCheck = this.Location + new Vector2Int(-1, 1);
        if (CheckLocation.Check(cellLocationToCheck))
            listLocation.Add(cellLocationToCheck);


        cellLocationToCheck = this.Location + new Vector2Int(-1, -1);
        if (CheckLocation.Check(cellLocationToCheck))
            listLocation.Add(cellLocationToCheck);

        cellLocationToCheck = this.Location + new Vector2Int(0, 1);
        if (CheckLocation.Check(cellLocationToCheck))
            listLocation.Add(cellLocationToCheck);



        cellLocationToCheck = this.Location + new Vector2Int(0, -1);
        if (CheckLocation.Check(cellLocationToCheck))
            listLocation.Add(cellLocationToCheck);



        cellLocationToCheck = this.Location + new Vector2Int(-1, 0);
        if (CheckLocation.Check(cellLocationToCheck))
            listLocation.Add(cellLocationToCheck);


        cellLocationToCheck = this.Location + new Vector2Int(1, 0);
        if (CheckLocation.Check(cellLocationToCheck))
            listLocation.Add(cellLocationToCheck);


        foreach (var item in listLocation)
        {
            Cell checkCell = ChessBoard._instance.Cells[item.x][item.y];

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
