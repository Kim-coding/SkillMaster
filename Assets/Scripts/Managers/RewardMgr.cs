using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardMgr : MonoBehaviour
{
    //public PlayerCurrency currency;  TO-DO �÷��̾����׼� �̽�
    public GuideQuest guideQuest;


    public void Init()
    {
        guideQuest = new GuideQuest();
        guideQuest.Init();
    }

    public void OfflineRewardPopUp(TimeSpan offlineDuration)
    {
        Debug.Log("������ �ð�: " + offlineDuration.TotalMinutes + "��");
        Debug.Log("������ �ð�: " + offlineDuration.TotalSeconds + "��");
    }

    private void OfflineReward()
    {

    }
}
