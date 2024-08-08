using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NormalItem : Item
{
    public NormalItem() { }
    public NormalItem(int itemID,int itemValue)
    {
        itemNumber = itemID;
        stuffData = DataTableMgr.Get<StuffTable>(DataTableIds.stuff).GetID(itemID);
        icon = stuffData.Geticon;
        itemName = stuffData.GetName;
        itemExplain = stuffData.GetExplain;
        this.itemValue = itemValue;
    }

    public StuffData stuffData = new StuffData();
    public int itemNumber;
    public int itemValue;
    public string itemExplain;

    public void Init(int itemID, int itemValue)
    {
        itemNumber = itemID;
        stuffData = DataTableMgr.Get<StuffTable>(DataTableIds.stuff).GetID(itemID);
        icon = stuffData.Geticon;
        itemName = stuffData.GetName;
        itemExplain = stuffData.GetExplain;
        this.itemValue = itemValue;
    }
}
