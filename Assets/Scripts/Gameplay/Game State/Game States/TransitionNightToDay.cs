using UnityEngine;

public class TransitionNightToDayState : IGameState
{
    private float timer;

    public void Enter(IGameContext context)
    {
        timer = 5f;
        Debug.Log("Transition: dawn breaks...");
    }

    public void Update(IGameContext context)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            context.SetState(new DayState());
        }
    }

    public void Exit(IGameContext context)
    {
        Debug.Log("The sun rises, a new day begins.");
    }
}