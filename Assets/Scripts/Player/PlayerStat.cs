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


    // ����??
    //��ȭ ��ġ�����͵� ���⼭ �ջ� �ؾߵɰ� ����.

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
        �⺻ ������ * ( 100 + ���� x �ۼ�Ʈ / 100 ) ���� �ص�
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
