using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnhance
{
    public int maxReserveSkillCount;
    public int maxSpawnSkillCount;
    public int currentSpawnSkillCount;


    //Level / Value / Cost ��Ʈ�� �־�� ��
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
        //���̺� �ε�� �����;� ��

        maxReserveSkillCount = 20;
        maxSpawnSkillCount = 14;
        currentSpawnSkillCount = maxSpawnSkillCount;
        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();

        attackPowerLevel = 0;
        attackPowerValue = 1; //TO-DO ���̺���
        attackPowerCost = new BigInteger(100 + 100 * attackPowerLevel); //TO-DO �� �־�ξ����

        defenceLevel = 0;
        defenceValue = 1; //TO-DO ���̺���
        defenceCost = new BigInteger(100 + 100 * defenceLevel); //TO-DO �� �־�ξ����

        maxHealthLevel = 0;
        maxHealthValue = 1; //TO-DO ���̺���
        maxHealthCost = new BigInteger(100 + 100 * maxHealthLevel); //TO-DO �� �־�ξ����

        recoveryLevel = 0;
        recoveryValue = 1; //TO-DO ���̺���
        recoveryCost = new BigInteger(100 + 100 * recoveryLevel); //TO-DO �� �־�ξ����

        criticalPercentLevel = 0;
        criticalPercentValue = 1; //TO-DO ���̺���
        criticalPercentCost = new BigInteger(100 + 100 * criticalPercentLevel); //TO-DO �� �־�ξ����

        criticalMultipleLevel = 0;
        criticalMultipleValue = 1; //TO-DO ���̺���
        criticalMultipleCost = new BigInteger(100 + 100 * criticalMultipleLevel); //TO-DO �� �־�ξ����

        attackSpeedLevel = 0;
        attackSpeedValue = 1; //TO-DO ���̺���
        attackSpeedCost = new BigInteger(100 + 100 * attackSpeedLevel); //TO-DO �� �־�ξ����

        speedLevel = 0;
        speedValue = 1; //TO-DO ���̺���
        speedCost = new BigInteger(100 + 100 * speedLevel); //TO-DO �� �־�ξ����

        attackRangeLevel = 0;
        attackRangeValue = 1; //TO-DO ���̺���
        attackRangeCost = new BigInteger(100 + 100 * attackRangeLevel); //TO-DO �� �־�ξ����

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
        attackPowerCost = new BigInteger(100 + 100 * attackPowerLevel); //TO-DO cost ��
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
        defenceCost = new BigInteger(100 + 100 * defenceLevel); //TO-DO cost ��
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
        maxHealthCost = new BigInteger(100 + 100 * maxHealthLevel); //TO-DO cost ��
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
        recoveryCost = new BigInteger(100 + 100 * recoveryLevel); //TO-DO cost ��
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
        criticalPercentCost = new BigInteger(100 + 100 * criticalPercentLevel); //TO-DO cost ��
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
        criticalMultipleCost = new BigInteger(100 + 100 * criticalMultipleLevel); //TO-DO cost ��
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
        attackSpeedCost = new BigInteger(100 + 100 * attackSpeedLevel); //TO-DO cost ��
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
        speedCost = new BigInteger(100 + 100 * speedLevel); //TO-DO cost ��
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
        attackRangeCost = new BigInteger(100 + 100 * attackRangeLevel); //TO-DO cost ��
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.AttackRange);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

}
