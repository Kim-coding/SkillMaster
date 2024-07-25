using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Status, IDamageable
{
    PlayerStat currentPlayerStat;

    private float recoveryTimer = 0f;
    public float cooldown;


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
    public float Defence { get; set; }


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

            UpdateHpBar();

        }
    }


    public void UpdateHpBar()
    {
        if (Health > maxHealth)
        {
            Health = new BigInteger(maxHealth);
        }
        var player = gameObject.GetComponent<PlayerAI>();

        float percent = 0f;

        if (maxHealth.factor - 1 > Health.factor)
        {
            percent = 0.1f;
        }
        else if(maxHealth.factor > Health.factor)
        {
            float max = maxHealth.numberList[maxHealth.factor - 1] * 1000 + maxHealth.numberList[maxHealth.factor - 2];
            float health = Health.numberList[Health.factor - 1];
            percent = health / max;
        }
        else if (maxHealth.factor < Health.factor)
        {
            percent = 1f;
        }
        else
        {
            percent =
            (float)Health.numberList[Health.factor - 1]
            / maxHealth.numberList[maxHealth.factor - 1];
        }

        player.UpdateHpBar(percent);
    }


    public void PlayerStatUpdate()
    {
        maxHealth = new BigInteger(currentPlayerStat.playerMaxHealth);
        attackPower = new BigInteger(currentPlayerStat.playerAttackPower);
        Defence = currentPlayerStat.defence;
        playerHealthRecovery = currentPlayerStat.playerHealthRecovery;
        playerCriticalPercent = currentPlayerStat.playerCriticalPercent;
        playerCriticalMultiple = currentPlayerStat.playerCriticalMultiple;

        speed = currentPlayerStat.speed;
        cooldown = currentPlayerStat.cooldown;
        attackRange = currentPlayerStat.attackRange;
        attackSpeed = currentPlayerStat.attackSpeed;
        playerRecoveryDuration = currentPlayerStat.recoveryDuration;
    }
}
