using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiGuideQuest : MonoBehaviour
{
    public Button guideQuestButton;
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questCount;
    public QuestData currentQuest;


    public void UiUpdate(int currentValue)
    {
        questName.text = currentQuest.Textid.ToString();
        questCount.text = currentValue.ToString() + " / " + currentQuest.Targetvalue.ToString();
    }

    public void UiButtonUpdate(bool a)
    {
        if (a)
        {
            guideQuestButton.onClick.AddListener(GameMgr.Instance.rewardMgr.guideQuest.NextQuest);
        }
        else
        {
            guideQuestButton.onClick.RemoveListener(GameMgr.Instance.rewardMgr.guideQuest.NextQuest);
        }
    }
}
