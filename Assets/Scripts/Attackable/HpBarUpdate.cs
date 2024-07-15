using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarUpdate : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        var player = defender.GetComponent<PlayerAI>();
        var characterHealth = gameObject.GetComponent<IDamageable>();
        if (player.characterStat.maxHealth.factor > attack.Damage.factor)
        {
            return;
        }
        else if (player.characterStat.maxHealth.factor < attack.Damage.factor)
        {
            player.UpdateHpBar(0f);
        }
        else 
        {
            if (characterHealth.Health.factor < player.characterStat.maxHealth.factor)
            {
                player.UpdateHpBar(0f);
            }
            float percent =
            (float)characterHealth.Health.numberList[characterHealth.Health.factor - 1]
            / player.characterStat.maxHealth.numberList[player.characterStat.maxHealth.factor - 1];
            player.UpdateHpBar(percent);
        }
    }
}
