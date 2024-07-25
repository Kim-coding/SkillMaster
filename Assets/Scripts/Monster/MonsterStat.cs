using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Status ,IDamageable
{
    private int monsterId;

    public string dropGold;
    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }
    public float Defence { get; set; }
    public void Init()
    {
        Ondeath = false;
        Health = new BigInteger(DataTableMgr.Get<MonsterTable>(DataTableIds.monster).GetID(monsterId).Health);
        attackPower = new BigInteger(DataTableMgr.Get<MonsterTable>(DataTableIds.monster).GetID(monsterId).Damage);
        attackSpeed = 2f;
        speed = 1f;
        attackRange = 1;
        Defence = 0;
        dropGold = DataTableMgr.Get<MonsterTable>(DataTableIds.monster).GetID(monsterId).GoldValue;
    }
    public void SetID(int a)
    {
        monsterId = a;
    }
}
