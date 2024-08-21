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
    public Transform tutorialPanel;
    public TextPosition[] textPositions;
    public GameObject[] targetUI;

    public TextMeshProUGUI topText;
    public TextMeshProUGUI bottomText;

    public int currentTutorialID;
    public int currentTutorialIndex = 0;
    public int EndTutorialIndex;

    private GameObject previousTarget = null;
    private Transform originalParent;
    private GameObject currentTargetUI;
    private Coroutine CheckCoroutine;
    public void OnTutorial()
    {
        if(!tutorialPanel.gameObject.activeSelf)
        {
            tutorialPanel.gameObject.SetActive(true);
        }

        if (previousTarget != null)
        {
            Button previousButton = previousTarget.GetComponent<Button>();
            if (previousButton != null)
            {
                previousButton.onClick.RemoveListener(OnButton);
            }

            Vector3 originalPosition = previousTarget.transform.position;
            previousTarget.transform.SetParent(originalParent, false);
            previousTarget.transform.position = originalPosition;
        }

        if (textPositions[currentTutorialIndex] == TextPosition.Top)
        {
            bottomText.transform.parent.gameObject.SetActive(false);
            topText.transform.parent.gameObject.gameObject.SetActive(true);
            topText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(currentTutorialID);
        }
        else if (textPositions[currentTutorialIndex] == TextPosition.Bottom)
        {
            topText.transform.parent.gameObject.gameObject.SetActive(false);
            bottomText.transform.parent.gameObject.gameObject.SetActive(true);
            bottomText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(currentTutorialID);
        }

        if (targetUI[currentTutorialIndex] != null)
        {
            currentTargetUI = targetUI[currentTutorialIndex];

            Vector3 originalPosition = currentTargetUI.transform.position;

            originalParent = currentTargetUI.transform.parent;
            currentTargetUI.transform.SetParent(tutorialPanel, false);

            currentTargetUI.transform.position = originalPosition;

            if (currentTargetUI.name == "Merge")
            {
                if (CheckCoroutine != null)
                {
                    StopCoroutine(CheckCoroutine);
                }
                CheckCoroutine = StartCoroutine(CheckMergeUIChildCount());
            }

            Slider monsterSlider = currentTargetUI.GetComponent<Slider>();
            if (monsterSlider != null && currentTargetUI.name == "MonsterSlider")
            {
                if (CheckCoroutine != null)
                {
                    StopCoroutine(CheckCoroutine);
                }
                CheckCoroutine = StartCoroutine(CheckMonsterSliderValue(monsterSlider));
            }

            Button currentButton = currentTargetUI.GetComponent<Button>();
            if (currentButton != null)
            {
                currentButton.onClick.AddListener(OnButton);
            }

            Button tutorialButton = tutorialPanel.gameObject.GetComponent<Button>();
            if (tutorialButton != null)
            {
                tutorialButton.onClick.RemoveListener(OnButton);
            }
        }
        else
        {
            Button tutorialButton = tutorialPanel.gameObject.GetComponent<Button>();
            if (tutorialButton != null)
            {
                tutorialButton.onClick.RemoveListener(OnButton);
                tutorialButton.onClick.AddListener(OnButton);
            }
        }

        previousTarget = currentTargetUI;
        currentTutorialIndex++;
        currentTutorialID++;
    }

    private IEnumerator CheckMonsterSliderValue(Slider slider)
    {
        while (true)
        {
            if (slider.value >= slider.maxValue)
            {
                NextTutorial();
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator CheckMergeUIChildCount()
    {
        while (true)
        {
            if (currentTargetUI != null && currentTargetUI.transform.childCount == 1)
            {
                NextTutorial();
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void NextTutorial()
    {
        if(EndTutorialIndex <= currentTutorialID)
        {
            EndTutorial();
            return;
        }
        OnTutorial();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            NextTutorial();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            EndTutorial();
        }
    }

    public void EndTutorial()
    {
        if(currentTargetUI != null && originalParent != null)
        {
            Vector3 originalPosition = currentTargetUI.transform.position;

            currentTargetUI.transform.SetParent(originalParent, false);

            currentTargetUI.transform.position = originalPosition;
        }

        if (CheckCoroutine != null)
        {
            StopCoroutine(CheckCoroutine);
        }

        tutorialPanel.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnButton()
    {
        NextTutorial();
    }
}
