using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiEnhance : MonoBehaviour
{
    private PlayerEnhance p_E;
    public Enhance attackPowerUpgrade;
    public Enhance defenceUpgrade;
    public Enhance maxHealthUpgrade;
    public Enhance recoveryUpgrade;
    public Enhance criticalPercentUpgrade;
    public Enhance criticalMultipleUpgrade;
    public Enhance goldUpgrade;


    private void Start()
    {
        attackPowerUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddAttackPower);
        attackPowerUpgrade.Init("공격력 강화");
        defenceUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddDefence);
        defenceUpgrade.Init("방어력 강화");
        maxHealthUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddMaxHealth);
        maxHealthUpgrade.Init("최대 체력 강화");
        recoveryUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddRecovery);
        recoveryUpgrade.Init("회복량 강화");
        criticalPercentUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddCriticalPercent);
        criticalPercentUpgrade.Init("치명타 확률 강화");
        criticalMultipleUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddCriticalMultiple);
        criticalMultipleUpgrade.Init("치명타 배률 강화");

        goldUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddGoldIncrease);
        goldUpgrade.Init("골드 획득 강화");
    }


    public void Init()
    {
        p_E = GameMgr.Instance.playerMgr.playerEnhance;
    }


    public void TextUpdate(EnhanceType enhanceType)
    {
        switch (enhanceType)
        {
            case EnhanceType.None:
                break;
            case EnhanceType.AttackPower:
                {
                    attackPowerUpgrade.TextUpdate(
                        p_E.attackPowerLevel,
                        p_E.attackPowerLevel * p_E.attackPowerValue,
                        (p_E.attackPowerLevel + 1) * p_E.attackPowerValue,
                        p_E.attackPowerCost);
                }
                break;
            case EnhanceType.Defence:
                {
                    defenceUpgrade.TextUpdate(
                        p_E.defenceLevel,
                        p_E.defenceLevel * p_E.defenceValue,
                       (p_E.defenceLevel + 1) * p_E.defenceValue,
                        p_E.defenceCost);
                }
                break;
            case EnhanceType.MaxHealth:
                {
                    maxHealthUpgrade.TextUpdate(
                        p_E.maxHealthLevel,
                        p_E.maxHealthLevel * p_E.maxHealthValue,
                        (p_E.maxHealthLevel + 1) * p_E.maxHealthValue,
                        p_E.maxHealthCost);
                }
                break;
            case EnhanceType.Recovery:
                {
                    recoveryUpgrade.TextUpdate(
                        p_E.recoveryLevel,
                        p_E.recoveryLevel * p_E.recoveryValue,
                        (p_E.recoveryLevel + 1) * p_E.recoveryValue,
                        p_E.recoveryCost);
                }
                break;
            case EnhanceType.CriticalPercent:
                {
                    criticalPercentUpgrade.PercentTextUpdate(
                        p_E.criticalPercentLevel,
                        p_E.criticalPercentLevel * p_E.criticalPercentValue,
                        (p_E.criticalPercentLevel + 1) * p_E.criticalPercentValue,
                        p_E.criticalPercentCost);
                }
                break;
            case EnhanceType.CriticalMultiple:
                {
                    criticalMultipleUpgrade.TextUpdate(
                       p_E.criticalMultipleLevel,
                       p_E.criticalMultipleLevel * p_E.criticalMultipleValue,
                      (p_E.criticalMultipleLevel + 1) * p_E.criticalMultipleValue,
                       p_E.criticalMultipleCost);
                }
                break;
            case EnhanceType.Gold:
                {
                    goldUpgrade.TextUpdate(
                       p_E.goldLevel,
                       p_E.goldLevel * p_E.goldValue,
                      (p_E.goldLevel + 1) * p_E.goldValue,
                       p_E.goldCost);
                }
                break;
        }
    }
}
