using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour, IAttackable
{
    public TextMeshPro damagePrefab;

    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        var damagePos = transform.position;
        damagePos.y += 1f;

        var textPrefab = Instantiate(damagePrefab, damagePos, Quaternion.identity);

        textPrefab.text = attack.Damage.ToStringShort();

        if(defender.GetComponent<PlayerAI>() != null )
        {
            textPrefab.fontSize = 3;
        }

        if (attack.Critical)
        {
            textPrefab.text = attack.Damage.ToStringShort() + "!!!";
            textPrefab.color = Color.yellow;
        }
        else
        {
            textPrefab.color = Color.white;
        }
    }
}
