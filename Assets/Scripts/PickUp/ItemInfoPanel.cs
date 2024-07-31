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
    public TextMeshProUGUI[] OptionTexts;

    public void SetItemInfoPanel(Equip equip)
    {
        foreach (var optiontexts in OptionTexts)
        {
            optiontexts.gameObject.SetActive(false);
        }
        icon.sprite = equip.icon;
        itemName.text = equip.itemName;
        itemType.text = "Type : " + equip.equipType + " / Rarity : " + equip.rarerityType;
        int optioncount = 0;
        foreach (var option in equip.EquipOption.currentOptions)
        {
            OptionTexts[optioncount].gameObject.SetActive(true);
            OptionTexts[optioncount].text = option.Item1 + " : " + option.Item2;
            optioncount++;
        }
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
