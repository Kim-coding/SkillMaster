using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Status
{
    PlayerStat currentPlayerStat;

    [HideInInspector]
    public int defence;

    [HideInInspector]
    public string playerHealthRecovery;

    [HideInInspector]
    public float playerCriticalPercent;

    [HideInInspector]
    public float playerCriticalMultiple;

   

    
    //���ǵ�, ���ݹ���, ���ݼӵ��� ��ũ���ͺ� ������Ʈ�� ��Ʈ�� 
    //base stat 6�� (�� �� ü üȸ ġȮ ġ��) ����

    private void Awake()
    {
        GameMgr.Instance.playerMgr.characters.Add(this);
        currentPlayerStat = GameMgr.Instance.playerMgr.playerStat;
        PlayerStatUpdate();
    }

    public void PlayerStatUpdate()
    {
        health = new BigInteger(currentPlayerStat.playerHealth);
        attackPower = new BigInteger(currentPlayerStat.playerAttackPower);
        playerCriticalPercent = currentPlayerStat.playerCriticalPercent;
        playerCriticalMultiple = currentPlayerStat.playerCriticalMultiple;
    }

    public void SetBaseStat()
    {

    }
}
