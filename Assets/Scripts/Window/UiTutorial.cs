using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    public int currentTutorialIndex;
    public bool isDungeonOpen;
    public int EndTutorialIndex;

    private GameObject previousTarget = null;
    private Transform originalParent;
    private GameObject currentTargetUI;
    private Coroutine CheckCoroutine;

    private float typingSpeed = 0.01f;
    private UnityAction<bool> toggleListener;
    public void OnTutorial()
    {
        var invincible = GameMgr.Instance.playerMgr.characters[0].GetComponent<IDamageable>();
        invincible.invincible = true;
        
        if (!tutorialPanel.gameObject.activeSelf)
        {
            tutorialPanel.gameObject.SetActive(true);
        }

        if (previousTarget != null)
        {
            RemoveHighlight(previousTarget);

            Button previousButton = previousTarget.GetComponent<Button>();
            if (previousButton != null)
            {
                previousButton.onClick.RemoveListener(OnButton);
            }
            Toggle previousToggle = previousTarget.GetComponent<Toggle>();
            if (previousToggle != null && toggleListener != null)
            {
                previousToggle.onValueChanged.RemoveListener(toggleListener);
            }

            Vector3 originalPosition = previousTarget.transform.position;
            previousTarget.transform.SetParent(originalParent, false);
            previousTarget.transform.position = originalPosition;
        }

        if (textPositions[currentTutorialIndex] == TextPosition.Top)
        {
            bottomText.transform.parent.gameObject.SetActive(false);
            topText.transform.parent.gameObject.gameObject.SetActive(true);
            var fullText = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(currentTutorialID);
            TextAnimation(topText, fullText);
        }
        else if (textPositions[currentTutorialIndex] == TextPosition.Bottom)
        {
            topText.transform.parent.gameObject.gameObject.SetActive(false);
            bottomText.transform.parent.gameObject.gameObject.SetActive(true);
            var fullText = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(currentTutorialID);
            TextAnimation(bottomText, fullText);
        }

        if (targetUI[currentTutorialIndex] != null)
        {
            currentTargetUI = targetUI[currentTutorialIndex];

            if (currentTargetUI.name == "Merge")
            {
                Time.timeScale = 0f;
                if (CheckCoroutine != null)
                {
                    StopCoroutine(CheckCoroutine);
                }
                CheckCoroutine = StartCoroutine(CheckMergeUIChildCount());
            }

            Slider monsterSlider = currentTargetUI.GetComponent<Slider>();
            if (monsterSlider != null && currentTargetUI.name == "MonsterSlider")
            {
                Time.timeScale = 1f;
                if (CheckCoroutine != null)
                {
                    StopCoroutine(CheckCoroutine);
                }
                CheckCoroutine = StartCoroutine(CheckMonsterSliderValue(monsterSlider));
            }

            Button tutorialButton = tutorialPanel.gameObject.GetComponent<Button>();
            if (tutorialButton != null)
            {
                tutorialButton.onClick.RemoveListener(OnButton);
            }

            if(currentTargetUI.name == "AutoSelect")
            {
                GameMgr.Instance.uiMgr.uiInventory.OnDecomposMode();
            }
            if(currentTargetUI.name == "Inven Button")
            {
                GameMgr.Instance.uiMgr.uiInventory.inventoryModeToggles[0].isOn = true;
                GameMgr.Instance.uiMgr.uiInventory.OnInventoryModeToggleValueChanged(true);
            }
            Button currentButton = currentTargetUI.GetComponent<Button>();
            Toggle currentToggle = currentTargetUI.GetComponent<Toggle>();

            if (currentButton != null)
            {
                if (currentTargetUI.name != "BossSpawnButton")
                {
                    currentButton.onClick.AddListener(OnButton);
                }
                else
                {
                    currentTargetUI.SetActive(true);
                    currentButton.interactable = false;
                    tutorialButton.onClick.AddListener(OnButton);
                }
            }
            else if (currentToggle != null)
            {
                toggleListener = (value) =>
                {
                    if (value)
                    {
                        NextTutorial();
                    }
                };

                currentToggle.onValueChanged.AddListener(toggleListener);
            }
            else
            {
                if(currentTargetUI.name == "EquipContent")
                {
                    currentTargetUI = currentTargetUI.transform.GetChild(0).gameObject;
                    Button childButton = currentTargetUI.GetComponent<Button>();

                    if (childButton != null)
                    {
                        childButton.onClick.RemoveListener(OnButton);
                        childButton.onClick.AddListener(OnButton);
                    }
                }
                else if (currentTargetUI.name == "Decompos" || currentTargetUI.name == "AutoDecompos" || currentTargetUI.name == "PickupScrollView" || currentTargetUI.name == "GoldDungeonButtons") 
                {
                    Button[] childButtons = currentTargetUI.GetComponentsInChildren<Button>();
                    foreach (Button btn in childButtons)
                    {
                        btn.interactable = false;
                    }
                    tutorialButton.onClick.RemoveListener(OnButton);
                    tutorialButton.onClick.AddListener(OnButton);

                    if(currentTargetUI.name == "AutoDecompos")
                    {
                        Toggle[] childToggles = currentTargetUI.GetComponentsInChildren<Toggle>();

                        foreach (Toggle toggle in childToggles)
                        {
                            toggle.interactable = false;
                        }
                    }
                }
                else if(currentTargetUI.name != "Merge" && currentTargetUI.name != "MonsterSlider")
                {
                    tutorialButton.onClick.RemoveListener(OnButton);
                    tutorialButton.onClick.AddListener(OnButton);
                }

            }
            Vector3 originalPosition = currentTargetUI.transform.position;

            originalParent = currentTargetUI.transform.parent;
            currentTargetUI.transform.SetParent(tutorialPanel, false);

            currentTargetUI.transform.position = originalPosition;
            HighlightTarget(currentTargetUI);
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

            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    public void NextTutorial()
    {
        if(currentTutorialID == 120230 && Time.timeScale != 0f)
        {
            return;
        }

        currentTutorialIndex++;
        currentTutorialID++;

        if (currentTargetUI != null && currentTargetUI.name == "BossSpawnButton")
        {
            currentTargetUI.GetComponent<Button>().interactable = true;
            currentTargetUI.gameObject.SetActive(false);
        }
        else if (currentTargetUI != null && (currentTargetUI.name == "Decompos" || currentTargetUI.name == "AutoDecompos" || currentTargetUI.name == "PickupScrollView" || currentTargetUI.name == "GoldDungeonButtons"))
        {
            Button[] childButtons = currentTargetUI.GetComponentsInChildren<Button>();
            foreach (Button btn in childButtons)
            {
                btn.interactable = true;
            }
            if (currentTargetUI.name == "AutoDecompos")
            {
                Toggle[] childToggles = currentTargetUI.GetComponentsInChildren<Toggle>();

                foreach (Toggle toggle in childToggles)
                {
                    toggle.interactable = true;
                }
            }
        }

        if (EndTutorialIndex <= currentTutorialID)
        {
            EndTutorial();
            return;
        }
        OnTutorial();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    NextTutorial();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    EndTutorial();
        //}
    }

    public void EndTutorial()
    {
        var invincible = GameMgr.Instance.playerMgr.characters[0].GetComponent<IDamageable>();
        invincible.invincible = false;

        if (currentTargetUI != null && originalParent != null)
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
        GameMgr.Instance.webTimeMgr.SaveTime();
        SaveLoadSystem.Save();
    }

    public void OnButton()
    {
        NextTutorial();
    }

    public void TextAnimation(TextMeshProUGUI textMesh, string fullText)
    {
        textMesh.text = "";
        Sequence typingSequence = DOTween.Sequence();

        for (int i = 0; i < fullText.Length; i++)
        {
            string currentText = fullText.Substring(0, i + 1);
            typingSequence.AppendCallback(() =>
            {
                textMesh.text = currentText;
            });
            typingSequence.AppendInterval(typingSpeed);
        }

        typingSequence.SetUpdate(true); //Time.timeScale이 0일때에도 작동
        typingSequence.Play();
    }

    public void HighlightTarget(GameObject target)
    {
        Outline outline = target.GetComponent<Outline>();
        if (outline == null)
        {
            outline = target.AddComponent<Outline>();
        }

        outline.effectColor = Color.yellow;
        outline.effectDistance = new Vector2(5, 5);
    }

    public void RemoveHighlight(GameObject target)
    {
        Outline outline = target.GetComponent<Outline>();
        if (outline != null)
        {
            Destroy(outline);
        }
    }
}
