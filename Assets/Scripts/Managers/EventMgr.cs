using System;
using System.Collections;
using System.Collections.Generic;
public static class EventMgr
{
    private static Dictionary<QuestType, Action> eventDictionary = new Dictionary<QuestType, Action>();
    public static void StartListening(QuestType questType, Action listener)
    {
        if (eventDictionary.TryGetValue(questType, out Action thisEvent))
        {
            thisEvent += listener;
            eventDictionary[questType] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            eventDictionary.Add(questType, thisEvent);
        }
    }

    public static void StopListening(QuestType questType  , Action listener)
    {
        if (eventDictionary.TryGetValue(questType, out Action thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[questType] = thisEvent;
        }
    }

    public static void TriggerEvent(QuestType questType)
    {
        if (eventDictionary.TryGetValue(questType, out Action thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
}
