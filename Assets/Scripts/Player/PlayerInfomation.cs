using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfomation
{

    public BigInteger monsterKill;
    public BigInteger getGold;
    public int stageClear;
    public int skillSpawnCount;
    public int maxSkillLevel;

    public void Init()
    {
        monsterKill = new BigInteger(0); //TO-DO저장한데서 들고오기 밑에전부
        getGold = new BigInteger(0);
        skillSpawnCount = 0;
        maxSkillLevel = 1;
        //stageClear;

        EventMgr.StartListening(QuestType.Stage, StageUpdate);
        EventMgr.StartListening(QuestType.MonsterKill, MonsterKillUpdate);
        EventMgr.StartListening(QuestType.MergeSkillCount, SkillSpawnCountUpdate);
    }
    private void StageUpdate()
    {
        stageClear = GameMgr.Instance.sceneMgr.mainScene.stageCount;
        //TO-DO ui에 올려야됨
    }

    private void MonsterKillUpdate()
    {
        monsterKill += 1;
        //TO-DO ui에 올려야됨
    }

    private void SkillSpawnCountUpdate()
    {
        skillSpawnCount += 1;
        //TO-DO ui에 올려야됨
    }

    public void MaxSkillLevelUpdate(int SkillLevel)
    {
        if (SkillLevel <= maxSkillLevel)
        { return; }

        maxSkillLevel = SkillLevel;
        GameMgr.Instance.rewardMgr.guideQuest.MaxSkillComparisonCheck();
    }
}
