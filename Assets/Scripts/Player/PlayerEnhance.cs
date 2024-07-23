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
    public int attackPowerValue;
    public BigInteger attackPowerCost;

    public int maxHealthLevel;
    public int maxHealthValue;
    public BigInteger maxHealthCost;

    public int defenceLevel;
    public int defenceValue;
    public BigInteger defenceCost;

    public int criticalPercentLevel;
    public float criticalPercentValue;
    public BigInteger criticalPercentCost;

    public int criticalMultipleLevel;
    public float criticalMultipleValue;
    public BigInteger criticalMultipleCost;

    public int recoveryLevel;
    public int recoveryValue;
    public BigInteger recoveryCost;

    public int goldLevel;
    public float goldValue;
    public BigInteger goldCost;

    public void Init()
    {
        //세이브 로드시 가져와야 함

        maxReserveSkillCount = 20;
        maxSpawnSkillCount = 14;
        currentSpawnSkillCount = maxSpawnSkillCount;
        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();

        attackPowerLevel = 0;
        attackPowerValue = 1; //TO-DO 테이블에서
        attackPowerCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10001).Gold)
            + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003).GoldRange) * attackPowerLevel;

        maxHealthLevel = 0;
        maxHealthValue = (int)DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003).Increase;
        maxHealthCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003).Gold)
            + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10003).GoldRange) * maxHealthLevel;

        defenceLevel = 0;
        defenceValue = (int)DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10002).Increase;
        defenceCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10002).Gold)
            + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10002).GoldRange) * defenceLevel;

        criticalPercentLevel = 0;
        criticalPercentValue = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10004).Increase;
        criticalPercentCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10004).Gold)
            + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10004).GoldRange) * criticalPercentLevel;

        criticalMultipleLevel = 0;
        criticalMultipleValue = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10005).Increase;
        criticalMultipleCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10005).Gold)
            + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10005).GoldRange) * criticalMultipleLevel;

        recoveryLevel = 0;
        recoveryValue = (int)DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10007).Increase;
        recoveryCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10007).Gold)
           + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10007).GoldRange) * recoveryLevel;

        goldLevel = 0;
        goldValue = DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10006).Increase;
        goldCost = new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10006).Gold)
           + new BigInteger(DataTableMgr.Get<UpgradeTable>(DataTableIds.upgrade).GetID(10006).GoldRange) * goldLevel;

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
        EventMgr.TriggerEvent(QuestType.GoldEnhance);

    }

}
