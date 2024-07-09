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


    //스피드, 공격범위, 공격속도는 스크립터블 오브젝트로 컨트롤 

    private void Awake()
    {
        currentPlayerStat = GameMgr.Instance.playerMgr.playerStat;
        Init();
    }

    public void Init()
    {
        health = new BigInteger(currentPlayerStat.playerHealth);
        attackPower = new BigInteger(currentPlayerStat.playerAttackPower);
        playerCriticalPercent = currentPlayerStat.playerCriticalPercent;
        playerCriticalMultiple = currentPlayerStat.playerCriticalMultiple;
    }
}
