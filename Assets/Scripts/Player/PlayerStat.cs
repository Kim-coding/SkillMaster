using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStat : Status
{
    public float criticalPercent;
    public float criticalMultiple;
    public void Init()
    {
        AttackPower = "100000000000000000";
        defense = 0;
        health = 0;
        criticalPercent = 50f / 100f;
        criticalMultiple = 2f;
    }

}
