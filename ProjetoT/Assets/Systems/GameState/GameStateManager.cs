using UnityEngine;

public enum GameState
{
    World_Free,
    Inventory,
    Cutscene,
    Pause
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Game;

    public delegate void GameStateCallback(GameState state);
    public GameStateCallback OnStateChange;

    public GameState State { get; private set; }
    public GameState PreviousState { get; private set; }

    private void Awake()
    {
        Game = this;
    }

    private void Start()
    {
        RaiseChangeGameState(GameState.World_Free);
    }

    public void RaiseChangeGameState(GameState state)
    {
        if (state == State)
        {
            return;
        }

        PreviousState = State;
        State = state;
        OnStateChange?.Invoke(State);
    }
}