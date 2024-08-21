using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial
{
    public int tutorialID;
    public int tutorialIndex;
    private UiTutorial UiTutorial;
    public void Init()
    {
        if (GameMgr.Instance.uiMgr.uiTutorial == null)
        {
            return;
        }

        UiTutorial = GameMgr.Instance.uiMgr.uiTutorial;

        if (SaveLoadSystem.CurrSaveData.savePlay != null)
        {
            var data = SaveLoadSystem.CurrSaveData.savePlay;
            tutorialID = data.tutorialID;
            tutorialIndex = data.tutorialIndex;
        }
        else
        {
            tutorialID = 120221;
        }

    }

    public void OnTutorial()
    {
        Time.timeScale = 0f;
        if(tutorialID <= 120238)
        {
            if (tutorialID >= 120234 && tutorialID <= 120237)
            {
                GameMgr.Instance.uiMgr.uiWindow.EnhanceWindowOpen();
            }

            UiTutorial.EndTutorialIndex = 120239;
            SetTutorial();
        }
        else if (tutorialID >= 120239 && tutorialID <= 120277)
        {
            UiTutorial.EndTutorialIndex = 120278;
            SetTutorial();
            
        }
        else
        {
            UiTutorial.EndTutorialIndex = 120284;
            SetTutorial();
        }
    }

    private void SetTutorial()
    {
        UiTutorial.currentTutorialID = tutorialID;
        UiTutorial.currentTutorialIndex = tutorialIndex;
        UiTutorial.OnTutorial();
    }
}
