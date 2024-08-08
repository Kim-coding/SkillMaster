using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI itemRarity;
    public TextMeshProUGUI[] OptionTexts;

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
