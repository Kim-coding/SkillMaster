using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStat
{
    public string playerAttackPower;
    public int basePlayerAttackPower;
    public int defence;
    public int basePlayerDefence;

    public string playerMaxHealth;
    public int basePlayerMaxHealth;
    public string playerHealthRecovery;
    public int basePlayerHealthRecovery;

    public float speed;
    public float basePlayerSpeed;
    public float attackRange;
    public float basePlayerAttackRange;
    public float attackSpeed;
    public float basePlayerAttackSpeed;

    public float playerCriticalPercent;
    public float basePlayerCriticalPercent;
    public float playerCriticalMultiple;
    public float basePlayerCriticalMultiple;


    // ����??
    //��ȭ ��ġ�����͵� ���⼭ �ջ� �ؾߵɰ� ����.

    public void Init(int baseAttackPower, int baseMaxHealth, int baseDefence,int baseRecovery, float baseCriPer, float baseCirMulti, float baseSpeed, float baseAttackSpeed, float baseAttackRange)
    {
        basePlayerAttackPower = baseAttackPower;
        basePlayerMaxHealth = baseMaxHealth;
        basePlayerDefence = baseDefence;
        basePlayerHealthRecovery = baseRecovery;

        basePlayerCriticalPercent = baseCriPer / 100f;
        basePlayerCriticalMultiple = baseCirMulti;

        basePlayerSpeed = baseSpeed;
        basePlayerAttackSpeed = baseAttackSpeed;
        basePlayerAttackRange = baseAttackRange;
        playerStatUpdate();
    }

    public void DebugStatSetting(float dSpeed, float dAttackSpeed, float dAttackRange)
    {
        basePlayerSpeed = dSpeed;
        basePlayerAttackSpeed = dAttackSpeed;
        basePlayerAttackRange = dAttackRange;
        playerStatUpdate();
    }


    public void playerStatUpdate()
    {
        playerAttackPower = (basePlayerAttackPower + 
            (GameMgr.Instance.playerMgr.playerEnhance.attackPowerLevel * 1/*TO-DO ���̺��� ���������� �޾ƿ� ��ü*/)).ToString();
        playerMaxHealth = basePlayerMaxHealth.ToString(); // @@@@
        defence = basePlayerDefence; // @@@@@
        playerHealthRecovery = basePlayerHealthRecovery.ToString(); // @@@@@
        playerCriticalPercent = basePlayerCriticalPercent; // + @@
        playerCriticalMultiple = basePlayerCriticalMultiple; //+ @@

        speed = basePlayerSpeed; // @@
        attackSpeed = basePlayerAttackSpeed; // @@
        attackRange = basePlayerAttackRange; // @@

        foreach (var character in GameMgr.Instance.playerMgr.characters)
        {
            character.PlayerStatUpdate();
        }

    }
}
