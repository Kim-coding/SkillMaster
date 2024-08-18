using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarUpdate : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        if(defender.GetComponent<PlayerAI>() != null)
        {
            var player = defender.GetComponent<PlayerAI>();
            player.characterStat.UpdateHpBar();
        }

        if(defender.GetComponent<BossAI>() != null)
        {
            var player = defender.GetComponent<BossAI>();
            player.bossStat.UpdateHpBar();
        }
    }
}
