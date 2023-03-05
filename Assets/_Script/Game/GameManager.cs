/* using System.Collections; */
/* using System.Collections.Generic; */
using System;
using UnityEngine;

public enum State
{
    Pause,
    Lose,
    Play
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public State state;
    /* public static event Action<State> OnGameStateChanged; */
    public static event Action OnResetGame;

    void Awake()
    {
        Instance = this;
        /* changeGameState(State.Play); */
    }

    /* void Start() */
    /* { */
    /*     changeGameState(State.Play); */
    /* } */

    void changeGameState(State newState)
    {
        this.state = newState;

        switch (newState)
        {
            case State.Lose:
                OnResetGame.Invoke();
                break;
            default:
                break;
        }

        /* OnGameStateChanged.Invoke(newState); */
    }

    public void lose()
    {
        changeGameState(State.Lose);
    }
}
