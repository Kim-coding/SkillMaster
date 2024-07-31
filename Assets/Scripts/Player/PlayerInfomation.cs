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
    public int obtainedItem;

    public int gachaLevel;
    public int gachaExp;
    public int gachaMaxExp;
    public void Init()
    {
        monsterKill = new BigInteger(0); //TO-DO�����ѵ��� ������ �ؿ�����
        getGold = new BigInteger(0);
        skillSpawnCount = 0;
        maxSkillLevel = 1;
        obtainedItem = 0;
        gachaLevel = 1;
        gachaExp = 0;
        //stageClear;

        EventMgr.StartListening(QuestType.Stage, StageUpdate);
        EventMgr.StartListening(QuestType.MonsterKill, MonsterKillUpdate);
        EventMgr.StartListening(QuestType.MergeSkillCount, SkillSpawnCountUpdate);
    }
    private void StageUpdate()
    {
        stageClear = GameMgr.Instance.sceneMgr.mainScene.stageCount;
        //TO-DO ui�� �÷��ߵ�
    }

    private void MonsterKillUpdate()
    {
        monsterKill += 1;
        //TO-DO ui�� �÷��ߵ�
    }

    private void SkillSpawnCountUpdate()
    {
        skillSpawnCount += 1;
        //TO-DO ui�� �÷��ߵ�
    }

    public void MaxSkillLevelUpdate(int SkillLevel)
    {
        if (SkillLevel <= maxSkillLevel)
        { return; }

        maxSkillLevel = SkillLevel;
        GameMgr.Instance.rewardMgr.guideQuest.MaxSkillComparisonCheck();
    }
}
