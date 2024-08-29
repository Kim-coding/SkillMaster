using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial
{
    public int tutorialID;
    public int tutorialIndex;
    private UiTutorial UiTutorial;
    public bool isDungeonOpen;

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
            isDungeonOpen = data.isDungeonOpen;
        }
        else
        {
            isDungeonOpen = false;
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
            if (!GameMgr.Instance.uiMgr.isStory)
            {
                GameMgr.Instance.uiMgr.OnStory();
            }

            UiTutorial.EndTutorialIndex = 120239;
            UiTutorial.OnTutorial();
        }
        else if (ID >= 120239 && ID <= 120278)
        {
            UiTutorial.EndTutorialIndex = 120279;
            UiTutorial.OnTutorial();

        }
        else if (ID >= 120279 && ID <= 120284)
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
