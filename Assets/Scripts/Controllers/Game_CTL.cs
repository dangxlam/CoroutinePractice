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
        Debug.Log("Current turn: " +  CurrentPlayer);

        
        if(CurrentPlayer == EPlayer.BLACK && GameState == EGameState.PLAYING)
        {
            //ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)].RandomMove();


            StartCoroutine(RandomBlackMove());


            /*
            while (rdChess.moveableCells.Count < 1)
            {
                rdChess.UnSelected();
                rdChess = ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)];
                rdChess.BeSelected();
                if (d++ > ChessBoard._instance.listChessPieces.Count - 2) ;
                    break;
            }

            StartCoroutine(RandomBlackMove(rdChess));
            rdChess.UnSelected();*/
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

    IEnumerator RandomBlackMove()
    {
        //System.Random rd = new System.Random();
        yield return new WaitForSeconds(1f);
        // yield return null;
        //BaseChessPieces rdChess = ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)];
        //while (rdChess.moveableCells.Count <1)
        //{
        //    rdChess = ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)];
        //}
        //ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)].RandomMove();
        //chessPieces.RandomMove();
        //chessPieces.UnSelected();
        int d = 1;
        int chek = 0;
        // BaseChessPieces rdChess = ChessBoard._instance.listChessPieces[rd.Next(1, ChessBoard._instance.listChessPieces.Count)];
        //rdChess.BeSelected();
        //ChessBoard._instance.listChessPieces.Count
        while (d < ChessBoard._instance.listChessPieces.Count)
        {
            
            foreach (BaseChessPieces chess in ChessBoard._instance.listChessPieces)
            {
                //Debug.Log("reest");
                if (chess == ChessBoard._instance.listChessPieces[0])
                    continue;
                chess.BeSelected();
                if (!chess.IsMoved && chess.moveableCells.Count > 0)
                {
                    //StartCoroutine(RandomBlackMove(chess));
                    chess.RandomMove();
                    
                    chess.IsMoved = true;
                    
                    d++;
                    yield return new WaitForSeconds(0.7f);

                    Debug.Log("Co chay ko + D = " + d  + " chek  = " + chek + " " + ChessBoard._instance.listChessPieces.Count);

                   
                }
                //chess.UnSelected();
                 //chess.BeSelected();

               
            }

            chek++;
            if (chek > 200)
                break;
        }

        foreach (BaseChessPieces chess in ChessBoard._instance.listChessPieces)
        {
            if (chess == ChessBoard._instance.listChessPieces[0])
                continue;

            chess.IsMoved = false;

        }

        SwitchTurn();
    }
}
