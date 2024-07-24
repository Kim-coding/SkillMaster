using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    None = -1,
    Equip = 0,
    misc = 1
}
public abstract class Item
{
    public Sprite[] icon;
    public string itemName;

    public Item() { }
    public Item(Sprite[] icon, string itemName)
    {
        this.icon = icon;
        this.itemName = itemName;
    }
}
