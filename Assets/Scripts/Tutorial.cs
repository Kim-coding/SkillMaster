using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial
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
            OnTutorial();
        }
    }

    public void OnTutorial()
    {
        if(tutorialID <= 120238)
        {
            tutorialID = 120221;
            GameMgr.Instance.uiMgr.uiTutorial.currentTutorialID = tutorialID;
            GameMgr.Instance.uiMgr.uiTutorial.EndTutorialIndex = 120239;
            GameMgr.Instance.uiMgr.uiTutorial.OnTutorial();
        }
        else if (tutorialID >= 120239 && tutorialID <= 120277)
        {
            tutorialID = 120239;
            GameMgr.Instance.uiMgr.uiTutorial.currentTutorialID = tutorialID;
            GameMgr.Instance.uiMgr.uiTutorial.EndTutorialIndex = 120278;
            GameMgr.Instance.uiMgr.uiTutorial.OnTutorial();
        }
        else
        {
            tutorialID = 120278;
            GameMgr.Instance.uiMgr.uiTutorial.currentTutorialID = tutorialID;
            GameMgr.Instance.uiMgr.uiTutorial.EndTutorialIndex = 120284;
            GameMgr.Instance.uiMgr.uiTutorial.OnTutorial();
        }
    }
}
