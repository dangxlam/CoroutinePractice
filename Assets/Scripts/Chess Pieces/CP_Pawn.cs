using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_Pawn : BaseChessPieces
{
    private bool isFirstMoved = true;
    public override void BeSelected()
    {
        //Show Moveable cell
        switch (this.player)
        {
            case EPlayer.BLACK:
                BeSelectedBlack();
                break;
            case EPlayer.WHITE:
                BeSelectedWhite();
                break;
            default:
                break;
        }

    }

    private void BeSelectedWhite()
    {
        
    }

    private void BeSelectedBlack()
    {
        if (Location.y > 0)
        {
            Cell frontCellCheck = ChessBoard._instance.Cells[(int)Location.x][(int)Location.y - 1];
            if (frontCellCheck.CurrentPiece == null)
            {
                moveableCells.Add(frontCellCheck);

                if (isFirstMoved)
                {
                    //ChessBoard._instance.Cells[(int)Location.x][(int)Location.y - 1].SetCellState(ECellState.POINTED);
                    frontCellCheck = ChessBoard._instance.Cells[(int)Location.x][(int)Location.y - 2];

                    if (frontCellCheck.CurrentPiece == null)
                    {
                        moveableCells.Add(frontCellCheck);
                    }


                    //ChessBoard._instance.Cells[(int)Location.x][(int)Location.y - 2].SetCellState(ECellState.MOVEABLE);
                }
            }


            //moveableCells.Add(ChessBoard._instance.Cells[(int)Location.x][(int)Location.y - 1]);
            //ChessBoard._instance.Cells[(int)Location.x][(int)Location.y - 1].SetCellState(ECellState.MOVEABLE);


            if (Location.x > 0)
            {
                Cell leftCellCheck = ChessBoard._instance.Cells[(int)Location.x - 1][(int)Location.y - 1];
                if (leftCellCheck.CurrentPiece != null && leftCellCheck.CurrentPiece.Player == EPlayer.WHITE)
                {
                    moveableCells.Add(leftCellCheck);

                }
                //ChessBoard._instance.Cells[(int)Location.x - 1][(int)Location.y - 1].SetCellState(ECellState.MOVEABLE);
            }

            if (Location.x < 7)
            {
                Cell rightCellCheck = ChessBoard._instance.Cells[(int)Location.x + 1][(int)Location.y - 1];
                if (rightCellCheck.CurrentPiece != null && rightCellCheck.CurrentPiece.Player == EPlayer.WHITE)
                {
                    moveableCells.Add(rightCellCheck);

                }
                //ChessBoard._instance.Cells[(int)Location.x + 1][(int)Location.y - 1].SetCellState(ECellState.MOVEABLE);
            }

            foreach (var cell in moveableCells)
            {
                cell.SetCellState(ECellState.MOVEABLE);
            }
        }
        
    }

    

    public override void Move(Cell nextCell)
    {
        isFirstMoved = false;
        Location = nextCell.CellLocation;
        BaseGame_CTL.Current.SwitchTurn();

    }

    public  void UnSelectedNothing()
    {
        foreach (var cell in moveableCells)
        {
            cell.SetCellState(ECellState.MOVEABLE);
        }

        //if (isFirstMoved)
        //{
        //    //ChessBoard._instance.Cells[(int)Location.x][(int)Location.y - 1].SetCellState(ECellState.POINTED);
        //    ChessBoard._instance.Cells[(int)Location.x][(int)Location.y - 2].SetCellState(ECellState.MOVEABLE);
        //}


        //ChessBoard._instance.Cells[(int)Location.x][(int)Location.y - 1].SetCellState(ECellState.MOVEABLE);


        //if (Location.x > 0)
        //{
        //    ChessBoard._instance.Cells[(int)Location.x - 1][(int)Location.y - 1].SetCellState(ECellState.MOVEABLE);

        //}

        //if (Location.x < 7)
        //{
        //    ChessBoard._instance.Cells[(int)Location.x + 1][(int)Location.y - 1].SetCellState(ECellState.MOVEABLE);
        //}


    }
}
