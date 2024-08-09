using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardMgr : MonoBehaviour
{
    //public PlayerCurrency currency;  TO-DO �÷��̾����׼� �̽�
    public GuideQuest guideQuest;
    public GameObject offlineRewardPopUp;
    public Button offlineRewardButton;
    public TextMeshProUGUI offlinDurationText;
    public TextMeshProUGUI goldRewardText;
    public TextMeshProUGUI diaRewardText;

    private int stageID;
    private int goldValue;
    private int diaValue;
    private float diaProbability;
    private TimeSpan offlineDuration;

    private int goldReward;
    private int totalDia;

    public void Init()
    {
        guideQuest = new GuideQuest();
        guideQuest.Init();

        var OfflineRewardTable = DataTableMgr.Get<OfflineRewardTable>(DataTableIds.offlineReward);

        var OfflineRewardData = OfflineRewardTable.GetID(30001); //TO-DO ����� ���������� ������ �;� ��
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

        if (offlineDuration.TotalHours >= 12)
        {
            offlineDuration = TimeSpan.FromHours(12);
        }

        this.offlineDuration = offlineDuration;

        if (offlineDuration.TotalHours >= 1)
        {
            offlinDurationText.text = String.Format("{0:D2}�ð� {1:D2}��", offlineDuration.Hours, offlineDuration.Minutes);
        }
        else
        {
            offlinDurationText.text = String.Format("{0:D2}��", offlineDuration.Minutes);
        }

        goldReward = goldValue * (int)offlineDuration.TotalMinutes;

        System.Random random = new System.Random();
        totalDia = 0;

        for (int i = 0; i < (int)offlineDuration.TotalMinutes; i++)
        {
            if (random.NextDouble() < diaProbability)
            {
                totalDia += diaValue;
            }
        }

        goldRewardText.text = $"{new BigInteger(goldReward).ToStringShort()}";
        diaRewardText.text = $"{new BigInteger(totalDia)}";
    }

    private void OfflineReward()
    {
        GameMgr.Instance.playerMgr.currency.AddGold(new BigInteger(goldReward));
        GameMgr.Instance.playerMgr.currency.AddDia(new BigInteger(totalDia));

        offlineRewardPopUp.SetActive(false);
    }
}
