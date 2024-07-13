using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Status
{
    public BigInteger dropGold;
    public void Init()
    {
        health = new BigInteger(10);
        attackPower = new BigInteger(2);
        attackRange = 1;
    }
}
