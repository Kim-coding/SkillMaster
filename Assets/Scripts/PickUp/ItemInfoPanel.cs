using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI itemRarity;
    public TextMeshProUGUI[] OptionTexts;

    public void SetItemInfoPanel(Equip equip)
    {
        foreach (var optiontexts in OptionTexts)
        {
            optiontexts.gameObject.SetActive(false);
        }
        icon.sprite = equip.icon;
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
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
