using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardMgr : MonoBehaviour
{
    //public PlayerCurrency currency;  TO-DO 플레이어한테서 이식
    public GuideQuest guideQuest;


    public void Init()
    {
        guideQuest = new GuideQuest();
        guideQuest.Init();
    }


}
