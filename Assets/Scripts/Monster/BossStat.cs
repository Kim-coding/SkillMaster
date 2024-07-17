using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : Status ,IDamageable
{
    public BigInteger dropGold;
    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }

    public void Init()
    {
        Ondeath = false;
        Health = new BigInteger(100);
        attackPower = new BigInteger(5);
        attackSpeed = 1f;
        speed = 3f;
        attackRange = 3f;
    }
}
