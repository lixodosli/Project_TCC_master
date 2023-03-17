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
            Debug.Log("Estou tentando mudar de estado, porém estou indo para um estado que já me encontro.");
            return;
        }

        PreviousState = State;
        State = state;
        Debug.Log("Estrei no estado <" + State.ToString() + ">.");
        OnStateChange?.Invoke(State);
    }
}