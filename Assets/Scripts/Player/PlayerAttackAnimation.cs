using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimation : MonoBehaviour
{

    public PlayerAI player;

    public void OnAttackAnimationComplete()
    {
        player.PlayerStateMachine.battleState.OnAttackAnimationComplete();
        player.PlayerStateMachine.battleState.onSkill = false;

    }
}
