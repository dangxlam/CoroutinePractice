using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame_CTL : MonoBehaviour
{
    public static BaseGame_CTL Current;

    private EGameState _gameState;
    public EGameState GameState
    {
        get 
        {
            return _gameState;
        }

        set { _gameState = value;}
    }

    private void Awake()
    {
        Current = this;
        GameState = EGameState.PLAYING;
    }

   
}
