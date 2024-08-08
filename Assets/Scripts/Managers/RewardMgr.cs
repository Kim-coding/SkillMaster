using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardMgr : MonoBehaviour
{
    //public PlayerCurrency currency;  TO-DO 플레이어한테서 이식
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
            offlinDurationText.text = String.Format("{0:D2}시간 {1:D2}분", offlineDuration.Hours, offlineDuration.Minutes);
        }

        if (offlineDuration.Minutes > 0)
        {
            durationText += String.Format("{0:D2}분", offlineDuration.Minutes);
        }
        offlinDurationText.text = durationText.Trim();

        Debug.Log("미접속 시간: " + offlineDuration.TotalMinutes + "분");
        Debug.Log("미접속 시간: " + offlineDuration.TotalSeconds + "초");
    }

    private void OfflineReward()
    {
        offlineRewardPopUp.SetActive(false);
    }
}
