using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SandBagDamageTake : MonoBehaviour, IAttackable
{

    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        var score = gameObject.GetComponent<IDamageable>();
        score.Health += attack.Damage;
        gameObject.GetComponent<DamageDisplay>().DisplayText(attack);
        GameMgr.Instance.uiMgr.ScoreSliderUpdate(score.Health);
    }
}
