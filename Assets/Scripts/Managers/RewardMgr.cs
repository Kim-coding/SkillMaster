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


}
