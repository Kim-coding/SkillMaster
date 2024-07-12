using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnhance
{
    public int maxReserveSkillCount;
    public int maxSpawnSkillCount;
    public int currentSpawnSkillCount;

    public int attackPowerLevel;
    public int attackPowerPercent;
    public BigInteger attackPowerCost;

    public int attackspeedUpgrade; //TO-DO level / percent / cost 세트로 있어야 함
    public int speedUpgrade;
    public int defenceUpgrade;


    public void Init()
    {
        //세이브 로드시 가져와야 함

        maxReserveSkillCount = 20;
        maxSpawnSkillCount = 14;
        currentSpawnSkillCount = maxSpawnSkillCount;
        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();

        attackPowerLevel = 0;
        attackPowerPercent = 10; //TO-DO 테이블에서
        attackPowerCost = new BigInteger(100 + 100 * attackPowerLevel); //TO-DO 식 넣어두어야함

        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate();

    }

    public void AddAttackPower()
    {
        attackPowerLevel++;
        attackPowerCost = new BigInteger(100 + 100 * attackPowerLevel);
        GameMgr.Instance.uiMgr.uiEnhance.TextUpdate();
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }

}
