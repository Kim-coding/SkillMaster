using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public Image icon;
    public Image rarityColor;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI itemRarity;
    public TextMeshProUGUI[] OptionTexts;
    public TextMeshProUGUI CPText;
    public TextMeshProUGUI[] plusOptionTexts;
    private EquipSlot currentEquip;

    public Button unEquipButton;

    public void SetItemInfoPanel(Equip equip, EquipSlot slot = null)
    {
        if(slot != null)
        {
            currentEquip = slot;
            if(slot.baseEquip)
            {
                unEquipButton.gameObject.SetActive(false);
            }
            else
            {
                unEquipButton.gameObject.SetActive(true);
            }
        }

        foreach (var optiontexts in OptionTexts)
        {
            optiontexts.gameObject.SetActive(false);
        }
        foreach (var optiontexts in plusOptionTexts)
        {
            optiontexts.gameObject.SetActive(false);
        }

        icon.sprite = equip.icon;
        Color newColor = Color.white;
        switch (equip.rarerityType)
        {
            case RarerityType.None:
                ColorUtility.TryParseHtmlString("#C4C4C4", out newColor);
                break;
            case RarerityType.C:
                ColorUtility.TryParseHtmlString("#97846B", out newColor);
                break;
            case RarerityType.B:
                ColorUtility.TryParseHtmlString("#6AAC8D", out newColor);
                break;
            case RarerityType.A:
                ColorUtility.TryParseHtmlString("#A4BDFF", out newColor);
                break;
            case RarerityType.S:
                ColorUtility.TryParseHtmlString("#C188D7", out newColor);
                break;
            case RarerityType.SS:
                ColorUtility.TryParseHtmlString("#F4C56B", out newColor);
                break;
            case RarerityType.SSS:
                ColorUtility.TryParseHtmlString("#C74B46", out newColor);
                break;
        }
        rarityColor.color = newColor;
        itemName.text = equip.itemName;
        string typeName = string.Empty;
        switch(equip.equipType)
        {
            case EquipType.None:
                typeName = "Error";
                break;
            case EquipType.Hair:
                typeName = "�Ӹ�";
                break;
            case EquipType.Face:
                typeName = "��";
                break;
            case EquipType.Cloth:
                typeName = "����";
                break;
            case EquipType.Pants:
                typeName = "����";
                break;
            case EquipType.Weapon:
                typeName = "����";
                break;
            case EquipType.Cloak:
                typeName = "����";
                break;
        }

        itemType.text = "���� : " + typeName;
        itemRarity.text = "��� : " + equip.rarerityType;
        if(equip.rarerityType == RarerityType.None)
        {
            itemRarity.text = "��� : " + "�⺻";
        }


        int optioncount = 0;
        foreach (var option in equip.EquipOption.currentOptions)
        {
            OptionTexts[optioncount].gameObject.SetActive(true);
            string optiontext = string.Empty;
            switch(option.Item1)
            {
                case OptionType.attackPower:
                    optiontext = "���ݷ�";
                    break;
                case OptionType.maxHealth:
                    optiontext = "�ִ� ü��";
                    break;
                case OptionType.deffence:
                    optiontext = "����";
                    break;
                case OptionType.recovery:
                    optiontext = "ȸ����";
                    break;
                case OptionType.criticalPercent:
                    optiontext = "ġ��Ÿ Ȯ��";
                    break;
                case OptionType.criticalMultiple:
                    optiontext = "ġ��Ÿ ������";
                    break;
                case OptionType.speed:
                    optiontext = "�̵� �ӵ�";
                    break;
                case OptionType.attackRange:
                    optiontext = "���� ����";
                    break;
                case OptionType.attackSpeed:
                    optiontext = "���� �ӵ�";
                    break;
                case OptionType.goldIncrease:
                    optiontext = "��� ȹ�淮";
                    break;
            }
            if(option.Item1 == OptionType.attackPower || 
                option.Item1 == OptionType.maxHealth ||
                option.Item1 == OptionType.deffence || 
                option.Item1 == OptionType.recovery)
            {
                OptionTexts[optioncount].text = optiontext + " : " + option.Item2 + "%";
            }
            else
            {
                OptionTexts[optioncount].text = optiontext + " : " + option.Item2;
            }



            if (plusOptionTexts.Length > 0)
            {
                plusOptionTexts[optioncount].gameObject.SetActive(true);
                int upgrade = 0;
                int limit = EquipRarityCheck(equip.rarerityType);
                switch(equip.equipType)
                {
                    case EquipType.Hair:
                        upgrade = Mathf.Min(GameMgr.Instance.playerMgr.playerinventory.hairSlotUpgrade, limit);
                        break;
                    case EquipType.Face:
                        upgrade = Mathf.Min(GameMgr.Instance.playerMgr.playerinventory.faceSlotUpgrade, limit);
                        break;
                    case EquipType.Cloth:
                        upgrade = Mathf.Min(GameMgr.Instance.playerMgr.playerinventory.clothSlotUpgrade, limit);
                        break;
                    case EquipType.Pants:
                        upgrade = Mathf.Min(GameMgr.Instance.playerMgr.playerinventory.pantSlotUpgrade, limit);
                        break;
                    case EquipType.Weapon:
                        upgrade = Mathf.Min(GameMgr.Instance.playerMgr.playerinventory.weaponSlotUpgrade, limit);
                        break;
                    case EquipType.Cloak:
                        upgrade = Mathf.Min(GameMgr.Instance.playerMgr.playerinventory.cloakSlotUpgrade, limit);
                        break;
                }
                var UpgradeData = DataTableMgr.Get<EquipUpgradeTable>(DataTableIds.equipmentUpgrade).GetID(upgrade);
                var option_raise = UpgradeData.option_raise;
                float optionValue = option.Item2 * option_raise - option.Item2;
                plusOptionTexts[optioncount].text = "(+" + optionValue.ToString("F3") + ")";
            }

            optioncount++;
        }





        CPText.text = "������ : " + equip.CP;
    }

    public void SetItemInfoPanel(NormalItem item)
    {
        icon.sprite = item.icon;
        itemName.text = item.itemName;
        OptionTexts[0].gameObject.SetActive(true);
        OptionTexts[0].text = item.itemExplain;
    }

    public void EquipUpgradePanel()
    {
        ClosePanel();
        GameMgr.Instance.uiMgr.uiWindow.equipUpgradePanel.gameObject.SetActive(true);
        GameMgr.Instance.uiMgr.uiWindow.equipUpgradePanel.partsToggles[(int)currentEquip.currentEquip.equipType - 1].isOn = true;
        GameMgr.Instance.uiMgr.uiWindow.equipUpgradePanel.EquipUpgradePanelUpdate();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void OnbuttonClick()
    {
        currentEquip.RemoveEquip();
        ClosePanel();
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

}
