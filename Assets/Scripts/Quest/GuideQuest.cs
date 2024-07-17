using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class GuideQuest
{
    private int questID;
    [HideInInspector]
    public QuestData currentQuest;
    public int currentTargetValue;

    private Action currentEventHandler;
    public void Init()
    {
        questID = 60001; //TO-DO 저장데이터
        currentQuest = new QuestData();
        currentQuest = DataTableMgr.Get<QuestTable>(DataTableIds.quest).GetID(questID);
        GameMgr.Instance.uiMgr.uiGuideQuest.currentQuest = currentQuest;
        currentTargetValue = 0; //TO-DO 저장데이터
        RegisterQuestEvents();
        CheckQuestCompletion();
        UiUpdate();

    }

    public void CheckQuestCompletion()
    {
        if (currentQuest.Targetvalue > currentTargetValue) { return; }
        GameMgr.Instance.uiMgr.uiGuideQuest.UiButtonUpdate(true);
        RemoveEvent();
    }

    public void NextQuest()
    {
        GameMgr.Instance.uiMgr.uiGuideQuest.UiButtonUpdate(false);
        questID = currentQuest.nextQuest;
        currentQuest = DataTableMgr.Get<QuestTable>(DataTableIds.quest).GetID(questID);
        GameMgr.Instance.uiMgr.uiGuideQuest.currentQuest = currentQuest;
        currentTargetValue = 0; //TO-DO 조건 확인하고 초기화}
        RegisterQuestEvents();
        UiUpdate();
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
                currentEventHandler = () => ComparisonValue(QuestType.AttackEnhance);
                AddEvent(QuestType.AttackEnhance, currentEventHandler);
                ComparisonValue(QuestType.AttackEnhance);
                break;
            case 3:
                AddEvent(QuestType.MonsterKill, QuestValueUpdate);
                break;
            case 4:
                AddEvent(QuestType.MergeSkill, QuestValueUpdate);
                break;
            case 5:
                AddEvent(QuestType.GetGold, QuestGetGoldUpdate);
                break;
        }
    }

    public void QuestValueUpdate()
    {
        currentTargetValue++;
        CheckQuestCompletion();
        UiUpdate();
    }

    public void QuestGetGoldUpdate()
    {
        currentTargetValue++;
        CheckQuestCompletion();
        UiUpdate();
    }


    //public void StageComparisonValue()
    //{
    //    currentTargetValue = GameMgr.Instance.playerMgr.playerInfo.stageClear;
    //    if (currentTargetValue > currentQuest.Targetvalue)
    //    {
    //        currentTargetValue = currentQuest.Targetvalue;
    //    }
    //    CheckQuestCompletion();
    //    UiUpdate();
    //}

    public void ComparisonValue(QuestType quest)
    {
        switch(quest)
        {
            case QuestType.Stage:
                currentTargetValue = GameMgr.Instance.playerMgr.playerInfo.stageClear;
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
        EventMgr.StopListening(QuestType.MonsterKill, QuestValueUpdate);
        EventMgr.StopListening(QuestType.MergeSkill, QuestValueUpdate);
        EventMgr.StopListening(QuestType.GetGold, QuestGetGoldUpdate);
    }
}
