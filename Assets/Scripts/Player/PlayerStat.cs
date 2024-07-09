using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStat
{
    public string playerAttackPower;
    public string playerHealth;

    public float playerCriticalPercent;
    public float playerCriticalMultiple;
    public void Init()
    {
        playerAttackPower = "100000000000000000";
        playerHealth = "1000";
        playerCriticalPercent = 50f / 100f;
        playerCriticalMultiple = 2f;
    }

}
