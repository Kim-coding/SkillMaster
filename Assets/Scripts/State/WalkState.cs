using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WalkState : IState
{
    private PlayerAI player;
    private float pathUpdateTimer = 0f;
    private float pathUpdateInterval = 0.5f;
    public WalkState(PlayerAI player)
    {
        this.player = player;
    }

    public void Enter()
    {
        //Debug.Log("Enter WalkState");
        player.UpdatePath();
        player.Animator.SetBool("Moving", true);
    }

    public void Update()
    {
        pathUpdateTimer += Time.deltaTime;

        if(!player.IsInAttackRange())
        {
            player.currentTarget = player.FindClosestMonster();
        }

        if (pathUpdateTimer >= pathUpdateInterval)
        {
            pathUpdateTimer = 0f;
            player.UpdatePath();
        }

        player.MoveAlongPath();
        if(player.IsInAttackRange())
        {
            player.CheckAndChangeState();
        }
    }

    public void Exit()
    {
        player.Animator.SetBool("Moving", false);
        //Debug.Log("Exit WalkState");
    }


}
