using UnityEngine;

public class NightState : IGameState
{
    private float timer;

    public void Enter(IGameContext context)
    {
        timer = 60f; // duration in seconds
        Debug.Log("🌒 Night begins! The castle opens...");
    }

    public void Update(IGameContext context)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            context.SetState(new TransitionNightToDayState());
        }
    }

    public void Exit(IGameContext context)
    {
        Debug.Log("Night ends.");
    }
}
