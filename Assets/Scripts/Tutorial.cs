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
        SetTutorial();
    }

    public void OnTutorial()
    {
        Time.timeScale = 0f;
        var ID = UiTutorial.currentTutorialID;
        if (ID <= 120238)
        {
            if (ID >= 120234 && ID <= 120237)
            {
                GameMgr.Instance.uiMgr.uiWindow.EnhanceWindowOpen();
            }

            UiTutorial.EndTutorialIndex = 120239;
            UiTutorial.OnTutorial();
        }
        else if (ID >= 120239 && ID <= 120278)
        {
            UiTutorial.EndTutorialIndex = 120279;
            UiTutorial.OnTutorial();

        }
        else
        {
            UiTutorial.EndTutorialIndex = 120285;
            UiTutorial.OnTutorial();
        }
    }

    private void SetTutorial()
    {
        UiTutorial.currentTutorialID = tutorialID;
        UiTutorial.currentTutorialIndex = tutorialIndex;
    }
}
