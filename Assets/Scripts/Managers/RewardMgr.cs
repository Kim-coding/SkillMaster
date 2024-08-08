using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardMgr : MonoBehaviour
{
    //public PlayerCurrency currency;  TO-DO �÷��̾����׼� �̽�
    public GuideQuest guideQuest;
    public GameObject offlineRewardPopUp;
    public Button offlineRewardButton;
    public TextMeshProUGUI offlinDurationText;
    public void Init()
    {
        guideQuest = new GuideQuest();
        guideQuest.Init();

        offlineRewardButton.onClick.AddListener(OfflineReward);
    }

    public void OfflineRewardPopUp(TimeSpan offlineDuration)
    {
        offlineRewardPopUp.SetActive(true);

        var durationText = "";
        if (offlineDuration.TotalHours >= 1)
        {
            offlinDurationText.text = String.Format("{0:D2}�ð� {1:D2}��", offlineDuration.Hours, offlineDuration.Minutes);
        }

        if (offlineDuration.Minutes > 0)
        {
            durationText += String.Format("{0:D2}��", offlineDuration.Minutes);
        }
        offlinDurationText.text = durationText.Trim();

        Debug.Log("������ �ð�: " + offlineDuration.TotalMinutes + "��");
        Debug.Log("������ �ð�: " + offlineDuration.TotalSeconds + "��");
    }

    private void OfflineReward()
    {
        offlineRewardPopUp.SetActive(false);
    }
}
