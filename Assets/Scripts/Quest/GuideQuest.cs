using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class GuideQuest
{
    public int questID;
    [HideInInspector]
    public QuestData currentQuest;
    public int currentTargetValue;

    private Action currentEventHandler;
    //private UiGuideQuest uiGuideQuest;
    public void Init()
    {

        if (GameMgr.Instance.uiMgr.uiGuideQuest == null)
        {
            return; 
        }

        if(SaveLoadSystem.CurrSaveData.savePlay != null)
        {
            var data = SaveLoadSystem.CurrSaveData.savePlay;
            questID = data.questID; //TO-DO 저장데이터
            currentTargetValue = data.questValue;
        }
        else
        {
            questID = 60001; //TO-DO 저장데이터
            currentTargetValue = 0;
        }

        currentQuest = DataTableMgr.Get<QuestTable>(DataTableIds.quest).GetID(questID);
        //uiGuideQuest = GameMgr.Instance.uiMgr.uiGuideQuest;
        GameMgr.Instance.uiMgr.uiGuideQuest.currentQuest = currentQuest;
        RegisterQuestEvents();
        CheckQuestCompletion();
        UiUpdate();

    }

    public void CheckQuestCompletion()
    {
        //if(questID == 60750)
        //{
        //    return;
        //}

        if (currentQuest.Targetvalue > currentTargetValue)
        {
            GameMgr.Instance.uiMgr.uiGuideQuest.UiButtonUpdate(false);
        }
        else
        {
            GameMgr.Instance.uiMgr.uiGuideQuest.UiButtonUpdate(true);
            RemoveEvent();
        }
    }

    public void NextQuest()
    {
        //if (questID == 60750)
        //{
        //    return;
        //}

        GameMgr.Instance.uiMgr.uiGuideQuest.UiButtonUpdate(false);
        GameMgr.Instance.soundMgr.PlaySFX("QuestClear");
        questID = currentQuest.Next_Quest;
        currentQuest = DataTableMgr.Get<QuestTable>(DataTableIds.quest).GetID(questID);
        GameMgr.Instance.uiMgr.uiGuideQuest.currentQuest = currentQuest;
        currentTargetValue = 0; //TO-DO 조건 확인하고 초기화}
        RegisterQuestEvents();
        CheckQuestCompletion();
        UiUpdate();
        SaveLoadSystem.Save();
    }

    public void RegisterQuestEvents()
    {
        switch (currentQuest.Division)
        {
            case 1:
                currentEventHandler = () => ComparisonValue(QuestType.Stage);
                AddEvent(QuestType.Stage, currentEventHandler);
                ComparisonValue(QuestType.Stage);
                break;
            case 2:
                AddEvent(QuestType.MonsterKill, QuestValueUpdate);
                break;
            case 3:
                ComparisonValue(QuestType.MaxSkillLevel);
                break;
            case 4:
                AddEvent(QuestType.MergeSkillCount, QuestValueUpdate);
                break;
            case 5:
                currentEventHandler = () => ComparisonValue(QuestType.AttackEnhance);
                AddEvent(QuestType.AttackEnhance, currentEventHandler);
                ComparisonValue(QuestType.AttackEnhance);
                break;
            case 6:
                currentEventHandler = () => ComparisonValue(QuestType.MaxHealthEnhance);
                AddEvent(QuestType.MaxHealthEnhance, currentEventHandler);
                ComparisonValue(QuestType.MaxHealthEnhance);
                break;
            case 7:
                currentEventHandler = () => ComparisonValue(QuestType.DefenceEnhance);
                AddEvent(QuestType.DefenceEnhance, currentEventHandler);
                ComparisonValue(QuestType.DefenceEnhance);
                break;
            case 8:
                currentEventHandler = () => ComparisonValue(QuestType.CriticalPercentEnhance);
                AddEvent(QuestType.CriticalPercentEnhance, currentEventHandler);
                ComparisonValue(QuestType.CriticalPercentEnhance);
                break;
            case 9:
                currentEventHandler = () => ComparisonValue(QuestType.CriticalMultipleEnhance);
                AddEvent(QuestType.CriticalMultipleEnhance, currentEventHandler);
                ComparisonValue(QuestType.CriticalMultipleEnhance);
                break;
            case 10:
                currentEventHandler = () => ComparisonValue(QuestType.RecoveryEnhance);
                AddEvent(QuestType.RecoveryEnhance, currentEventHandler);
                ComparisonValue(QuestType.RecoveryEnhance);
                break;
            case 11:
                currentEventHandler = () => ComparisonValue(QuestType.GoldEnhance);
                AddEvent(QuestType.GoldEnhance, currentEventHandler);
                ComparisonValue(QuestType.GoldEnhance);
                break;
            case 12:
                AddEvent(QuestType.GoldDungeon, QuestValueUpdate);
                break;
            case 13:
                AddEvent(QuestType.DiaDungeon, QuestValueUpdate);
                break;
            case 14:
                AddEvent(QuestType.PickUp, QuestValueUpdate);
                break;
            case 15:
                currentEventHandler = () => ComparisonValue(QuestType.MaxSkillCount);
                AddEvent(QuestType.MaxSkillCount, currentEventHandler);
                ComparisonValue(QuestType.MaxSkillCount);
                break;

        }
    }

    public void QuestValueUpdate()
    {
        currentTargetValue++;
        CheckQuestCompletion();
        UiUpdate();
    }

    public void MaxSkillComparisonCheck()
    {
        if(currentQuest.Division != (int)QuestType.MaxSkillLevel)
        {
            return;
        }
        ComparisonValue(QuestType.MaxSkillLevel);
    }
    public void StageComparisonCheck()
    {
        if (currentQuest.Division != (int)QuestType.Stage)
        {
            return;
        }
        ComparisonValue(QuestType.Stage);
    }

    public void ComparisonValue(QuestType quest)
    {
        switch(quest)
        {
            case QuestType.Stage:
                currentTargetValue = GameMgr.Instance.playerMgr.playerInfo.stageClear;
                break;
            case QuestType.MaxSkillLevel:
                currentTargetValue = GameMgr.Instance.playerMgr.playerInfo.maxSkillLevel;
                break;
            case QuestType.AttackEnhance:
                currentTargetValue = GameMgr.Instance.playerMgr.playerEnhance.attackPowerLevel;
                break;
            case QuestType.DefenceEnhance:
                currentTargetValue = GameMgr.Instance.playerMgr.playerEnhance.defenceLevel;
                break;
            case QuestType.MaxHealthEnhance:
                currentTargetValue = GameMgr.Instance.playerMgr.playerEnhance.maxHealthLevel;
                break;
            case QuestType.RecoveryEnhance:
                currentTargetValue = GameMgr.Instance.playerMgr.playerEnhance.recoveryLevel;
                break;
            case QuestType.CriticalPercentEnhance:
                currentTargetValue = GameMgr.Instance.playerMgr.playerEnhance.criticalPercentLevel;
                break;
            case QuestType.CriticalMultipleEnhance:
                currentTargetValue = GameMgr.Instance.playerMgr.playerEnhance.criticalMultipleLevel;
                break;
            case QuestType.GoldEnhance:
                currentTargetValue = GameMgr.Instance.playerMgr.playerEnhance.goldLevel;
                break;
            case QuestType.MaxSkillCount:
                currentTargetValue = GameMgr.Instance.playerMgr.playerEnhance.maxReserveSkillLevel;
                break;
        }
        if (currentTargetValue >= currentQuest.Targetvalue)
        {
            currentTargetValue = currentQuest.Targetvalue;
        }

        CheckQuestCompletion();
        UiUpdate();
    }


    public void UiUpdate()
    {
        GameMgr.Instance.uiMgr.uiGuideQuest.UiUpdate(currentTargetValue);
    }

    private void AddEvent(QuestType questType, Action subscriber)
    {
        EventMgr.StartListening(questType, subscriber);
    }

    private void RemoveEvent()
    {
        EventMgr.StopListening(QuestType.Stage, currentEventHandler);
        EventMgr.StopListening(QuestType.AttackEnhance, currentEventHandler);
        EventMgr.StopListening(QuestType.MaxHealthEnhance, currentEventHandler);
        EventMgr.StopListening(QuestType.DefenceEnhance, currentEventHandler);
        EventMgr.StopListening(QuestType.CriticalPercentEnhance, currentEventHandler);
        EventMgr.StopListening(QuestType.CriticalMultipleEnhance, currentEventHandler);
        EventMgr.StopListening(QuestType.RecoveryEnhance, currentEventHandler);
        EventMgr.StopListening(QuestType.GoldEnhance, currentEventHandler);
        EventMgr.StopListening(QuestType.MonsterKill, QuestValueUpdate);
        EventMgr.StopListening(QuestType.MergeSkillCount, QuestValueUpdate);
        EventMgr.StopListening(QuestType.GoldDungeon, currentEventHandler);
        EventMgr.StopListening(QuestType.DiaDungeon, currentEventHandler);
        EventMgr.StopListening(QuestType.PickUp, QuestValueUpdate);
        EventMgr.StopListening(QuestType.MaxSkillCount, currentEventHandler);
    }
}
