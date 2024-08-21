using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Status ,IDamageable
{
    private int monsterId;

    public string dropGold;
    public string asset1;
    public string asset2;

    public bool invincible { get; set; }

    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }
    public float Defence { get; set; }
    public void Init()
    {
        var monsterTable = DataTableMgr.Get<MonsterTable>(DataTableIds.monster).GetID(monsterId);
        Ondeath = false;
        Health = new BigInteger(monsterTable.Health);
        attackPower = new BigInteger(monsterTable.Damage);
        attackSpeed = 2f;
        speed = 1f;
        attackRange = 1;
        Defence = 0;
        dropGold = monsterTable.GoldValue;
        asset1 = monsterTable.Asset1;
        asset2 = monsterTable.Asset2;
    }
    public void SetID(int a)
    {
        monsterId = a;
    }
}
