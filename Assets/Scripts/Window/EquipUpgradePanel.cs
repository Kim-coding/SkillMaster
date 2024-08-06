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

    public TextMeshProUGUI currentLv;
    public TextMeshProUGUI nextLv;

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

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
