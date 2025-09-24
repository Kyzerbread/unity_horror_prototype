public interface IGameContext
{
    void SetState(IGameState newState);
}

public interface IGameState
{
    void Enter(IGameContext context);   // called once when state starts
    void Update(IGameContext context);  // called every frame
    void Exit(IGameContext context);    // called once when leaving
}