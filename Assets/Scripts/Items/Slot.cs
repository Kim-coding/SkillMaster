using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int slotIndex {  get; set; }
    public Button button;
    public Image selectedBorder;

    public Image itemImage;
    public Equip currentEquip = null;
    public int customOrder;

    public bool onSelected = false;


    public void SetData(Equip equipData)
    {
        if (equipData != null)
        {
            currentEquip = equipData;
            itemImage.sprite = equipData.icon;
            if (button != null)
            {
                button.interactable = true;
            }
        }

    }

    public void SetEmpty()
    {
        button.interactable = false;
        currentEquip = null;
        itemImage = null;
    }

}
