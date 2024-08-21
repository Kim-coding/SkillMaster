using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : Status ,IDamageable
{
    private int bossId;

    public string dropGold;
    public string dropDia;

    public bool invincible { get; set; }

    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }
    public float Defence { get; set; }
    public void Init()
    {
        Ondeath = false;
        Health = new BigInteger(DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(bossId).Health);
        maxHealth = new BigInteger(Health);
        attackPower = new BigInteger(DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(bossId).Damage);
        attackSpeed = 1f;
        speed = 3f;
        attackRange = 3f;
        Defence = 0;
        dropGold = DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(bossId).GoldValue;
        dropDia = DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(bossId).DiaValue;
    }

    public void SetBossID(int a)
    {
        bossId = a;
    }

    public void UpdateHpBar()
    {
        if (Health > maxHealth)
        {
            Health = new BigInteger(maxHealth);
        }
        var boss = gameObject.GetComponent<BossAI>();

        float percent = 0f;

        if (maxHealth.factor - 1 > Health.factor)
        {
            percent = 0.1f;
        }
        else if (maxHealth.factor > Health.factor)
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

        boss.UpdateHpBar(percent);
    }
}
