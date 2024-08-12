using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBagDamageTake : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        var score  = gameObject.GetComponent<IDamageable>();
        score.Health += attack.Damage;
        Debug.Log(score.Health);
        gameObject.GetComponent<DamageDisplay>().DisplayText(attack);
    }
}
