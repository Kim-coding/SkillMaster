using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnhance
{
    public int baseMaxReserveSkillCount;
    public int maxReserveSkillCount;
    public int maxSpawnSkillCount;
    public int currentSpawnSkillCount;

    public int maxReserveSkillLevel;
    public int maxReserveSkillMaxLevel;
    public int maxReserveSkillValue;
    public BigInteger maxReserveSkillCost;

    public int SkillSpawnCooldownLevel;
    public int SkillSpawnCooldownMaxLevel;
    public float SkillSpawnCooldownValue;
    public BigInteger SkillSpawnCooldownCost;

    public int SpawnSkillLvMaxLevel;
    public int SpawnSkillLvValue;
    public BigInteger SpawnSkillLvCost;

    public int autoSpawnLevel;
    public int autoSpawnMaxLevel;
    public float autoSpawnValue;
    public BigInteger autoSpawnCost;

    public int autoMergeLevel;
    public int autoMergeMaxLevel;
    public float autoMergeValue;
    public BigInteger autoMergeCost;


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
    public BigInteger cbnUpgradeCost;

    public void Init(int baseMaxSkill, float baseskillSpawnDuration, float baseAutoSpawnDuration, float baseAutoMergeDuration)
    {
        //세이브 로드시 가져와야 함
        baseMaxReserveSkillCount = baseMaxSkill;
        maxReserveSkillCount = baseMaxReserveSkillCount += maxReserveSkillLevel * maxReserveSkillValue;
        maxSpawnSkillCount = 14;
        currentSpawnSkillCount = maxSpawnSkillCount; // 이것도 저장해야됨 아니면 껏다킬때마다 풀충됨
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

        var maxReserveSkillData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190001);
        maxReserveSkillLevel = 0;
        maxReserveSkillMaxLevel = maxReserveSkillData.MaxLv;
        maxReserveSkillValue = (int)maxReserveSkillData.Increase;
        maxReserveSkillCost = new BigInteger(
            (maxReserveSkillLevel / maxReserveSkillData.PayRange) * maxReserveSkillData.PayIncrease + maxReserveSkillData.PayDefault
            );

        var skillCooldownData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190002);
        GameMgr.Instance.playerMgr.SetBase(baseskillSpawnDuration);
        SkillSpawnCooldownLevel = 0;
        SkillSpawnCooldownMaxLevel = skillCooldownData.MaxLv;
        SkillSpawnCooldownValue = skillCooldownData.Increase;
        SkillSpawnCooldownCost = new BigInteger(
            (SkillSpawnCooldownLevel / skillCooldownData.PayRange) * skillCooldownData.PayIncrease + skillCooldownData.PayDefault
            );

        var spawnSkillLvData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190003);
        cbnUpgradeLv = 0;
        SpawnSkillLvMaxLevel = spawnSkillLvData.MaxLv;
        SpawnSkillLvValue = (int)spawnSkillLvData.Increase;
        SpawnSkillLvCost = new BigInteger(
            (cbnUpgradeLv / spawnSkillLvData.PayRange) * spawnSkillLvData.PayIncrease + spawnSkillLvData.PayDefault
            );

        var autoSpawnData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190004);
        GameMgr.Instance.uiMgr.uiMerge.skillSpawner.SetBase(baseAutoSpawnDuration);
        autoSpawnLevel = 0;
        autoSpawnMaxLevel = autoSpawnData.MaxLv;
        autoSpawnValue = autoSpawnData.Increase;
        autoSpawnCost = new BigInteger(
            (autoSpawnLevel / autoSpawnData.PayRange) * autoSpawnData.PayIncrease + autoSpawnData.PayDefault
            );

        var autoMergeData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190005);
        GameMgr.Instance.uiMgr.uiMerge.SetBase(baseAutoMergeDuration);
        autoMergeLevel = 0;
        autoMergeMaxLevel = autoMergeData.MaxLv;
        autoMergeValue = autoMergeData.Increase;
        autoMergeCost = new BigInteger(
            (autoMergeLevel / autoMergeData.PayRange) * autoMergeData.PayIncrease + autoMergeData.PayDefault
            );

        GameMgr.Instance.uiMgr.uiEnhance.Init();
        GameMgr.Instance.uiMgr.uiEnhance.AttackTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.DefenceTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.MaxHealthTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.RecoveryTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.CriticalPercentTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.CriticalMultipleTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.GoldIncreaseTextUpdate();

        GameMgr.Instance.uiMgr.uiEnhance.MaxReserveSkillTextUpdate();
        GameMgr.Instance.playerMgr.AddSpawnSkillCooldown(SkillSpawnCooldownLevel, SkillSpawnCooldownValue);
        GameMgr.Instance.uiMgr.uiEnhance.SpawnSkillCooldownTextUpdate();
        GameMgr.Instance.uiMgr.uiEnhance.MinSummonLvTextUpdate();
        GameMgr.Instance.uiMgr.uiMerge.skillSpawner.SetSpawnDuration(autoSpawnData.Increase * autoSpawnLevel);
        GameMgr.Instance.uiMgr.uiEnhance.AUtoSpawnTextUpdate();
        GameMgr.Instance.uiMgr.uiMerge.SetAutoMergeDuration(autoMergeData.Increase * autoMergeLevel);
        GameMgr.Instance.uiMgr.uiEnhance.AUtoMergeTextUpdate();


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

    public void AddMaxReserveSkillCount()
    {
        var maxReserveSkillData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190001);
        if(maxReserveSkillData.Pay == 1)
        {
            if (maxReserveSkillCost > GameMgr.Instance.playerMgr.currency.gold)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveGold(maxReserveSkillCost);
        }
        else
        {
            if (maxReserveSkillCost > GameMgr.Instance.playerMgr.currency.diamond)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveDia(maxReserveSkillCost);
        }


        maxReserveSkillLevel++;
        maxReserveSkillCount = baseMaxReserveSkillCount + maxReserveSkillLevel * maxReserveSkillValue;
        maxReserveSkillCost = new BigInteger(
    (maxReserveSkillLevel / maxReserveSkillData.PayRange) * maxReserveSkillData.PayIncrease + maxReserveSkillData.PayDefault
    );
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        GameMgr.Instance.uiMgr.uiMerge.skillSpawner.maxReserveSkillCountUpdate();
        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();

    }
    public void AddSkillSpawnCooldown()
    {
        var SkillSpawnCooldownData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190002);

        if (SkillSpawnCooldownData.Pay == 1)
        {
            if (SkillSpawnCooldownCost > GameMgr.Instance.playerMgr.currency.gold)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveGold(SkillSpawnCooldownCost);
        }
        else
        {
            if (SkillSpawnCooldownCost > GameMgr.Instance.playerMgr.currency.diamond)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveDia(SkillSpawnCooldownCost);
        }

        SkillSpawnCooldownLevel++;
        SkillSpawnCooldownCost = new BigInteger(
    (SkillSpawnCooldownLevel / SkillSpawnCooldownData.PayRange) * SkillSpawnCooldownData.PayIncrease + SkillSpawnCooldownData.PayDefault
    );
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        GameMgr.Instance.playerMgr.AddSpawnSkillCooldown(SkillSpawnCooldownLevel, SkillSpawnCooldownValue);
    }

    public void AddSpawnSkillLevel()
    {
        var SpawnSkillLvData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190003);

        if (SpawnSkillLvData.Pay == 1)
        {
            if (SpawnSkillLvCost > GameMgr.Instance.playerMgr.currency.gold)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveGold(SpawnSkillLvCost);
        }
        else
        {
            if (SpawnSkillLvCost > GameMgr.Instance.playerMgr.currency.diamond)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveDia(SpawnSkillLvCost);
        }


        cbnUpgradeLv++;
        SpawnSkillLvCost = new BigInteger(
    (cbnUpgradeLv / SpawnSkillLvData.PayRange) * SpawnSkillLvData.PayIncrease + SpawnSkillLvData.PayDefault
    );

        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");

        //TO-DO 업데이트 해줘야함 스킬 스포너
    }

    public void AddAutoSpawnCooldown()
    {
        var autoSpawnCooldownData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190004);

        if (autoSpawnCooldownData.Pay == 1)
        {
            if (autoSpawnCost > GameMgr.Instance.playerMgr.currency.gold)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveGold(autoSpawnCost);
        }
        else
        {
            if (autoSpawnCost > GameMgr.Instance.playerMgr.currency.diamond)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveDia(autoSpawnCost);
        }

        autoSpawnLevel++;
        autoSpawnCost = new BigInteger(
    (autoSpawnLevel / autoSpawnCooldownData.PayRange) * autoSpawnCooldownData.PayIncrease + autoSpawnCooldownData.PayDefault
    );
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        GameMgr.Instance.uiMgr.uiMerge.skillSpawner.SetSpawnDuration(autoSpawnCooldownData.Increase * autoSpawnLevel);
    }

    public void AddAutoMergeCooldown()
    {
        var autoMergeData = DataTableMgr.Get<CombinationUpgradeTable>(DataTableIds.cbnUpgrade).GetID(190005);

        if (autoMergeData.Pay == 1)
        {
            if (autoMergeCost > GameMgr.Instance.playerMgr.currency.gold)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveGold(autoMergeCost);
        }
        else
        {
            if (autoMergeCost > GameMgr.Instance.playerMgr.currency.diamond)
            {
                return;
            }
            GameMgr.Instance.playerMgr.currency.RemoveDia(autoMergeCost);
        }

        autoMergeLevel++;
        autoMergeCost = new BigInteger(
    (autoMergeLevel / autoMergeData.PayRange) * autoMergeData.PayIncrease + autoMergeData.PayDefault
    );
        GameMgr.Instance.soundMgr.PlaySFX("UpgradeButton");
        GameMgr.Instance.uiMgr.uiMerge.SetAutoMergeDuration(autoMergeData.Increase * autoMergeLevel);

    }


}
