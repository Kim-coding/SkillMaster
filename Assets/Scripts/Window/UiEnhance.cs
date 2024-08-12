using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiEnhance : MonoBehaviour
{
    private PlayerEnhance p_E;
    private PlayerBaseStat p_BS;

    public Toggle[] enhanceModes;
    public GameObject statPanel;
    public GameObject skillPanel;

    public Enhance attackPowerUpgrade;
    public Enhance defenceUpgrade;
    public Enhance maxHealthUpgrade;
    public Enhance recoveryUpgrade;
    public Enhance criticalPercentUpgrade;
    public Enhance criticalMultipleUpgrade;
    public Enhance goldUpgrade;

    public Enhance maxSkillCount;
    public Enhance summonCooldown;
    public Enhance minSummonLevel;
    public Enhance autoSummonCooldown;
    public Enhance autoMergeCooldown;

    private void Start()
    {
        attackPowerUpgrade.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddAttackPower;
        attackPowerUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10001).GetStringID);
        defenceUpgrade.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddDefence;
        defenceUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10002).GetStringID);
        maxHealthUpgrade.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddMaxHealth;
        maxHealthUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003).GetStringID);
        recoveryUpgrade.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddRecovery;
        recoveryUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10007).GetStringID);
        criticalPercentUpgrade.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddCriticalPercent;
        criticalPercentUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10004).GetStringID);
        criticalMultipleUpgrade.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddCriticalMultiple;
        criticalMultipleUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10005).GetStringID);

        goldUpgrade.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddGoldIncrease;
        goldUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10006).GetStringID);

        maxSkillCount.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddMaxReserveSkillCount;
        maxSkillCount.GetComponent<Enhance>().buttonClick += MaxReserveSkillTextUpdate;
        maxSkillCount.Init(DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190001).GetCbnName);

        summonCooldown.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddSkillSpawnCooldown;
        summonCooldown.GetComponent<Enhance>().buttonClick += SpawnSkillCooldownTextUpdate;
        summonCooldown.Init(DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190002).GetCbnName);

        minSummonLevel.GetComponent<Enhance>().buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddSpawnSkillLevel;
        minSummonLevel.GetComponent<Enhance>().buttonClick += MinSummonLvTextUpdate;
        minSummonLevel.Init(DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190003).GetCbnName);

        foreach (var toggle in enhanceModes)
        {
            toggle.onValueChanged.AddListener(onToggleValueChange);
        }


        UpdateToggleColors();
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
       p_E.attackPowerMaxLevel,
       p_BS.basePlayerAttackPower + p_E.attackPowerLevel * p_E.attackPowerValue,
       p_BS.basePlayerAttackPower + (p_E.attackPowerLevel + 1) * p_E.attackPowerValue,
       p_E.attackPowerCost);
    }

    public void DefenceTextUpdate()
    {
        defenceUpgrade.TextUpdate(
            p_E.defenceLevel,
            p_E.defenceMaxLevel,
            p_BS.basePlayerDefence + p_E.defenceLevel * p_E.defenceValue,
           p_BS.basePlayerDefence +( p_E.defenceLevel + 1) * p_E.defenceValue,
            p_E.defenceCost);
    }
    public void MaxHealthTextUpdate()
    {
        maxHealthUpgrade.TextUpdate(
            p_E.maxHealthLevel,
            p_E.maxHealthMaxLevel,
            p_BS.basePlayerMaxHealth + p_E.maxHealthLevel * p_E.maxHealthValue,
            p_BS.basePlayerMaxHealth + (p_E.maxHealthLevel + 1) * p_E.maxHealthValue,
            p_E.maxHealthCost);
    }

    public void RecoveryTextUpdate()
    {
        recoveryUpgrade.TextUpdate(
            p_E.recoveryLevel,
            p_E.recoveryMaxLevel,
            p_BS.basePlayerHealthRecovery + p_E.recoveryLevel * p_E.recoveryValue,
            p_BS.basePlayerHealthRecovery + (p_E.recoveryLevel + 1) * p_E.recoveryValue,
            p_E.recoveryCost);
    }

    public void CriticalPercentTextUpdate()
    {
        criticalPercentUpgrade.PercentTextUpdate(
            p_E.criticalPercentLevel,
            p_E.criticalPercentMaxLevel,
            p_BS.basePlayerCriticalPercent + p_E.criticalPercentLevel * p_E.criticalPercentValue,
            p_BS.basePlayerCriticalPercent + (p_E.criticalPercentLevel + 1) * p_E.criticalPercentValue,
            p_E.criticalPercentCost);
    }
    public void CriticalMultipleTextUpdate()
    {
        criticalMultipleUpgrade.PercentTextUpdate(
           p_E.criticalMultipleLevel,
           p_E.criticalMultipleMaxLevel,
           p_BS.basePlayerCriticalMultiple + p_E.criticalMultipleLevel * p_E.criticalMultipleValue,
          p_BS.basePlayerCriticalMultiple + (p_E.criticalMultipleLevel + 1) * p_E.criticalMultipleValue,
           p_E.criticalMultipleCost);
    }
    public void GoldIncreaseTextUpdate()
    {
        goldUpgrade.PercentTextUpdate(
           p_E.goldLevel,
           p_E.goldMaxLevel,
           1 + p_E.goldLevel * p_E.goldValue,
          1 + (p_E.goldLevel + 1) * p_E.goldValue,
           p_E.goldCost);
    }
    public void MaxReserveSkillTextUpdate()
    {
        maxSkillCount.TextUpdate(
            p_E.maxReserveSkillLevel,
            p_E.maxReserveSkillMaxLevel,
            p_E.maxReserveSkillCount,
            p_E.maxReserveSkillCount + p_E.maxReserveSkillValue,
            p_E.maxReserveSkillCost);;
    }

    public void SpawnSkillCooldownTextUpdate()
    {
        summonCooldown.TextUpdate(
            p_E.SkillSpawnCooldownLevel,
            p_E.SkillSpawnCooldownMaxLevel,
            GameMgr.Instance.playerMgr.spawnCooldown,
            GameMgr.Instance.playerMgr.spawnCooldown - p_E.SkillSpawnCooldownValue,
            p_E.SkillSpawnCooldownCost);
    }

    public void MinSummonLvTextUpdate()
    {
        minSummonLevel.TextUpdate(
            p_E.SpawnSkillLvLevel,
            p_E.SpawnSkillLvMaxLevel,
            p_E.skill1Lv,
            p_E.skill1Lv + 1,
            p_E.SpawnSkillLvCost);
    }


    private void onToggleValueChange(bool isOn)
    {
        UpdateToggleColors();
        if (enhanceModes[0].isOn)
        {
            statPanel.gameObject.SetActive(isOn);
            skillPanel.gameObject.SetActive(!isOn);
        }
        else
        {
            statPanel.gameObject.SetActive(!isOn);
            skillPanel.gameObject.SetActive(isOn);
        }
    }

    private void UpdateToggleColors()
    {
        foreach (var toggle in enhanceModes)
        {
            if (toggle.isOn)
            {
                SetToggleColor(toggle, Color.blue);
            }
            else
            {
                SetToggleColor(toggle, Color.white);
            }
        }
    }
    private void SetToggleColor(Toggle toggle, Color color)
    {
        var background = toggle.targetGraphic;
        var checkmark = toggle.graphic;

        if (background != null)
        {
            background.color = color;
        }

        if (checkmark != null)
        {
            checkmark.color = color;
        }
    }
}
