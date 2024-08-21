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

    public int goldDungeonLv;
    public int diaDungeonLv;
    public bool dungeonMode;

    public Dictionary<int,SkillBookSaveData> skillBookDatas = new Dictionary<int,SkillBookSaveData>();

    public void Init()
    {
        if(SaveLoadSystem.CurrSaveData.savePlay != null)
        {
            var data = SaveLoadSystem.CurrSaveData.savePlay.savePlayerInfomation;
            monsterKill = new BigInteger(data.monsterKill); //TO-DO�����ѵ��� ������ �ؿ�����
            getGold = new BigInteger(data.getGold);
            skillSpawnCount = data.skillSpawnCount;
            maxSkillLevel = data.maxSkillLevel;
            obtainedItem = data.obtainedItem;
            gachaLevel = data.gachaLevel;
            gachaExp = data.gachaExp;
            gachaMaxExp = DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(gachaLevel).gachaRequestValue;
            goldDungeonLv = data.goldDungeonLv;
            diaDungeonLv = data.diaDungeonLv;
            dungeonMode = data.dungeonMode;

            foreach (var item in data.skillBookDatas)
            {
                skillBookDatas.Add(item.Key,item.Value);
            }


        }
        else
        {
            monsterKill = new BigInteger(0); //TO-DO�����ѵ��� ������ �ؿ�����
            getGold = new BigInteger(0);
            skillSpawnCount = 0;
            maxSkillLevel = 0;
            obtainedItem = 0;
            gachaLevel = 1;
            gachaExp = 0;
            gachaMaxExp = DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(gachaLevel).gachaRequestValue;
            goldDungeonLv = 1;
            diaDungeonLv = 1;
            dungeonMode = true;

            var data = DataTableMgr.Get<SkillBookTable>(DataTableIds.skillBook).skillBookDatas;

            foreach (var item in data)
            {
                SkillBookSaveData savedata = new SkillBookSaveData();
                savedata.skillID = item.skill_equipment_id;
                savedata.state = ClearState.NotAcquired;
                skillBookDatas.Add(item.skill_equipment_id, savedata);
            }

        }
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
        if (SkillLevel <= maxSkillLevel || GameMgr.Instance.rewardMgr.guideQuest == null)
            return;

        maxSkillLevel = SkillLevel;
        AcquiredUpdate(SkillLevel + 40000, ClearState.Acquired);
        GameMgr.Instance.rewardMgr.guideQuest.MaxSkillComparisonCheck();
    }

    public void AcquiredUpdate(int skillLv, ClearState state)
    {
        var skillbookElement = skillBookDatas[skillLv];
        skillbookElement.state = state;
        GameMgr.Instance.uiMgr.uiBook.skillBookDic[skillbookElement.skillID].saveData.state = state;
        GameMgr.Instance.uiMgr.uiBook.skillBookDic[skillbookElement.skillID].AcquiredCheck();
    }

    public void GetGachaExp(int i)
    {
        gachaExp += i;
        if(gachaExp >= gachaMaxExp && gachaMaxExp != -1)
        {
            GameMgr.Instance.rewardMgr.SetReward(DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(gachaLevel).gachaRequestValue);
            gachaLevel++;
            gachaExp -= gachaMaxExp;

            gachaMaxExp = DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(gachaLevel).gachaRequestValue;
        }
        GameMgr.Instance.uiMgr.uiWindow.pickUpWindow.GetComponent<PickUp>().UIUpdate(gachaLevel, gachaExp, gachaMaxExp, DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(gachaLevel).gachaRequestValue);
    }
}
