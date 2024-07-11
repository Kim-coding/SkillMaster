using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Status
{
    PlayerStat currentPlayerStat;

    public int defence;

    public string playerHealthRecovery;

    public float playerCriticalPercent;
    public float playerCriticalMultiple;


    //���ǵ�, ���ݹ���, ���ݼӵ��� ��ũ���ͺ� ������Ʈ�� ��Ʈ�� 

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
}
