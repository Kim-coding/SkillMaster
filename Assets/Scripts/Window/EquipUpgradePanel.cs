using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipUpgradePanel : MonoBehaviour
{

    public Button exitButton;
    public ToggleGroup allParts;
    public Toggle[] partsToggles;
    public TextMeshProUGUI[] currentLevels;

    public TextMeshProUGUI currentLv;
    public TextMeshProUGUI nextLv;

    public TextMeshProUGUI explainText;
    public GameObject lvPanel;
    public GameObject optionPanel;
    public GameObject materialPanel;

    public GameObject[] options;
    public TextMeshProUGUI[] currentPercent;
    public TextMeshProUGUI[] nextPercent;

    public GameObject missingPanel;

    public TextMeshProUGUI successPercent;
    public TextMeshProUGUI sumSuccessPercent;
    public Button informationButton;

    public Button informationPanel;
    public TextMeshProUGUI informationText;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI reinforceText;

    public Button upgradeButton;

    public GameObject upgradeResultPanel;

    private int currentToggleNumber = -1;

    private void Awake()
    {
        exitButton.onClick.AddListener(ClosePanel);
        foreach (var part in partsToggles)
        {
            part.onValueChanged.AddListener(onToggleValueChange);
        }
    }

    private void OnEnable()
    {
        foreach(var part in partsToggles)
        {
            part.isOn = false;
        }

        EquipUpgradePanelUpdate();
    }


    private void EquipUpgradePanelUpdate()
    {
        toggleCheck();
        currentLevels[0].text = "Lv " + GameMgr.Instance.playerMgr.playerinventory.hairSlotUpgrade;
        currentLevels[1].text = "Lv " + GameMgr.Instance.playerMgr.playerinventory.faceSlotUpgrade;
        currentLevels[2].text = "Lv " + GameMgr.Instance.playerMgr.playerinventory.clothSlotUpgrade;
        currentLevels[3].text = "Lv " + GameMgr.Instance.playerMgr.playerinventory.pantSlotUpgrade;
        currentLevels[4].text = "Lv " + GameMgr.Instance.playerMgr.playerinventory.weaponSlotUpgrade;
        currentLevels[5].text = "Lv " + GameMgr.Instance.playerMgr.playerinventory.cloakSlotUpgrade;

        if(currentToggleNumber != -1)
        {
            switch(currentToggleNumber)
            {
                case 0:
                    currentLv.text = "Lv. " + GameMgr.Instance.playerMgr.playerinventory.hairSlotUpgrade;
                    if(GameMgr.Instance.playerMgr.playerinventory.hairSlotUpgrade == 60)
                    {
                        nextLv.text = "Lv. Max";
                    }
                    else
                    {
                        nextLv.text = "Lv. " + (GameMgr.Instance.playerMgr.playerinventory.hairSlotUpgrade + 1);
                    }
                    break;
                case 1:
                    currentLv.text = "Lv. " + GameMgr.Instance.playerMgr.playerinventory.faceSlotUpgrade;
                    if (GameMgr.Instance.playerMgr.playerinventory.faceSlotUpgrade == 60)
                    {
                        nextLv.text = "Lv. Max";
                    }
                    else
                    {
                        nextLv.text = "Lv. " + (GameMgr.Instance.playerMgr.playerinventory.faceSlotUpgrade + 1);
                    }
                    break;
                case 2:
                    currentLv.text = "Lv. " + GameMgr.Instance.playerMgr.playerinventory.clothSlotUpgrade;
                    if (GameMgr.Instance.playerMgr.playerinventory.clothSlotUpgrade == 60)
                    {
                        nextLv.text = "Lv. Max";
                    }
                    else
                    {
                        nextLv.text = "Lv. " + (GameMgr.Instance.playerMgr.playerinventory.clothSlotUpgrade + 1);
                    }
                    break;
                case 3:
                    currentLv.text = "Lv. " + GameMgr.Instance.playerMgr.playerinventory.pantSlotUpgrade;
                    if (GameMgr.Instance.playerMgr.playerinventory.pantSlotUpgrade == 60)
                    {
                        nextLv.text = "Lv. Max";
                    }
                    else
                    {
                        nextLv.text = "Lv. " + (GameMgr.Instance.playerMgr.playerinventory.pantSlotUpgrade + 1);
                    }
                    break;
                case 4:
                    currentLv.text = "Lv. " + GameMgr.Instance.playerMgr.playerinventory.weaponSlotUpgrade;
                    if (GameMgr.Instance.playerMgr.playerinventory.weaponSlotUpgrade == 60)
                    {
                        nextLv.text = "Lv. Max";
                    }
                    else
                    {
                        nextLv.text = "Lv. " + (GameMgr.Instance.playerMgr.playerinventory.weaponSlotUpgrade + 1);
                    }
                    break;
                case 5:
                    currentLv.text = "Lv. " + GameMgr.Instance.playerMgr.playerinventory.cloakSlotUpgrade;
                    if (GameMgr.Instance.playerMgr.playerinventory.cloakSlotUpgrade == 60)
                    {
                        nextLv.text = "Lv. Max";
                    }
                    else
                    {
                        nextLv.text = "Lv. " + (GameMgr.Instance.playerMgr.playerinventory.cloakSlotUpgrade + 1);
                    }
                    break;
            }
        }
    }

    private void toggleCheck()
    {
        explainText.gameObject.SetActive(true);
        optionPanel.gameObject.SetActive(false);
        lvPanel.gameObject.SetActive(false);
        materialPanel.gameObject.SetActive(false);
        upgradeButton.interactable = false;
        currentToggleNumber = -1;
        for(int i = 0; i < partsToggles.Length; i++)
        {
            if (partsToggles[i].isOn)
            {
                explainText.gameObject.SetActive(false);
                optionPanel.gameObject.SetActive(true);
                lvPanel.gameObject.SetActive(true);
                materialPanel.gameObject.SetActive(true);
                currentToggleNumber = i;
                break;
            }
        }
    }

    private void onToggleValueChange(bool isOn)
    {
        EquipUpgradePanelUpdate();
        UpdateToggleColors();
    }

    private void UpdateToggleColors()
    {
        foreach (var toggle in partsToggles)
        {
            if (toggle.isOn)
            {
                SetToggleColor(toggle, Color.blue);
            }
            else
            {
                SetToggleColor(toggle, Color.white);
            }
        }
    }


    private void SetToggleColor(Toggle toggle, Color color)
    {
        var background = toggle.targetGraphic;
        var checkmark = toggle.graphic;

        if (background != null)
        {
            background.color = color;
        }

        if (checkmark != null)
        {
            checkmark.color = color;
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
