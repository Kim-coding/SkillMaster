using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    public TextMeshPro damagePrefab;

    public void DisplayText(Attack attack)
    {
        if(!GameMgr.Instance.soundMgr.damageTextToggle.isOn)
        {
            return;
        }
        var damagePos = transform.position;
        damagePos.y += 1f;
        var characterHealth = gameObject.GetComponent<IDamageable>();
        var defenceValue = 1 / (1 + characterHealth.Defence * 0.005f);
        attack.Damage *= defenceValue;
        string text = attack.Damage.ToStringShort();
        Color color = Color.red;
        if (gameObject.tag == "Player")
        {
            color = Color.red;
        }
        else
        {
            color = attack.Critical ? Color.yellow : Color.white;
        }
        float fontSize = 5f;

        if (attack.Critical)
        {
            text += "\nCRITICAl";
        }

        GameMgr.Instance.sceneMgr.damageTextMgr.ShowDamageText(damagePos, text, color, fontSize, true);
    }
}
