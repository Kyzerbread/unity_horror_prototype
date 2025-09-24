using System;
using UnityEngine;

public class GameLoopManager : MonoBehaviour, IGameContext
{
    private IGameState currentState;

    public event Action<IGameState> OnStateChanged;

    private void Start()
    {
        SetState(new DayState());
    }

    private void Update()
    {
        currentState?.Update(this);
    }

    public void SetState(IGameState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
        OnStateChanged?.Invoke(currentState);
    }
}