using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfomation
{

    public BigInteger monsterKill;
    public BigInteger getGold;
    public int stageClear;
    public int skillSpawnCount;

    public void Init()
    {
        monsterKill = new BigInteger(0); //TO-DO�����ѵ��� ������
        getGold = new BigInteger(0);
        skillSpawnCount = 0;
        //stageClear;

        EventMgr.StartListening(QuestType.Stage, StageUpdate);
        EventMgr.StartListening(QuestType.MonsterKill, MonsterKillUpdate);
        EventMgr.StartListening(QuestType.MergeSkill, SkillSpawnCountUpdate);
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
}
