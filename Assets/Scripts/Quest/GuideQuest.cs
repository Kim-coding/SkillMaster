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

    private List<Action> eventSubscribers = new List<Action>();
    public void Init()
    {
        questID = 60001; //TO-DO 저장데이터
        currentQuest = new QuestData();
        currentQuest = DataTableMgr.Get<QuestTable>(DataTableIds.quest).GetID(questID);
        GameMgr.Instance.uiMgr.uiGuideQuest.currentQuest = currentQuest;
        currentTargetValue = 0; //TO-DO 저장데이터
        eventSubscribers.Add(QuestValueUpdate);
        eventSubscribers.Add(QuestGetGoldUpdate);
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
        CheckQuestCompletion();
        UiUpdate();
    }

    public void RegisterQuestEvents()
    {
        switch (currentQuest.Division)
        {
            case 1:
                AddEvent(QuestType.Stage, StageComparisonValue);
                StageComparisonValue();
                break;
            case 2:
                AddEvent(QuestType.AttackEnhance, QuestValueUpdate);
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


    public void StageComparisonValue()
    {
        if (GameMgr.Instance.playerMgr.playerInfo.stageClear >= GameMgr.Instance.sceneMgr.mainScene.stageCount)
        {
            currentTargetValue++;
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
        EventMgr.StopListening(QuestType.Stage, StageComparisonValue);
        EventMgr.StopListening(QuestType.AttackEnhance, QuestValueUpdate);
        EventMgr.StopListening(QuestType.MonsterKill, QuestValueUpdate);
        EventMgr.StopListening(QuestType.MergeSkill, QuestValueUpdate);
        EventMgr.StopListening(QuestType.GetGold, QuestGetGoldUpdate);
    }
}
