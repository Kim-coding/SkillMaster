using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Status, IDamageable
{
    PlayerStat currentPlayerStat;

    private float recoveryTimer = 0f;

    [HideInInspector]
    public int defence;

    [HideInInspector]
    public string playerHealthRecovery;

    [HideInInspector]
    public float playerCriticalPercent;

    [HideInInspector]
    public float playerCriticalMultiple;

    [HideInInspector]
    public float playerRecoveryDuration;

    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }



    //스피드, 공격범위, 공격속도, 회복쿨타임은 스크립터블 오브젝트로 컨트롤 
    //base stat 6종 (공 방 체 체회 치확 치피) 포함

    private void Awake()
    {
        GameMgr.Instance.playerMgr.characters.Add(this);
        currentPlayerStat = GameMgr.Instance.playerMgr.playerStat;
        PlayerStatUpdate();

        Health = new BigInteger(maxHealth);
        Ondeath = false;
    }

    private void Update()
    {
        if (Ondeath || playerHealthRecovery == "0")
        {
            return;
        }
        recoveryTimer += Time.deltaTime;
        if (recoveryTimer > playerRecoveryDuration)
        {
            recoveryTimer = 0f;
            var recoveryValue = new BigInteger(playerHealthRecovery);
            Health += recoveryValue;

            var displayPos = transform.position;
            displayPos.y += 2f;

            string text = recoveryValue.ToStringShort();
            Color color = Color.green;
            float fontSize = 10f;

            GameMgr.Instance.sceneMgr.damageTextMgr.ShowDamageText(displayPos, text, color, fontSize);

            if (Health > maxHealth)
            {
                Health = new BigInteger(maxHealth);
            }

            var player = gameObject.GetComponent<PlayerAI>();
            if (maxHealth.factor > recoveryValue.factor)
            {
                return;
            }
            else if (maxHealth.factor < recoveryValue.factor)
            {
                player.UpdateHpBar(1f);
            }
            else
            {
                float percent =
                (float)Health.numberList[Health.factor - 1]
                / maxHealth.numberList[maxHealth.factor - 1];
                player.UpdateHpBar(percent);
            }

        }
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

        playerRecoveryDuration = currentPlayerStat.recoveryDuration;
    }
}
