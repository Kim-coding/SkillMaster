using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiEnhance : MonoBehaviour
{
    private PlayerEnhance p_E;
    public Enhance attackPowerUpgrade;

    private void Start()
    {
        attackPowerUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddAttackPower);
    }


    public void TextUpdate()
    {
        p_E = GameMgr.Instance.playerMgr.playerEnhance;
        attackPowerUpgrade.TextUpdate(p_E.attackPowerLevel, p_E.attackPowerLevel * p_E.attackPowerPercent, (p_E.attackPowerLevel + 1) * p_E.attackPowerPercent, p_E.attackPowerCost);
    }
}
