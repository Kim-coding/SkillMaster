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
        if(currentQuest.Targetvalue == -1)
        {
            gameObject.SetActive(false);
        }
        questName.text = (DataTableMgr.Get<QuestTable>(DataTableIds.quest).GetID(currentQuest.QuestID).GetStringID);
        questCount.text = currentValue.ToString() + " / " + currentQuest.Targetvalue.ToString();
    }

    public void UiButtonUpdate(bool a)
    {
        guideQuestButton.onClick.RemoveAllListeners();
        if (a)
        {
            guideQuestButton.onClick.AddListener(GameMgr.Instance.rewardMgr.guideQuest.NextQuest);
        }
        else
        {
            switch (currentQuest.Division)
            {
                case 1:
                case 2:
                    break;
                case 3:
                case 4:
                    guideQuestButton.onClick.AddListener(GameMgr.Instance.uiMgr.uiWindow.AnimateCloseCurrentWindow);
                    guideQuestButton.onClick.AddListener(GameMgr.Instance.uiMgr.uiWindow.AnimateOpenMergeWindow);
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    guideQuestButton.onClick.AddListener(GameMgr.Instance.uiMgr.uiWindow.EnhanceWindowOpen);
                    break;
            }
        }
    }
}
