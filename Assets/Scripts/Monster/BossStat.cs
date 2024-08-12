using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : Status ,IDamageable
{
    private int bossId;

    public string dropGold;    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }
    public float Defence { get; set; }
    public void Init()
    {
        Ondeath = false;
        Health = new BigInteger(DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(bossId).Health);
        attackPower = new BigInteger(DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(bossId).Damage);
        attackSpeed = 1f;
        speed = 3f;
        attackRange = 3f;
        Defence = 0;
        dropGold = DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(bossId).GoldValue;
    }
    public void SetBossID(int a)
    {
        bossId = a;
    }
}
