using UnityEngine;

public class DayState : IGameState
{
    private float timer;

    public void Enter(IGameContext context)
    {
        timer = 60f; // example day duration
        Debug.Log("Day begins!");
    }

    public void Update(IGameContext context)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            context.SetState(new TransitionDayToNightState());
        }
    }

    public void Exit(IGameContext context)
    {
        Debug.Log("Day ends!");
    }
}
