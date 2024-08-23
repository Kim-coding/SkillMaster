using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

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

    public Dictionary<int, SkillBookSaveData> skillBookDatas = new Dictionary<int, SkillBookSaveData>();
    public Dictionary<int, EquipBookSaveData> equipBookDatas = new Dictionary<int, EquipBookSaveData>();
    public Dictionary<int, bool> SetDatas = new Dictionary<int, bool>();

    public float attackPowerSetOption;
    public float criticalPercentSetOption;
    public float maxHealthSetOption;
    public float criticalMultipleSetOption;
    public float deffenceSetOption;
    public float recoverySetOption;

    public void Init()
    {
        if (SaveLoadSystem.CurrSaveData.savePlay != null)
        {
            var data = SaveLoadSystem.CurrSaveData.savePlay.savePlayerInfomation;
            monsterKill = new BigInteger(data.monsterKill); //TO-DO저장한데서 들고오기 밑에전부
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
                skillBookDatas.Add(item.Key, item.Value);
            }

            foreach (var item in data.equipBookDatas)
            {
                equipBookDatas.Add(item.Key, item.Value);
            }

            foreach (var item in data.SetDatas)
            {
                SetDatas.Add(item.Key, item.Value);
            }

        }
        else
        {
            monsterKill = new BigInteger(0); //TO-DO저장한데서 들고오기 밑에전부
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

            var skilldata = DataTableMgr.Get<SkillBookTable>(DataTableIds.skillBook).skillBookDatas;

            foreach (var item in skilldata)
            {
                SkillBookSaveData savedata = new SkillBookSaveData();
                savedata.skillID = item.skill_equipment_id;
                savedata.state = ClearState.NotAcquired;
                skillBookDatas.Add(item.skill_equipment_id, savedata);
            }

            var equipdata = DataTableMgr.Get<EquipBookTable>(DataTableIds.equipBook).equipBookDatas;

            foreach (var item in equipdata)
            {
                int[] equipmentIds = { item.equipment1_id, item.equipment2_id, item.equipment3_id, item.equipment4_id, item.equipment5_id };

                foreach (int equipID in equipmentIds)
                {
                    EquipBookSaveData saveData = new EquipBookSaveData
                    {
                        equipID = equipID,
                        state = ClearState.NotAcquired
                    };

                    equipBookDatas.Add(equipID, saveData);
                }

                SetDatas.Add(item.equipbook_id, false);
            }

        }
        //stageClear;
        SetOptionUpdate();
        EventMgr.StartListening(QuestType.Stage, StageUpdate);
        EventMgr.StartListening(QuestType.MonsterKill, MonsterKillUpdate);
        EventMgr.StartListening(QuestType.MergeSkillCount, SkillSpawnCountUpdate);
        EventMgr.StartListening(QuestType.PickUp, ObtainedItemUpdate);
    }
    private void StageUpdate()
    {
        stageClear = GameMgr.Instance.sceneMgr.mainScene.stageCount;
        //TO-DO ui에 올려야됨
    }

    public void ObtainedItemUpdate()
    {
        obtainedItem += 1;
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
        if (gachaExp >= gachaMaxExp && gachaMaxExp != -1)
        {
            GameMgr.Instance.rewardMgr.SetReward(DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(gachaLevel).gachaRequestValue);
            gachaLevel++;
            gachaExp -= gachaMaxExp;

            gachaMaxExp = DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(gachaLevel).gachaRequestValue;
        }
        GameMgr.Instance.uiMgr.uiWindow.pickUpWindow.GetComponent<PickUp>().UIUpdate(gachaLevel, gachaExp, gachaMaxExp, DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(gachaLevel).gachaRequestValue);
    }

    public void SetOptionUpdate()
    {
        attackPowerSetOption = 0;
        criticalPercentSetOption = 0;
        maxHealthSetOption = 0;
        criticalMultipleSetOption = 0;
        deffenceSetOption = 0;
        recoverySetOption = 0;

        foreach (var item in GameMgr.Instance.uiMgr.uiBook.setDic)
        {
            if (!item.Value.getReward)
            {
                continue;
            }

            switch (item.Value.setOptionType)
            {
                case 1:
                    attackPowerSetOption += item.Value.setOptionValue;
                    break;
                case 2:
                    criticalPercentSetOption += item.Value.setOptionValue;
                    break;
                case 3:
                    maxHealthSetOption += item.Value.setOptionValue;
                    break;
                case 4:
                    criticalMultipleSetOption += item.Value.setOptionValue;
                    break;
                case 5:
                    deffenceSetOption += item.Value.setOptionValue;
                    break;
                case 6:
                    recoverySetOption += item.Value.setOptionValue;
                    break;
            }
        }
        attackPowerSetOption = 1 + (attackPowerSetOption / 100f);
        criticalPercentSetOption = 1 + (criticalPercentSetOption / 100f);
        maxHealthSetOption = 1 + (maxHealthSetOption / 100f);
        criticalMultipleSetOption = 1 + (criticalMultipleSetOption / 100f);
        deffenceSetOption = 1 + (deffenceSetOption / 100f);
        recoverySetOption = 1 + (recoverySetOption / 100f);
        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }
}
