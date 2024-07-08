using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public BigInteger health;
    public string damage;
    public void Init()
    {
        health = new BigInteger();
        health.Init(100);
        damage = "250";
    }
}
