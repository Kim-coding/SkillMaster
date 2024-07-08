using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStat : Status
{
    public void Init()
    {
        AttackPower = 100;
        defense = 0;
        health = 0;
    }

}
