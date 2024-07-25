using UnityEngine;

public class IdleState : IState
{
    private PlayerAI player;

    public IdleState(PlayerAI player)
    {
        this.player = player;
    }

    public void Enter()
    {
        //Debug.Log("Enter IdleState");
    }

    public void Update()
    {
        Transform target = player.FindClosestMonster();
        if (target != null)
        {
            player.SetTarget(target);
            player.CheckAndChangeState();
        }
    }

    public void Exit()
    {
        //Debug.Log("Exit IdleState");
    }
}
