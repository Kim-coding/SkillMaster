using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStat
{
    public string playerAttackPower;
    public int basePlayerAttackPower;
    public float defence;
    public float basePlayerDefence;

    public string playerMaxHealth;
    public int basePlayerMaxHealth;
    public string playerHealthRecovery;
    public int basePlayerHealthRecovery;

    public float speed;
    public float basePlayerSpeed;
    public float attackRange;
    public float basePlayerAttackRange;
    public float cooldown;
    public float basePlayerCooldown;
    public float attackSpeed;
    public float basePlayerAttackSpeed;

    public float recoveryDuration;
    public float basePlayerRecoveryDuration;

    public float playerCriticalPercent;
    public float basePlayerCriticalPercent;
    public float playerCriticalMultiple;
    public float basePlayerCriticalMultiple;


    // 외형??
    //강화 수치같은것도 여기서 합산 해야될것 같다.

    public void Init(int baseAttackPower, int baseMaxHealth, 
        int baseDefence,int baseRecovery, float baseCriPer, 
        float baseCirMulti, float baseSpeed, float baseCooldown, float baseAttackSpeed
        , float baseAttackRange, float baseRecoveryDuration)
    {
        basePlayerAttackPower = baseAttackPower;
        basePlayerMaxHealth = baseMaxHealth;
        basePlayerDefence = baseDefence;
        basePlayerHealthRecovery = baseRecovery;

        basePlayerCriticalPercent = baseCriPer / 100f;
        basePlayerCriticalMultiple = baseCirMulti;

        basePlayerSpeed = baseSpeed;
        basePlayerCooldown = baseCooldown;
        basePlayerAttackSpeed = baseAttackSpeed;
        basePlayerAttackRange = baseAttackRange;

        basePlayerRecoveryDuration = baseRecoveryDuration;
        playerStatUpdate();
    }

    public void DebugStatSetting(float dSpeed, float dCooldown, float dAttackSpeed, float dAttackRange, float dRecoveryDuration)
    {
        basePlayerSpeed = dSpeed;
        basePlayerCooldown = dCooldown;
        basePlayerAttackSpeed = dAttackSpeed;
        basePlayerAttackRange = dAttackRange;
        basePlayerRecoveryDuration = dRecoveryDuration;
        playerStatUpdate();
    }


    public void playerStatUpdate()
    {
        playerAttackPower = ((basePlayerAttackPower + 
            (GameMgr.Instance.playerMgr.playerEnhance.attackPowerLevel * 1/*TO-DO 테이블에서 증가량으로 받아와 대체*/))
            * GameMgr.Instance.playerMgr.playerinventory.itemAttackPower)
            .ToString();
        
        playerMaxHealth = ((new BigInteger(basePlayerMaxHealth) +
            (new BigInteger(GameMgr.Instance.playerMgr.playerEnhance.maxHealthValue) *
            GameMgr.Instance.playerMgr.playerEnhance.maxHealthLevel))
            * GameMgr.Instance.playerMgr.playerinventory.itemMaxHealth)
            .ToString();

        defence = (basePlayerDefence +
            GameMgr.Instance.playerMgr.playerEnhance.defenceLevel *
            GameMgr.Instance.playerMgr.playerEnhance.defenceValue)
            * GameMgr.Instance.playerMgr.playerinventory.itemDeffence;

        playerHealthRecovery = ((new BigInteger(basePlayerHealthRecovery) +
                            (new BigInteger(GameMgr.Instance.playerMgr.playerEnhance.recoveryValue) *
                            GameMgr.Instance.playerMgr.playerEnhance.recoveryLevel))
                            * GameMgr.Instance.playerMgr.playerinventory.itemRecovery
                            )
                            .ToString();

        playerCriticalPercent = basePlayerCriticalPercent +
                        GameMgr.Instance.playerMgr.playerEnhance.criticalPercentLevel *
                        GameMgr.Instance.playerMgr.playerEnhance.criticalPercentValue;
        playerCriticalPercent += GameMgr.Instance.playerMgr.playerinventory.itemCriticalPercent;

        playerCriticalMultiple = basePlayerCriticalMultiple +
                        GameMgr.Instance.playerMgr.playerEnhance.criticalMultipleLevel * 
                        GameMgr.Instance.playerMgr.playerEnhance.criticalMultipleValue;
        playerCriticalMultiple += GameMgr.Instance.playerMgr.playerinventory.itemCriticalMultiple;


        speed = basePlayerSpeed + GameMgr.Instance.playerMgr.playerinventory.itemSpeed;
        cooldown = basePlayerCooldown; // @@
        attackRange = basePlayerAttackRange + GameMgr.Instance.playerMgr.playerinventory.itemAttackRange;
        attackSpeed = basePlayerAttackSpeed + GameMgr.Instance.playerMgr.playerinventory.itemAttackSpeed;

        recoveryDuration = basePlayerRecoveryDuration;

        foreach (var character in GameMgr.Instance.playerMgr.characters)
        {
            character.PlayerStatUpdate();
        }

    }
}
