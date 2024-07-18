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
    public int goldValue;
    public BigInteger goldCost;

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

        maxHealthLevel = 0;
        maxHealthValue = 1; //TO-DO ���̺���
        maxHealthCost = new BigInteger(100 + 100 * maxHealthLevel); //TO-DO �� �־�ξ����

        defenceLevel = 0;
        defenceValue = 1; //TO-DO ���̺���
        defenceCost = new BigInteger(100 + 100 * defenceLevel); //TO-DO �� �־�ξ����

        criticalPercentLevel = 0;
        criticalPercentValue = 1; //TO-DO ���̺���
        criticalPercentCost = new BigInteger(100 + 100 * criticalPercentLevel); //TO-DO �� �־�ξ����

        criticalMultipleLevel = 0;
        criticalMultipleValue = 1; //TO-DO ���̺���
        criticalMultipleCost = new BigInteger(100 + 100 * criticalMultipleLevel); //TO-DO �� �־�ξ����

        recoveryLevel = 0;
        recoveryValue = 1; //TO-DO ���̺���
        recoveryCost = new BigInteger(100 + 100 * recoveryLevel); //TO-DO �� �־�ξ����

        goldLevel = 0;
        goldValue = 1; //TO-DO ���̺���
        goldCost = new BigInteger(100 + 100 * goldLevel); //TO-DO �� �־�ξ����


        GameMgr.Instance.uiMgr.uiEnhance.Init();
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.AttackPower);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Defence);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.MaxHealth);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Recovery);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.CriticalMultiple);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.CriticalPercent);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Gold);

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
        defenceCost = new BigInteger(100 + 100 * defenceLevel); //TO-DO cost ��
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Defence);
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
        maxHealthCost = new BigInteger(100 + 100 * maxHealthLevel); //TO-DO cost ��
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.MaxHealth);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
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
        recoveryCost = new BigInteger(100 + 100 * recoveryLevel); //TO-DO cost ��
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Recovery);
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
        criticalPercentCost = new BigInteger(100 + 100 * criticalPercentLevel); //TO-DO cost ��
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.CriticalPercent);
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
        criticalMultipleCost = new BigInteger(100 + 100 * criticalMultipleLevel); //TO-DO cost ��
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.CriticalMultiple);
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
        goldCost = new BigInteger(100 + 100 * goldLevel); //TO-DO cost ��
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate(EnhanceType.Gold);
        EventMgr.TriggerEvent(QuestType.GoldEnhance);

    }

}
