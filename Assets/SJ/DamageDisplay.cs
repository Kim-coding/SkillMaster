using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour, IAttackable
{
    public TextMeshPro damagePrefab;

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var damagePos = transform.position;
        damagePos.y += 1f;

        var textPrefab = Instantiate(damagePrefab, damagePos, Quaternion.identity);

        textPrefab.text = attack.Damage.ToString();
        if (attack.Critical)
        {
            textPrefab.text = attack.Damage.ToString() + "!!!";
            textPrefab.color = Color.yellow;
        }
        else
        {
            textPrefab.color = Color.red;
        }
    }
}
