using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int tutorialID;

    public void Init()
    {
        if (GameMgr.Instance.uiMgr.uiTutorial == null)
        {
            return;
        }

        if (SaveLoadSystem.CurrSaveData.savePlay != null)
        {
            var data = SaveLoadSystem.CurrSaveData.savePlay;
            tutorialID = data.tutorialID;
        }
        else
        {
            tutorialID = 120221;
        }

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
