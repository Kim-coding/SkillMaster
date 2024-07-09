using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Status
{
    PlayerStat playerStat = new PlayerStat();

    private void Awake()
    {
        playerStat = GameMgr.Instance.playerMgr.playerStat;
    }

    public float playerCriticalPercent;
    public float playerCriticalMultiple;

    public void Init()
    {
        health = new BigInteger(playerStat.playerHealth);
        attackPower = new BigInteger(playerStat.playerAttackPower);
        playerCriticalPercent = playerStat.playerCriticalPercent;
        playerCriticalMultiple = playerStat.playerCriticalMultiple;
    }
}
