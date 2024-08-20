using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int tutorialID;

    public void Init()
    {
        if (GameMgr.Instance.uiMgr.uiBattleTutorial == null)
        {
            return;
        }

        var uiTutorial = GameMgr.Instance.uiMgr.uiBattleTutorial;

        if (SaveLoadSystem.CurrSaveData.savePlay != null)
        {
            var data = SaveLoadSystem.CurrSaveData.savePlay;
            uiTutorial.currentTutorialID = data.tutorialID;
        }
        else
        {
            tutorialID = 120221;
        }
    }

}
