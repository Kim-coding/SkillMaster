using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnhance
{
    public int maxSpawnCount;
    public int attackPowerUpgrade;
    public int attackspeedUpgrade;
    public int speedUpgrade;
    public int defenceUpgrade;


    public void Init()
    {
        maxSpawnCount = 15;
        GameMgr.Instance.uiMgr.SkillCountUpdate();
    }

}
