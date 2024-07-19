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
        var characterHealth = gameObject.GetComponent<IDamageable>();
        var defenceValue = 1 / (1 + characterHealth.Defence * 0.005f);
        attack.Damage *= defenceValue;
        string text = attack.Damage.ToStringShort();
        Color color = attack.Critical ? Color.yellow : Color.white;
        float fontSize = defender.GetComponent<PlayerAI>() != null ? 10f : 5f;

        if (attack.Critical)
        {
            text += "!!!";
        }

        GameMgr.Instance.sceneMgr.damageTextMgr.ShowDamageText(damagePos, text, color, fontSize);
    }
}
