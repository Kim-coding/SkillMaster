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

        basePlayerCriticalPercent = baseCriPer;
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
        var at = new BigInteger(basePlayerAttackPower);
        at += new BigInteger(GameMgr.Instance.playerMgr.playerEnhance.attackPowerValue);
        at *= (1 + GameMgr.Instance.playerMgr.playerinventory.itemAttackPower / 100f);
        at *= GameMgr.Instance.playerMgr.playerInfo.attackPowerSetOption;

        playerAttackPower = at.ToString();
        
        playerMaxHealth = (((new BigInteger(basePlayerMaxHealth) +
            (new BigInteger(GameMgr.Instance.playerMgr.playerEnhance.maxHealthValue) *
            GameMgr.Instance.playerMgr.playerEnhance.maxHealthLevel))
            * (1 + GameMgr.Instance.playerMgr.playerinventory.itemMaxHealth / 100f))
            * GameMgr.Instance.playerMgr.playerInfo.maxHealthSetOption)
            .ToString();

        defence = ((basePlayerDefence +
            GameMgr.Instance.playerMgr.playerEnhance.defenceLevel *
            GameMgr.Instance.playerMgr.playerEnhance.defenceValue)
            * (1 + GameMgr.Instance.playerMgr.playerinventory.itemDeffence / 100f) 
            * GameMgr.Instance.playerMgr.playerInfo.deffenceSetOption);

        playerHealthRecovery = (((new BigInteger(basePlayerHealthRecovery) +
                            (new BigInteger(GameMgr.Instance.playerMgr.playerEnhance.recoveryValue) *
                            GameMgr.Instance.playerMgr.playerEnhance.recoveryLevel))
                            * (1 + GameMgr.Instance.playerMgr.playerinventory.itemRecovery / 100f)
                            * GameMgr.Instance.playerMgr.playerInfo.recoverySetOption))
                            .ToString();

        playerCriticalPercent = basePlayerCriticalPercent +
                        GameMgr.Instance.playerMgr.playerEnhance.criticalPercentLevel *
                        GameMgr.Instance.playerMgr.playerEnhance.criticalPercentValue;
        playerCriticalPercent += GameMgr.Instance.playerMgr.playerinventory.itemCriticalPercent;
        playerCriticalPercent *= GameMgr.Instance.playerMgr.playerInfo.criticalPercentSetOption;

        playerCriticalMultiple = basePlayerCriticalMultiple +
                        GameMgr.Instance.playerMgr.playerEnhance.criticalMultipleLevel * 
                        GameMgr.Instance.playerMgr.playerEnhance.criticalMultipleValue;
        playerCriticalMultiple += GameMgr.Instance.playerMgr.playerinventory.itemCriticalMultiple;
        playerCriticalMultiple *= GameMgr.Instance.playerMgr.playerInfo.criticalMultipleSetOption;


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
