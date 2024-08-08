using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnhance
{
    public int maxReserveSkillCount;
    public int maxSpawnSkillCount;
    public int currentSpawnSkillCount;


    //Level / Value / Cost 세트로 있어야 함
    public int attackPowerLevel;
    public int attackPowerMaxLevel;
    public int attackPowerValue;
    public BigInteger attackPowerCost;

    public int maxHealthLevel;
    public int maxHealthMaxLevel;
    public int maxHealthValue;
    public BigInteger maxHealthCost;

    public int defenceLevel;
    public int defenceMaxLevel;
    public float defenceValue;
    public BigInteger defenceCost;

    public int criticalPercentLevel;
    public int criticalPercentMaxLevel;
    public float criticalPercentValue;
    public BigInteger criticalPercentCost;

    public int criticalMultipleLevel;
    public int criticalMultipleMaxLevel;
    public float criticalMultipleValue;
    public BigInteger criticalMultipleCost;

    public int recoveryLevel;
    public int recoveryMaxLevel;
    public int recoveryValue;
    public BigInteger recoveryCost;

    public int goldLevel;
    public int goldMaxLevel;
    public float goldValue;
    public BigInteger goldCost;

    public int cbnUpgradeLv;
    public int skill1Lv;
    public int skill2Lv;
    public int skill3Lv;
    public int skill4Lv;
    public int skill1per;
    public int skill2per;
    public int skill3per;
    public int skill4per;
    public BigInteger cbnUpgradeCost;

    public void Init()
    {
        //세이브 로드시 가져와야 함
        cbnUpgradeLv = 1;
        maxReserveSkillCount = 20;
        maxSpawnSkillCount = 14;
        currentSpawnSkillCount = maxSpawnSkillCount;
        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();

        var attackPowerUpgradeData = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10001);
        attackPowerLevel = 0;
        attackPowerMaxLevel = attackPowerUpgradeData.MaxLv;
        attackPowerValue = (int)attackPowerUpgradeData.Increase;
        attackPowerCost = new BigInteger(attackPowerUpgradeData.Gold)
            + new BigInteger(attackPowerUpgradeData.GoldRange) * attackPowerLevel;

        var maxHealthUpgradeData = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003);
        maxHealthLevel = 0;
        maxHealthMaxLevel = maxHealthUpgradeData.MaxLv;
        maxHealthValue = (int)maxHealthUpgradeData.Increase;
        maxHealthCost = new BigInteger(attackPowerUpgradeData.Gold)
            + new BigInteger(attackPowerUpgradeData.GoldRange) * maxHealthLevel;

        var defenceUpgradeData = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10002);
        defenceLevel = 0;
        defenceMaxLevel = defenceUpgradeData.MaxLv;
        defenceValue = defenceUpgradeData.Increase;
        defenceCost = new BigInteger(defenceUpgradeData.Gold)
            + new BigInteger(defenceUpgradeData.GoldRange) * defenceLevel;

        var criticalPercentUpgradeData = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10004);
        criticalPercentLevel = 0;
        criticalPercentMaxLevel = criticalPercentUpgradeData.MaxLv;
        criticalPercentValue = criticalPercentUpgradeData.Increase;
        criticalPercentCost = new BigInteger(criticalPercentUpgradeData.Gold)
            + new BigInteger(criticalPercentUpgradeData.GoldRange) * criticalPercentLevel;

        var criticalMultipleUpgradeData = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10005);
        criticalMultipleLevel = 0;
        criticalMultipleMaxLevel = criticalMultipleUpgradeData.MaxLv;
        criticalMultipleValue = criticalMultipleUpgradeData.Increase;
        criticalMultipleCost = new BigInteger(criticalMultipleUpgradeData.Gold)
            + new BigInteger(criticalMultipleUpgradeData.GoldRange) * criticalMultipleLevel;

        var recoveryUpgradeData = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10007);
        recoveryLevel = 0;
        recoveryMaxLevel = recoveryUpgradeData.MaxLv;
        recoveryValue = (int)recoveryUpgradeData.Increase;
        recoveryCost = new BigInteger(recoveryUpgradeData.Gold)
           + new BigInteger(recoveryUpgradeData.GoldRange) * recoveryLevel;

        var goldUpgradeData = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10006);
        goldLevel = 0;
        goldMaxLevel = goldUpgradeData.MaxLv;
        goldValue = goldUpgradeData.Increase;
        goldCost = new BigInteger(goldUpgradeData.Gold)
           + new BigInteger(goldUpgradeData.GoldRange) * goldLevel;

        var skillSummonData = DataTableMgr.Get<SkillSummonTable>(DataTableIds.skillSummon).GetID(cbnUpgradeLv);
        skill1Lv = skillSummonData.skill1Lv;
        skill2Lv = skillSummonData.skill2Lv;
        skill3Lv = skillSummonData.skill3Lv;
        skill4Lv = skillSummonData.skill4Lv;
        skill1per = skillSummonData.skill1per;
        skill2per = skillSummonData.skill2per;
        skill3per = skillSummonData.skill3per;
        skill4per = skillSummonData.skill4per;


        GameMgr.Instance.uiMgr.uiEnhance.Init();
        GameMgr.Instance.uiMgr.uiEnhance.AttackTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.DefenceTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.MaxHealthTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.RecoveryTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.CriticalPercentTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.CriticalMultipleTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.GoldIncreaseTextUpdate();


    }

    public void AddAttackPower()
    {
        if (attackPowerCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(attackPowerCost);

        attackPowerLevel++;
        attackPowerCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10001).Gold)
            + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003).GoldRange) * attackPowerLevel;
        // GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.AttackPower);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        EventMgr.TriggerEvent(QuestType.AttackEnhance);

    }

    public void AddDefence()
    {
        if (defenceCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(defenceCost);

        defenceLevel++;
        defenceCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10002).Gold)
           + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10002).GoldRange) * defenceLevel;
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        EventMgr.TriggerEvent(QuestType.DefenceEnhance);
    }

    public void AddMaxHealth()
    {
        if (maxHealthCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(maxHealthCost);

        maxHealthLevel++;
        maxHealthCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003).Gold)
          + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003).GoldRange) * maxHealthLevel;
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
        GameMgr.Instance.playerMgr.characters[0].UpdateHpBar();
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        EventMgr.TriggerEvent(QuestType.MaxHealthEnhance);

    }

    public void AddRecovery()
    {
        if (recoveryCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(recoveryCost);

        recoveryLevel++;
        recoveryCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10007).Gold)
         + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10007).GoldRange) * recoveryLevel;
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        EventMgr.TriggerEvent(QuestType.RecoveryEnhance);
    }

    public void AddCriticalPercent()
    {
        if (criticalPercentCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(criticalPercentCost);

        criticalPercentLevel++;
        criticalPercentCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10004).Gold)
          + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10004).GoldRange) * criticalPercentLevel;
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        EventMgr.TriggerEvent(QuestType.CriticalPercentEnhance);
    }

    public void AddCriticalMultiple()
    {
        if (criticalMultipleCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(criticalMultipleCost);

        criticalMultipleLevel++;
        criticalMultipleCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10005).Gold)
           + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10005).GoldRange) * criticalMultipleLevel;
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        EventMgr.TriggerEvent(QuestType.CriticalMultipleEnhance);

    }
    public void AddGoldIncrease()
    {
        if (goldCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(goldCost);

        goldLevel++;
        goldCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10006).Gold)
         + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10006).GoldRange) * goldLevel;
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        EventMgr.TriggerEvent(QuestType.GoldEnhance);

    }
}
