using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardMgr : MonoBehaviour
{
    //public PlayerCurrency currency;  TO-DO 플레이어한테서 이식
    public GuideQuest guideQuest;
    public GameObject offlineRewardPopUp;
    public Button offlineRewardButton;
    public TextMeshProUGUI offlinDurationText;

    private int stageID;
    private int goldValue;
    private int diaValue;
    private float diaProbability;
    private TimeSpan offlineDuration;

    public void Init()
    {
        guideQuest = new GuideQuest();
        guideQuest.Init();
    }

    private void Start()
    {
        var OfflineRewardTable = DataTableMgr.Get<OfflineRewardTable>(DataTableIds.offlineReward);

        var OfflineRewardData = OfflineRewardTable.GetID(30001); //TO-DO 저장된 스테이지를 가지고 와야 함
        if (OfflineRewardData != null)
        {
            stageID = OfflineRewardData.StageID;
            goldValue = OfflineRewardData.GoldValue;
            diaValue = OfflineRewardData.DiaValue;
            diaProbability = OfflineRewardData.DiaProbability;
        }

        offlineRewardButton.onClick.AddListener(OfflineReward);
    }

    public void OfflineRewardPopUp(TimeSpan offlineDuration)
    {
        offlineRewardPopUp.SetActive(true);

        this.offlineDuration = offlineDuration;

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
        var rewardGold = goldValue * offlineDuration.Minutes;

        System.Random random = new System.Random();
        int totalDia = 0;

        for (int i = 0; i < offlineDuration.Minutes; i++)
        {
            if (random.NextDouble() < diaProbability)
            {
                totalDia += diaValue;
            }
        }

        GameMgr.Instance.playerMgr.currency.AddGold(new BigInteger(rewardGold));
        GameMgr.Instance.playerMgr.currency.AddDia(new BigInteger(totalDia));

        offlineRewardPopUp.SetActive(false);
    }
}
