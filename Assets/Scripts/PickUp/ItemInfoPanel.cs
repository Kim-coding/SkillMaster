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

        itemType.text = "Type : " + typeName;
        itemRarity.text = "Rarity : " + equip.rarerityType;
        if(equip.rarerityType == RarerityType.None)
        {
            itemRarity.text = "Rarity : " + "�⺻";
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
            OptionTexts[optioncount].text = optiontext + " : " + option.Item2;
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

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void OnbuttonClick()
    {
        currentEquip.RemoveEquip();
        ClosePanel();
    }
}
