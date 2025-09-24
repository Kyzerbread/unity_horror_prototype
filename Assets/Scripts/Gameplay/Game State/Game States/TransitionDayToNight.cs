using UnityEngine;

public class TransitionDayToNightState : IGameState
{
    private float timer;

    public void Enter(IGameContext context)
    {
        timer = 5f;
        Debug.Log("Transition: dusk falls...");
    }

    public void Update(IGameContext context)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            context.SetState(new NightState());
        }
    }

    public void Exit(IGameContext context)
    {
        Debug.Log("The night takes over.");
    }
}
