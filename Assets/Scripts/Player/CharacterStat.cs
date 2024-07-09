using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Status
{
    PlayerStat currentPlayerStat;

    private void Awake()
    {
        currentPlayerStat = GameMgr.Instance.playerMgr.playerStat;
    }

    public float playerCriticalPercent;
    public float playerCriticalMultiple;

    public void Init()
    {
        health = new BigInteger(currentPlayerStat.playerHealth);
        attackPower = new BigInteger(currentPlayerStat.playerAttackPower);
        playerCriticalPercent = currentPlayerStat.playerCriticalPercent;
        playerCriticalMultiple = currentPlayerStat.playerCriticalMultiple;
    }
}
