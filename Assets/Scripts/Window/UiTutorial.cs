using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TextPosition
{
    Top,
    Bottom,
}

public class UiTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextPosition[] textPositions;
    public GameObject[] targetUI;
    public GameObject[] ui;

    public TextMeshProUGUI topText;
    public TextMeshProUGUI bottomText;

    public int currentTutorialID;
    public int currentTutorial = 0;

    private GameObject previousTarget = null;

    public void init()
    {
        CanvasGroup tutoriaCanvasGroup = tutorialPanel.GetComponent<CanvasGroup>();
        tutoriaCanvasGroup.blocksRaycasts = false;
        foreach(var i in ui)
        {
            i.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void UiUpdate()
    {
        if (currentTutorialID > 120238)
        {
            return;
        }

        if (textPositions[currentTutorial] == TextPosition.Top)
        {
            bottomText.transform.parent.gameObject.SetActive(false);
            topText.transform.parent.gameObject.gameObject.SetActive(true);
            topText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(currentTutorialID);
        }
        else if (textPositions[currentTutorial] == TextPosition.Bottom)
        {
            topText.transform.parent.gameObject.gameObject.SetActive(false);
            bottomText.transform.parent.gameObject.gameObject.SetActive(true);
            bottomText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(currentTutorialID);
        }

        if (previousTarget != null)
        {
            if(previousTarget.GetComponent<CanvasGroup>() == null)
            {
                previousTarget.AddComponent<CanvasGroup>();
            }
            CanvasGroup previousCanvasGroup = previousTarget.GetComponent<CanvasGroup>();
            if (previousCanvasGroup != null)
            {
                previousCanvasGroup.blocksRaycasts = false;
                previousCanvasGroup.interactable = false;
            }
        }

        if (targetUI[currentTutorial] != null)
        {
            GameObject currentTarget = targetUI[currentTutorial];
            if(currentTarget.GetComponent<CanvasGroup>() == null)
            {
                currentTarget.AddComponent<CanvasGroup>();
            }
            CanvasGroup canvasGroup = currentTarget.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            }

            EventSystem.current.SetSelectedGameObject(currentTarget);
            previousTarget = currentTarget;
        }

        currentTutorial++;
        currentTutorialID++;
    }

    public void NextTutorial()
    {
        UiUpdate();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            NextTutorial();
        }
    }
}
