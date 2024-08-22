using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiEnhance : MonoBehaviour
{
    private PlayerEnhance p_E;
    private PlayerBaseStat p_BS;
    private PlayerStat p_S;

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
        attackPowerUpgrade.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddAttackPower;
        attackPowerUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10001));
        defenceUpgrade.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddDefence;
        defenceUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10002));
        maxHealthUpgrade.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddMaxHealth;
        maxHealthUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003));
        recoveryUpgrade.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddRecovery;
        recoveryUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10007));
        criticalPercentUpgrade.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddCriticalPercent;
        criticalPercentUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10004));
        criticalMultipleUpgrade.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddCriticalMultiple;
        criticalMultipleUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10005));

        goldUpgrade.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddGoldIncrease;
        goldUpgrade.Init(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10006));

        maxSkillCount.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddMaxReserveSkillCount;
        //maxSkillCount.button.buttonClick += MaxReserveSkillTextUpdate;
        var data = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190001);
        maxSkillCount.Init(data);

        summonCooldown.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddSkillSpawnCooldown;
        summonCooldown.button.buttonClick += SpawnSkillCooldownTextUpdate;
        data = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190002);
        summonCooldown.Init(data);

        minSummonLevel.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddSpawnSkillLevel;
        minSummonLevel.button.buttonClick += MinSummonLvTextUpdate;
        data = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190003);
        minSummonLevel.Init(data);

        autoSummonCooldown.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddAutoSpawnCooldown;
        autoSummonCooldown.button.buttonClick += AUtoSpawnTextUpdate;
        data = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190004);
        autoSummonCooldown.Init(data);

        autoMergeCooldown.button.buttonClick += GameMgr.Instance.playerMgr.playerEnhance.AddAutoMergeCooldown;
        autoMergeCooldown.button.buttonClick += AUtoMergeTextUpdate;
        data = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190005);
        autoMergeCooldown.Init(data);


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
        p_S = GameMgr.Instance.playerMgr.playerStat;
        EventMgr.StartListening(QuestType.AttackEnhance, AttackTextUpdate);
        EventMgr.StartListening(QuestType.DefenceEnhance, DefenceTextUpdate);
        EventMgr.StartListening(QuestType.MaxHealthEnhance, MaxHealthTextUpdate);
        EventMgr.StartListening(QuestType.RecoveryEnhance, RecoveryTextUpdate);
        EventMgr.StartListening(QuestType.CriticalPercentEnhance, CriticalPercentTextUpdate);
        EventMgr.StartListening(QuestType.CriticalMultipleEnhance, CriticalMultipleTextUpdate);
        EventMgr.StartListening(QuestType.GoldEnhance, GoldIncreaseTextUpdate);
        EventMgr.StartListening(QuestType.MaxSkillCount, MaxReserveSkillTextUpdate);

    }

    public void AttackTextUpdate()
    {
        attackPowerUpgrade.TextUpdate(
       p_E.attackPowerLevel,
       p_E.attackPowerMaxLevel,
       p_E.attackPowerValue,
       p_E.attackPowerValue + p_E.attackPowerIncrease,
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
            p_E.cbnUpgradeLv,
            p_E.SpawnSkillLvMaxLevel,
            DataTableMgr.Get<SkillSummonTable>(DataTableIds.skillSummon).GetID(GameMgr.Instance.playerMgr.playerEnhance.cbnUpgradeLv).skill1Lv,
            DataTableMgr.Get<SkillSummonTable>(DataTableIds.skillSummon).GetID(GameMgr.Instance.playerMgr.playerEnhance.cbnUpgradeLv).skill1Lv + 1,
            p_E.SpawnSkillLvCost);
    }

    public void AUtoSpawnTextUpdate()
    {
        autoSummonCooldown.TextUpdate(
            p_E.autoSpawnLevel,
            p_E.autoSpawnMaxLevel,
            GameMgr.Instance.uiMgr.uiMerge.skillSpawner.spawnduration,
            GameMgr.Instance.uiMgr.uiMerge.skillSpawner.spawnduration - p_E.autoSpawnValue,
            p_E.autoSpawnCost);
    }

    public void AUtoMergeTextUpdate()
    {
        autoMergeCooldown.TextUpdate(
            p_E.autoMergeLevel,
            p_E.autoMergeMaxLevel,
            GameMgr.Instance.uiMgr.uiMerge.autoMergeDuration,
            GameMgr.Instance.uiMgr.uiMerge.autoMergeDuration - p_E.autoMergeValue,
            p_E.autoMergeCost);
    }


    public void onToggleValueChange(bool isOn)
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
