using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStat : Status
{
    private void Start()
    {
        AttackPower = 1;
        defense = 0;
        health = 0;
    }

}
