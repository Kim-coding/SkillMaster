using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Status ,IDamageable
{
    public BigInteger dropGold;
    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }
    public int Defence { get; set; }
    public void Init()
    {
        Ondeath = false;
        Health = new BigInteger(10);
        attackPower = new BigInteger(5);
        attackSpeed = 2f;
        speed = 1f;
        attackRange = 1;
        Defence = 0;
    }
}
