using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarUpdate : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        var player = defender.GetComponent<PlayerAI>();
        player.characterStat.UpdateHpBar();
    }
}
