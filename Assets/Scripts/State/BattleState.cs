using System.Linq;
using UnityEngine;

public class BattleState : IState
{
    private PlayerAI player;
    private float attackTimer = 0f;

    public BattleState(PlayerAI player)
    {
        this.player = player;
    }

    public void Enter()
    {
        //Debug.Log("Enter BattleState");
    }

    public void Update()
    {
        if (!player.onSkill)
        {
            attackTimer += Time.deltaTime;
        }

        if(attackTimer > player.characterStat.cooldown && !player.onSkill)
        {
            attackTimer = 0f;
            Attack();
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
        player.onSkill = true;
        player.playerSkills.SetList();
        int skillType = player.playerSkills.castingList[0].skillType;
        int[] attackMagicSkillTypes = { 1, 2, 5, 7, 9 };
        int[] skillMagicSkillTypes = { 3, 4, 6, 11};
        int[] attackNormalSkillTypes = { 8 , 10};

        if (attackNormalSkillTypes.Contains(skillType))
        {
            player.Animator.SetTrigger("Attack_Normal");
        }
        else if (attackMagicSkillTypes.Contains(skillType))
        {
            player.Animator.SetTrigger("Attack_Magic");
        }
        else
        {
            player.Animator.SetTrigger("Skill_Magic");
        }
    }

    public void OnAttackAnimationComplete()
    {
        player.OnAttack(player.playerSkills.skills[0]);
        player.onSkill = false;
    }
}
