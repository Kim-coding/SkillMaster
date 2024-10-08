using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiEnhance : MonoBehaviour
{
    private PlayerEnhance p_E;
    private PlayerBaseStat p_BS;
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
        attackPowerUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10001).GetStringID);
        defenceUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddDefence);
        defenceUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10002).GetStringID);
        maxHealthUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddMaxHealth);
        maxHealthUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003).GetStringID);
        recoveryUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddRecovery);
        recoveryUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10007).GetStringID);
        criticalPercentUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddCriticalPercent);
        criticalPercentUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10004).GetStringID);
        criticalMultipleUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddCriticalMultiple);
        criticalMultipleUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10005).GetStringID);

        goldUpgrade.GetComponent<Button>().onClick.AddListener(GameMgr.Instance.playerMgr.playerEnhance.AddGoldIncrease);
        goldUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10006).GetStringID);
    }


    public void Init()
    {
        p_E = GameMgr.Instance.playerMgr.playerEnhance;
        p_BS = GameMgr.Instance.playerMgr.playerBaseStat;
        EventMgr.StartListening(QuestType.AttackEnhance, AttackTextUpdate);
        EventMgr.StartListening(QuestType.DefenceEnhance, DefenceTextUpdate);
        EventMgr.StartListening(QuestType.MaxHealthEnhance, MaxHealthTextUpdate);
        EventMgr.StartListening(QuestType.RecoveryEnhance, RecoveryTextUpdate);
        EventMgr.StartListening(QuestType.CriticalPercentEnhance, CriticalPercentTextUpdate);
        EventMgr.StartListening(QuestType.CriticalMultipleEnhance, CriticalMultipleTextUpdate);
        EventMgr.StartListening(QuestType.GoldEnhance, GoldIncreaseTextUpdate);

    }

    public void AttackTextUpdate()
    {
        attackPowerUpgrade.TextUpdate(
       p_E.attackPowerLevel,
       p_BS.basePlayerAttackPower + p_E.attackPowerLevel * p_E.attackPowerValue,
       p_BS.basePlayerAttackPower + (p_E.attackPowerLevel + 1) * p_E.attackPowerValue,
       p_E.attackPowerCost);
    }

    public void DefenceTextUpdate()
    {
        defenceUpgrade.TextUpdate(
            p_E.defenceLevel,
            p_BS.basePlayerDefence + p_E.defenceLevel * p_E.defenceValue,
           p_BS.basePlayerDefence +( p_E.defenceLevel + 1) * p_E.defenceValue,
            p_E.defenceCost);
    }
    public void MaxHealthTextUpdate()
    {
        maxHealthUpgrade.TextUpdate(
            p_E.maxHealthLevel,
            p_BS.basePlayerMaxHealth + p_E.maxHealthLevel * p_E.maxHealthValue,
            p_BS.basePlayerMaxHealth + (p_E.maxHealthLevel + 1) * p_E.maxHealthValue,
            p_E.maxHealthCost);
    }

    public void RecoveryTextUpdate()
    {
        recoveryUpgrade.TextUpdate(
            p_E.recoveryLevel,
            p_BS.basePlayerHealthRecovery + p_E.recoveryLevel * p_E.recoveryValue,
            p_BS.basePlayerHealthRecovery + (p_E.recoveryLevel + 1) * p_E.recoveryValue,
            p_E.recoveryCost);
    }

    public void CriticalPercentTextUpdate()
    {
        criticalPercentUpgrade.PercentTextUpdate(
            p_E.criticalPercentLevel,
            p_BS.basePlayerCriticalPercent + p_E.criticalPercentLevel * p_E.criticalPercentValue,
            p_BS.basePlayerCriticalPercent + (p_E.criticalPercentLevel + 1) * p_E.criticalPercentValue,
            p_E.criticalPercentCost);
    }
    public void CriticalMultipleTextUpdate()
    {
        criticalMultipleUpgrade.PercentTextUpdate(
           p_E.criticalMultipleLevel,
           p_BS.basePlayerCriticalMultiple + p_E.criticalMultipleLevel * p_E.criticalMultipleValue,
          p_BS.basePlayerCriticalMultiple + (p_E.criticalMultipleLevel + 1) * p_E.criticalMultipleValue,
           p_E.criticalMultipleCost);
    }
    public void GoldIncreaseTextUpdate()
    {
        goldUpgrade.PercentTextUpdate(
           p_E.goldLevel,
           1 + p_E.goldLevel * p_E.goldValue,
          1 + (p_E.goldLevel + 1) * p_E.goldValue,
           p_E.goldCost);
    }

}
