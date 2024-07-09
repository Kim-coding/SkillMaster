using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Status
{
    public void Init()
    {
        health = new BigInteger(100);
        attackPower = new BigInteger(250);
    }
}
