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

    public int defenceLevel;
    public int defenceValue;
    public BigInteger defenceCost;

    public int maxHealthLevel;
    public int maxHealthValue;
    public BigInteger maxHealthCost;

    public int recoveryLevel;
    public int recoveryValue;
    public BigInteger recoveryCost;

    public int criticalPercentLevel;
    public float criticalPercentValue;
    public BigInteger criticalPercentCost;

    public int criticalMultipleLevel;
    public float criticalMultipleValue;
    public BigInteger criticalMultipleCost;

    public int attackSpeedLevel;
    public float attackSpeedValue;
    public BigInteger attackSpeedCost;

    public int speedLevel;
    public float speedValue;
    public BigInteger speedCost;

    public int attackRangeLevel;
    public float attackRangeValue;
    public BigInteger attackRangeCost;

    public void Init()
    {
        //세이브 로드시 가져와야 함

        maxReserveSkillCount = 20;
        maxSpawnSkillCount = 14;
        currentSpawnSkillCount = maxSpawnSkillCount;
        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();

        attackPowerLevel = 0;
        attackPowerValue = 1; //TO-DO 테이블에서
        attackPowerCost = new BigInteger(100 + 100 * attackPowerLevel); //TO-DO 식 넣어두어야함

        defenceLevel = 0;
        defenceValue = 1; //TO-DO 테이블에서
        defenceCost = new BigInteger(100 + 100 * defenceLevel); //TO-DO 식 넣어두어야함

        maxHealthLevel = 0;
        maxHealthValue = 1; //TO-DO 테이블에서
        maxHealthCost = new BigInteger(100 + 100 * maxHealthLevel); //TO-DO 식 넣어두어야함

        recoveryLevel = 0;
        recoveryValue = 1; //TO-DO 테이블에서
        recoveryCost = new BigInteger(100 + 100 * recoveryLevel); //TO-DO 식 넣어두어야함

        criticalPercentLevel = 0;
        criticalPercentValue = 1; //TO-DO 테이블에서
        criticalPercentCost = new BigInteger(100 + 100 * criticalPercentLevel); //TO-DO 식 넣어두어야함

        criticalMultipleLevel = 0;
        criticalMultipleValue = 1; //TO-DO 테이블에서
        criticalMultipleCost = new BigInteger(100 + 100 * criticalMultipleLevel); //TO-DO 식 넣어두어야함

        attackSpeedLevel = 0;
        attackSpeedValue = 1; //TO-DO 테이블에서
        attackSpeedCost = new BigInteger(100 + 100 * attackSpeedLevel); //TO-DO 식 넣어두어야함

        speedLevel = 0;
        speedValue = 1; //TO-DO 테이블에서
        speedCost = new BigInteger(100 + 100 * speedLevel); //TO-DO 식 넣어두어야함

        attackRangeLevel = 0;
        attackRangeValue = 1; //TO-DO 테이블에서
        attackRangeCost = new BigInteger(100 + 100 * attackRangeLevel); //TO-DO 식 넣어두어야함

        GameMgr.Instance.uiMgr.uiEnhance.Init();
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.AttackPower);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Defence);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.MaxHealth);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Recovery);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.CriticalMultiple);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.CriticalPercent);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.AttackSpeed);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Speed);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.AttackRange);

    }

    public void AddAttackPower()
    {
        if (attackPowerCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(attackPowerCost);

        attackPowerLevel++;
        attackPowerCost = new BigInteger(100 + 100 * attackPowerLevel); //TO-DO cost 식
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.AttackPower);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

    public void AddDefence()
    {
        if (defenceCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(defenceCost);

        defenceLevel++;
        defenceCost = new BigInteger(100 + 100 * defenceLevel); //TO-DO cost 식
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Defence);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

    public void AddMaxHealth()
    {
        if (maxHealthCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(maxHealthCost);

        maxHealthLevel++;
        maxHealthCost = new BigInteger(100 + 100 * maxHealthLevel); //TO-DO cost 식
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.MaxHealth);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

    public void AddRecovery()
    {
        if (recoveryCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(recoveryCost);

        recoveryLevel++;
        recoveryCost = new BigInteger(100 + 100 * recoveryLevel); //TO-DO cost 식
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Recovery);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

    public void AddCriticalPercent()
    {
        if (criticalPercentCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(criticalPercentCost);

        criticalPercentLevel++;
        criticalPercentCost = new BigInteger(100 + 100 * criticalPercentLevel); //TO-DO cost 식
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.CriticalPercent);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

    public void AddCriticalMultiple()
    {
        if (criticalMultipleCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(criticalMultipleCost);

        criticalMultipleLevel++;
        criticalMultipleCost = new BigInteger(100 + 100 * criticalMultipleLevel); //TO-DO cost 식
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.CriticalMultiple);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

    public void AddAttackSpeed()
    {
        if (attackSpeedCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(attackSpeedCost);

        attackSpeedLevel++;
        attackSpeedCost = new BigInteger(100 + 100 * attackSpeedLevel); //TO-DO cost 식
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.AttackSpeed);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

    public void AddSpeed()
    {
        if (speedCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(speedCost);

        speedLevel++;
        speedCost = new BigInteger(100 + 100 * speedLevel); //TO-DO cost 식
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Speed);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

    public void AddAttackRange()
    {
        if (attackRangeCost > GameMgr.Instance.playerMgr.currency.gold)
        {
            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveGold(attackRangeCost);

        attackRangeLevel++;
        attackRangeCost = new BigInteger(100 + 100 * attackRangeLevel); //TO-DO cost 식
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.AttackRange);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

}
