using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStat
{
    public string playerAttackPower;
    public int defence;

    public string playerHealth;
    public string playerMaxHealth;
    public string playerHealthRecovery;

    public float speed;
    public float attackRange;
    public float attackSpeed;

    public float playerCriticalPercent;
    public float playerCriticalMultiple;


    // 외형??
    //강화 수치같은것도 여기서 합산 해야될것 같다.

    public void Init()
    {
        playerAttackPower = "30";
        playerHealth = "1000";
        playerCriticalPercent = 50f / 100f;
        playerCriticalMultiple = 2f;
    }


    public void playerStatUpdate()
    {
        /*
        기본 데미지 * ( 100 + 레벨 x 퍼센트 / 100 ) 으로 해둠
        */
        playerAttackPower = (new BigInteger(30) * (
            ( 100f + 
            (GameMgr.Instance.playerMgr.playerEnhance.attackPowerLevel * GameMgr.Instance.playerMgr.playerEnhance.attackPowerPercent)
            ) / 100f )
            ).ToString();

        //defence;

        //playerHealth;
        //playerMaxHealth;
        //playerHealthRecovery;

        //speed;
        //attackRange;
        //attackSpeed;

        //playerCriticalPercent;
        //playerCriticalMultiple;

        foreach(var character in GameMgr.Instance.playerMgr.characters)
        {
            character.PlayerStatUpdate();
        }

    }
}
