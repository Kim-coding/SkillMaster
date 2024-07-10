using UnityEngine;

public class BattleState : IState
{
    private PlayerAI player;
    private float attackTimer = 0f;
    private float attackIntervar;

    public BattleState(PlayerAI player)
    {
        this.player = player;
    }

    public void Enter()
    {
        attackIntervar = player.playerBaseStat.attackSpeed;
        //Debug.Log("Enter BattleState");
    }

    public void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer > attackIntervar)
        {
            Attack();
            attackTimer = 0f;
        }
        if (player.currentTarget == null || !player.currentTarget.gameObject.activeInHierarchy)
        {
            player.CheckAndChangeState();
        }
    }

    public void Exit()
    {
        //Debug.Log("Exit BattleState");
    }

    private void Attack()
    {
        if(player.playerSkills.skills.Count > 0)
        {
            player.OnAttack(player.playerSkills.skills[0]);
        }
    }
}
