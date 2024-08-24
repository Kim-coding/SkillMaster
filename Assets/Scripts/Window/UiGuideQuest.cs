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
    public GameObject clearText;
    public Image rewardImage;
    public TextMeshProUGUI rewardCount;

    private UIMgr uiMgr;

    public void UiUpdate(int currentValue)
    {
        if(currentQuest.Targetvalue == -1)
        {
            gameObject.SetActive(false);
        }
        var questData = DataTableMgr.Get<QuestTable>(DataTableIds.quest).GetID(currentQuest.QuestID);
        questName.text = "Äù½ºÆ® "+ questData.Level + " " + questData.GetStringID;
        questCount.text = currentValue.ToString() + " / " + currentQuest.Targetvalue.ToString();
        rewardImage.sprite = DataTableMgr.Get<StuffTable>(DataTableIds.stuff).GetID(currentQuest.reward).Geticon;
        rewardCount.text = currentQuest.rewardvalue.ToString();
    }

    public void UiButtonUpdate(bool a)
    {
        guideQuestButton.onClick.RemoveAllListeners();
        if (a)
        {
            var value = new BigInteger(currentQuest.rewardvalue);
            if(currentQuest.reward == 220001)
            {
                guideQuestButton.onClick.AddListener(() => { GameMgr.Instance.playerMgr.currency.AddGold(value); });
            }
            else if (currentQuest.reward == 220002)
            {
                guideQuestButton.onClick.AddListener(() => { GameMgr.Instance.playerMgr.currency.AddDia(value); });
            }
            else
            {
                guideQuestButton.onClick.AddListener(() => {
                    GameMgr.Instance.playerMgr.playerinventory.CreateItem(currentQuest.reward, currentQuest.rewardvalue, ItemType.misc); });
            }

            guideQuestButton.onClick.AddListener(GameMgr.Instance.rewardMgr.guideQuest.NextQuest);
            clearText.SetActive(true);
        }
        else
        {
            GameMgr.Instance.uiMgr.uiWindow.UnLock();
            GameMgr.Instance.uiMgr.uiMerge.UnLockAutoButton();
            clearText.SetActive(false);
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
                    guideQuestButton.onClick.AddListener(() => {
                        if (GameMgr.Instance.uiMgr.uiWindow.CurrentOpenWindow == Windows.Enhance)
                        { return; }
                        GameMgr.Instance.uiMgr.uiWindow.EnhanceWindowOpen();
                        GameMgr.Instance.uiMgr.uiEnhance.enhanceModes[0].isOn = true;
                        GameMgr.Instance.uiMgr.uiEnhance.onToggleValueChange(true);
                    });

                    break;
                case 15:
                    guideQuestButton.onClick.AddListener(() => {if(GameMgr.Instance.uiMgr.uiWindow.CurrentOpenWindow == Windows.Enhance)
                        { return; }
                        GameMgr.Instance.uiMgr.uiWindow.EnhanceWindowOpen(); 
                    GameMgr.Instance.uiMgr.uiEnhance.enhanceModes[1].isOn = true;
                        GameMgr.Instance.uiMgr.uiEnhance.onToggleValueChange(true);
                    });
                    break;
                case 12:
                case 13:
                    guideQuestButton.onClick.AddListener(() => {
                        if (GameMgr.Instance.uiMgr.uiWindow.CurrentOpenWindow == Windows.Dungeon)
                        { return; }
                        GameMgr.Instance.uiMgr.uiWindow.DungeonWindowOpen();
                    });
                    break;
                case 14:
                    guideQuestButton.onClick.AddListener(() => {
                        if (GameMgr.Instance.uiMgr.uiWindow.CurrentOpenWindow == Windows.PickUp)
                        { return; }
                        GameMgr.Instance.uiMgr.uiWindow.PickUpWindowOpen();
                    });
                    break;

            }
        }
    }
}
