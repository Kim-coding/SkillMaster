using System;
using Unity.VisualScripting;

public class StateMachine
{
    public IState currentState { get; private set; }

    public WalkState walkState;
    public IdleState idleState;
    public BattleState battleState;

    public event Action<IState> stateChanged;

    public StateMachine(PlayerAI player)
    {
        walkState = new WalkState(player);
        idleState = new IdleState(player);
        battleState = new BattleState(player);
    }

    public void Initialize(IState startState)
    {
        currentState = startState;
        currentState.Enter();

        stateChanged?.Invoke(startState);
    }

    public void ChangState(IState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();

        stateChanged?.Invoke(newState);
    }

    public void Update()
    {
        if(currentState != null) 
        {
            currentState.Update();
        }
    }
}
