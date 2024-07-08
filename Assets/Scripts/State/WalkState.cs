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
    }

    public void Update()
    {
        pathUpdateTimer += Time.deltaTime;

        if (pathUpdateTimer >= pathUpdateInterval)
        {
            pathUpdateTimer = 0f;
            player.UpdatePath();
        }

        MoveAlongPath();
        if(player.IsInAttackRange())
        {
            player.CheckAndChangeState();
        }
    }

    public void Exit()
    {
        //Debug.Log("Exit WalkState");
    }

    private void MoveAlongPath()
    {
        if (player.path != null && player.currentPathIndex < player.path.Count && player.currentTarget != null)
        {
            if (Vector3.Distance(player.transform.position, player.currentTarget.position) <= player.attackRange)
                return;

            Vector3 targetPosition = player.path[player.currentPathIndex].worldPosition;
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, Time.deltaTime * player.speed);

            if (Vector3.Distance(player.transform.position, targetPosition) < 0.1f)
            {
                player.currentPathIndex++;
            }
        }
    }
}
