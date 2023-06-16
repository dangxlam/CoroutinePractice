using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_CTL : MonoBehaviour
{
    public static Game_CTL Current;

    private EGameState _gameState;

    System.Random rd;
    public EPlayer CurrentPlayer { get;  private set; }
    public EGameState GameState
    {
        get 
        {
            return _gameState;
        }

        set 
        { 
            _gameState = value;
            CheckGameState();
        }
    }

    private void Awake()
    {
        Current = this;
        GameState = EGameState.PLAYING;
        CurrentPlayer = EPlayer.WHITE;
        //SwitchTurn();
        
    }

    private void Start()
    {
        //SwitchTurn();
        rd = new System.Random();
    }

    public void SwitchTurn ()
    {
        CurrentPlayer = CurrentPlayer == EPlayer.WHITE? EPlayer.BLACK : EPlayer.WHITE;

        
        if(CurrentPlayer == EPlayer.BLACK && GameState == EGameState.PLAYING)
        {
            //ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)].RandomMove();


            int d = 0;
            BaseChessPieces rdChess = ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)];
            rdChess.BeSelected();
            while (rdChess.moveableCells.Count < 1)
            {
                rdChess.UnSelected();
                rdChess = ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)];
                rdChess.BeSelected();
                if (d++ > 100)
                    break;
            }

            StartCoroutine(RandomBlackMove(rdChess));
            rdChess.UnSelected();
        }
        else if (CurrentPlayer == EPlayer.WHITE && GameState == EGameState.PLAYING)
        {
            BaseChessPieces w_king = ChessBoard._instance.listChessPieces[0];
            ChessBoard._instance.currentSelectedCell = ChessBoard._instance.Cells[w_king.Location.x][w_king.Location.y];
            ChessBoard._instance.currentSelectedCell.SetCellState(ECellState.SELECTED);
            w_king.BeSelected();
        }

    }

    public void CheckGameState()
    {
        if(GameState == EGameState.GAME_OVER) 
        {
            GameOver(CurrentPlayer == EPlayer.WHITE? EPlayer.WHITE : EPlayer.BLACK);
        }

        
    }
   
    public void GameOver(EPlayer winner)
    {
        Debug.Log("Winner: " + winner);
    }

    IEnumerator RandomBlackMove(BaseChessPieces chessPieces)
    {
        //System.Random rd = new System.Random();
        yield return new WaitForSeconds(1f);
        //BaseChessPieces rdChess = ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)];
        //while (rdChess.moveableCells.Count <1)
        //{
        //    rdChess = ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)];
        //}
        //ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)].RandomMove();
        chessPieces.RandomMove();
        //chessPieces.UnSelected();
    }
}
