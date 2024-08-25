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

    public TextMeshProUGUI currentLvText;
    private int currentLv;
    public TextMeshProUGUI nextLvText;
    public TextMeshProUGUI currentOptionValueText;
    public TextMeshProUGUI nextOptionValueText;


    public TextMeshProUGUI explainText;
    public GameObject lvPanel;
    public GameObject optionPanel;
    public GameObject optionValuePanel;
    public GameObject percentPanel;
    public GameObject materialPanel;

    public GameObject[] options;
    public TextMeshProUGUI[] optionNames;
    public TextMeshProUGUI[] currentPercent;
    public TextMeshProUGUI[] nextPercent;

    public GameObject missingPanel;

    public TextMeshProUGUI successPercentText;
    public TextMeshProUGUI sumSuccessPercentText;
    public int successPercent;

    public Button informationButton;
    public Button informationPanel;
    public TextMeshProUGUI informationText;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI reinforceText;

    public Button upgradeButton;

    public GameObject upgradeResultPanel;

    private int currentToggleNumber = -1;
    private Equip selectedEquip;
    private bool[] limits = new bool[6];
    private void Awake()
    {
        exitButton.onClick.AddListener(ClosePanel);
        foreach (var part in partsToggles)
        {
            part.onValueChanged.AddListener(onToggleValueChange);
        }
        informationButton.onClick.AddListener(OpenInformation);
        informationPanel.onClick.AddListener(() => { informationPanel.gameObject.SetActive(false); });
        upgradeButton.onClick.AddListener(EquipUpgrade);
    }

    private void OnEnable()
    {
        foreach (var part in partsToggles)
        {
            part.isOn = false;
        }

        EquipUpgradePanelUpdate();
    }


    public void EquipUpgradePanelUpdate()
    {
        toggleCheck();
        LimitCheck();

        foreach (var option in options)
        {
            option.gameObject.SetActive(false);
        }

        if (currentToggleNumber != -1)
        {
            switch (currentToggleNumber)
            {
                case 0:
                    currentLv = GameMgr.Instance.playerMgr.playerinventory.hairSlotUpgrade;
                    missingPanel.gameObject.SetActive(GameMgr.Instance.uiMgr.uiInventory.hairSlot.baseEquip);
                    selectedEquip = GameMgr.Instance.playerMgr.playerinventory.playerHair;
                    break;
                case 1:
                    currentLv = GameMgr.Instance.playerMgr.playerinventory.faceSlotUpgrade;
                    missingPanel.gameObject.SetActive(GameMgr.Instance.uiMgr.uiInventory.faceSlot.baseEquip);
                    selectedEquip = GameMgr.Instance.playerMgr.playerinventory.playerFace;
                    break;
                case 2:
                    currentLv = GameMgr.Instance.playerMgr.playerinventory.clothSlotUpgrade;
                    missingPanel.gameObject.SetActive(GameMgr.Instance.uiMgr.uiInventory.clothSlot.baseEquip);
                    selectedEquip = GameMgr.Instance.playerMgr.playerinventory.playerCloth;
                    break;
                case 3:
                    currentLv = GameMgr.Instance.playerMgr.playerinventory.pantSlotUpgrade;
                    missingPanel.gameObject.SetActive(GameMgr.Instance.uiMgr.uiInventory.pantSlot.baseEquip);
                    selectedEquip = GameMgr.Instance.playerMgr.playerinventory.playerPant;
                    break;
                case 4:
                    currentLv = GameMgr.Instance.playerMgr.playerinventory.weaponSlotUpgrade;
                    missingPanel.gameObject.SetActive(GameMgr.Instance.uiMgr.uiInventory.weaponSlot.baseEquip);
                    selectedEquip = GameMgr.Instance.playerMgr.playerinventory.playerWeapon;
                    break;
                case 5:
                    currentLv = GameMgr.Instance.playerMgr.playerinventory.cloakSlotUpgrade;
                    missingPanel.gameObject.SetActive(GameMgr.Instance.uiMgr.uiInventory.cloakSlot.baseEquip);
                    selectedEquip = GameMgr.Instance.playerMgr.playerinventory.playerCloak;
                    break;
            }

            var currentUpgradeData = DataTableMgr.Get<EquipUpgradeTable>(DataTableIds.equipmentUpgrade).GetID(currentLv);

            currentLvText.text = "Lv. " + currentLv;
            currentOptionValueText.text = "옵션 상승률 : " + currentUpgradeData.option_raise.ToString();

            if (currentLv == 60)
            {
                nextLvText.text = "Lv. Max";
                nextOptionValueText.text = "Max";
            }
            else
            {
                nextLvText.text = "Lv. " + (currentLv + 1);
                var nextUpgradeData = DataTableMgr.Get<EquipUpgradeTable>(DataTableIds.equipmentUpgrade).GetID(currentLv+1);
                nextOptionValueText.text = "옵션 상승률 : " + nextUpgradeData.option_raise.ToString();
            }


            for (int i = 0; i < selectedEquip.EquipOption.currentOptions.Count; i++)
            {
                options[i].gameObject.SetActive(true);
                string optiontext = string.Empty;
                switch (selectedEquip.EquipOption.currentOptions[i].Item1)
                {
                    case OptionType.attackPower:
                        optiontext = "공격력";
                        break;
                    case OptionType.maxHealth:
                        optiontext = "최대 체력";
                        break;
                    case OptionType.deffence:
                        optiontext = "방어력";
                        break;
                    case OptionType.recovery:
                        optiontext = "회복력";
                        break;
                    case OptionType.criticalPercent:
                        optiontext = "치명타 확률";
                        break;
                    case OptionType.criticalMultiple:
                        optiontext = "치명타 데미지";
                        break;
                    case OptionType.speed:
                        optiontext = "이동 속도";
                        break;
                    case OptionType.attackRange:
                        optiontext = "공격 범위";
                        break;
                    case OptionType.attackSpeed:
                        optiontext = "공격 속도";
                        break;
                    case OptionType.goldIncrease:
                        optiontext = "골드 획득량";
                        break;
                }
                optionNames[i].text = optiontext;
                var option_raise = currentUpgradeData.option_raise;
                currentPercent[i].text = (selectedEquip.EquipOption.currentOptions[i].Item2 * option_raise).ToString() + "%";

                if (currentLv == 60)
                {
                    nextPercent[i].text = "Max";
                }
                else if (limits[currentToggleNumber])
                {
                    int lv = 0;
                    switch (currentToggleNumber)
                    {
                        case 0:
                            lv = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.hairSlot.currentEquip.rarerityType);
                            break;
                        case 1:
                            lv = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.faceSlot.currentEquip.rarerityType);
                            break;
                        case 2:
                            lv = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.clothSlot.currentEquip.rarerityType);
                            break;
                        case 3:
                            lv = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.pantSlot.currentEquip.rarerityType);
                            break;
                        case 4:
                            lv = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.weaponSlot.currentEquip.rarerityType);
                            break;
                        case 5:
                            lv = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.cloakSlot.currentEquip.rarerityType);
                            break;
                    }
                    var limitOption_raise = DataTableMgr.Get<EquipUpgradeTable>(DataTableIds.equipmentUpgrade).GetID(lv).option_raise;
                    currentPercent[i].text = (selectedEquip.EquipOption.currentOptions[i].Item2 * limitOption_raise).ToString() + "%";
                    nextPercent[i].text = currentPercent[i].text;
                }
                else
                {
                    var nextOption_raise = DataTableMgr.Get<EquipUpgradeTable>(DataTableIds.equipmentUpgrade).GetID(currentLv + 1).option_raise;
                    nextPercent[i].text = (selectedEquip.EquipOption.currentOptions[i].Item2 * nextOption_raise).ToString() + "%";
                }
            }
            int upgradeCount = GameMgr.Instance.playerMgr.playerinventory.upgradeFailCount[currentToggleNumber];
            successPercent = currentUpgradeData.Success_persent + currentUpgradeData.Success_persentdown * upgradeCount;
            successPercentText.text = successPercent + "%";
            sumSuccessPercentText.text = "(" + currentUpgradeData.Success_persent + "% + " +
                (currentUpgradeData.Success_persentdown * upgradeCount) + "%)";

            goldText.text = $"{currentUpgradeData.gold_usevalue}개 소모 / 보유 : {GameMgr.Instance.playerMgr.currency.gold.ToStringShort()}";
            int reinforceCount = 0;
            foreach (var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
            {
                if (item.itemNumber == 220006)
                {
                    reinforceCount = item.itemValue;
                    break;
                }
            }
            reinforceText.text = $"{currentUpgradeData.reinforce_increase}개 소모 / 보유 : {reinforceCount}";
            BigInteger goldValue = new BigInteger(currentUpgradeData.gold_usevalue);

            bool upgradeButtonActive = false;
            if (goldValue > GameMgr.Instance.playerMgr.currency.gold)
            {
                goldText.color = Color.red;
            }
            else
            {
                goldText.color = Color.black;
                upgradeButtonActive = true;
            }

            if (currentUpgradeData.reinforce_increase > reinforceCount)
            {
                reinforceText.color = Color.red;
                upgradeButtonActive = false;
            }
            else
            {
                reinforceText.color = Color.black;
            }

            if (currentLv == 60)
            {
                upgradeButtonActive = false;
            }

            upgradeButton.interactable = upgradeButtonActive;
        }
    }

    private void toggleCheck()
    {
        explainText.gameObject.SetActive(true);
        optionPanel.gameObject.SetActive(false);
        lvPanel.gameObject.SetActive(false);
        optionValuePanel.gameObject.SetActive(false);
        materialPanel.gameObject.SetActive(false);
        percentPanel.gameObject.SetActive(false);
        upgradeButton.interactable = false;
        currentToggleNumber = -1;
        for (int i = 0; i < partsToggles.Length; i++)
        {
            if (partsToggles[i].isOn)
            {
                explainText.gameObject.SetActive(false);
                optionPanel.gameObject.SetActive(true);
                lvPanel.gameObject.SetActive(true);
                optionValuePanel.gameObject.SetActive(true);
                materialPanel.gameObject.SetActive(true);
                percentPanel.gameObject.SetActive(true);
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
                SetToggleColor(toggle, Color.black);
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

    public void OpenInformation()
    {
        informationPanel.gameObject.SetActive(true);
        informationPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"강화에 실패할 때마다 성공 확률이\r\n" +
            $"{DataTableMgr.Get<EquipUpgradeTable>(DataTableIds.equipmentUpgrade).GetID(currentLv).Success_persentdown}% 씩 높아집니다.";
    }

    public void EquipUpgrade()
    {
        var currentUpgradeData = DataTableMgr.Get<EquipUpgradeTable>(DataTableIds.equipmentUpgrade).GetID(currentLv);
        BigInteger goldValue = new BigInteger(currentUpgradeData.gold_usevalue);
        GameMgr.Instance.playerMgr.currency.RemoveGold(goldValue);

        foreach (var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
        {
            if (item.itemNumber == 220006)
            {
                item.itemValue -= currentUpgradeData.reinforce_increase;
                break;
            }
        }

        GameMgr.Instance.uiMgr.uiInventory.NormalItemUpdate();

        int upgradeNum = Random.Range(0, 101);
        if (upgradeNum <= successPercent)
        {
            //강화 성공
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.gameObject.SetActive(true);
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.SetText("강화 성공");
            switch (currentToggleNumber)
            {
                case 0:
                    GameMgr.Instance.playerMgr.playerinventory.hairSlotUpgrade++;
                    break;
                case 1:
                    GameMgr.Instance.playerMgr.playerinventory.faceSlotUpgrade++;
                    break;
                case 2:
                    GameMgr.Instance.playerMgr.playerinventory.clothSlotUpgrade++;
                    break;
                case 3:
                    GameMgr.Instance.playerMgr.playerinventory.pantSlotUpgrade++;
                    break;
                case 4:
                    GameMgr.Instance.playerMgr.playerinventory.weaponSlotUpgrade++;
                    break;
                case 5:
                    GameMgr.Instance.playerMgr.playerinventory.cloakSlotUpgrade++;
                    break;
            }

            GameMgr.Instance.playerMgr.playerinventory.upgradeFailCount[currentToggleNumber] = 0;
            GameMgr.Instance.playerMgr.playerinventory.ItemOptionsUpdate();

        }
        else
        {
            //강화 실패
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.gameObject.SetActive(true);
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.SetText("강화 실패");
            GameMgr.Instance.playerMgr.playerinventory.upgradeFailCount[currentToggleNumber]++;
        }
        EquipUpgradePanelUpdate();
        GameMgr.Instance.uiMgr.uiInventory.SlotLevelUpdate();
        SaveLoadSystem.Save();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private int EquipRarityCheck(RarerityType rarerity)
    {
        switch (rarerity)
        {
            case RarerityType.C:
                return 10;
            case RarerityType.B:
                return 20;
            case RarerityType.A:
                return 30;
            case RarerityType.S:
                return 40;
            case RarerityType.SS:
                return 50;
            case RarerityType.SSS:
                return 60;
        }
        return int.MaxValue;
    }


    private void LimitCheck()
    {
        var hairRare = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.hairSlot.currentEquip.rarerityType);
        var hairLv = GameMgr.Instance.playerMgr.playerinventory.hairSlotUpgrade;
        if (hairLv >= hairRare)
        {
            currentLevels[0].text = "Lv " + hairRare;
            currentLevels[0].color = Color.red;
            limits[0] = true;
        }
        else
        {
            currentLevels[0].text = "Lv " + hairLv;
            currentLevels[0].color = Color.white;
            limits[0] = false;
        }

        var faceRare = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.faceSlot.currentEquip.rarerityType);
        var faceLv = GameMgr.Instance.playerMgr.playerinventory.faceSlotUpgrade;
        if (faceLv >= faceRare)
        {
            currentLevels[1].text = "Lv " + faceRare;
            currentLevels[1].color = Color.red;
            limits[1] = true;
        }
        else
        {
            currentLevels[1].text = "Lv " + faceLv;
            currentLevels[1].color = Color.white;
            limits[1] = false;
        }

        var clothRare = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.clothSlot.currentEquip.rarerityType);
        var clothLv = GameMgr.Instance.playerMgr.playerinventory.clothSlotUpgrade;
        if (clothLv >= clothRare)
        {
            currentLevels[2].text = "Lv " + clothRare;
            currentLevels[2].color = Color.red;
            limits[2] = true;

        }
        else
        {
            currentLevels[2].text = "Lv " + clothLv;
            currentLevels[2].color = Color.white;
            limits[2] = false;
        }

        var pantsRare = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.pantSlot.currentEquip.rarerityType);
        var pantsLv = GameMgr.Instance.playerMgr.playerinventory.pantSlotUpgrade;
        if (pantsLv >= pantsRare)
        {
            currentLevels[3].text = "Lv " + pantsRare;
            currentLevels[3].color = Color.red;
            limits[3] = true;
        }
        else
        {
            currentLevels[3].text = "Lv " + pantsLv;
            currentLevels[3].color = Color.white;
            limits[3] = false;
        }

        var weaponRare = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.weaponSlot.currentEquip.rarerityType);
        var weaponLv = GameMgr.Instance.playerMgr.playerinventory.weaponSlotUpgrade;
        if (weaponLv >= weaponRare)
        {
            currentLevels[4].text = "Lv " + weaponRare;
            currentLevels[4].color = Color.red;
            limits[4] = true;
        }
        else
        {
            currentLevels[4].text = "Lv " + weaponLv;
            currentLevels[4].color = Color.white;
            limits[4] = false;
        }

        var cloakRare = EquipRarityCheck(GameMgr.Instance.uiMgr.uiInventory.cloakSlot.currentEquip.rarerityType);
        var cloakLv = GameMgr.Instance.playerMgr.playerinventory.cloakSlotUpgrade;
        if (cloakLv >= cloakRare)
        {
            currentLevels[5].text = "Lv " + cloakRare;
            currentLevels[5].color = Color.red;
            limits[5] = true;
        }
        else
        {
            currentLevels[5].text = "Lv " + cloakLv;
            currentLevels[5].color = Color.white;
            limits[5] = false;
        }
    }
}
