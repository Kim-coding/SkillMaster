using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Status, IDamageable
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

    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }



    //스피드, 공격범위, 공격속도는 스크립터블 오브젝트로 컨트롤 
    //base stat 6종 (공 방 체 체회 치확 치피) 포함

    private void Awake()
    {
        GameMgr.Instance.playerMgr.characters.Add(this);
        currentPlayerStat = GameMgr.Instance.playerMgr.playerStat;
        PlayerStatUpdate();

        Health = new BigInteger(maxHealth);
        Ondeath = false;
    }


    public void PlayerStatUpdate()
    {
        maxHealth = new BigInteger(currentPlayerStat.playerMaxHealth);
        attackPower = new BigInteger(currentPlayerStat.playerAttackPower);
        defence = currentPlayerStat.defence;
        playerHealthRecovery = currentPlayerStat.playerHealthRecovery;
        playerCriticalPercent = currentPlayerStat.playerCriticalPercent;
        playerCriticalMultiple = currentPlayerStat.playerCriticalMultiple;

        speed = currentPlayerStat.speed;
        attackSpeed = currentPlayerStat.attackSpeed;
        attackRange = currentPlayerStat.attackRange;
    }
}
