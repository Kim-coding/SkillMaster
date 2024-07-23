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
        currentEquip = equipData;
        itemImage.sprite = equipData.icon;

    }
}
