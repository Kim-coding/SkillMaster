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
    public Sprite[] texture;
    public string itemName;
    public Sprite icon;

    public Item() { }
    public Item(Sprite[] texture, string itemName)
    {
        this.texture = texture;
        this.itemName = itemName;
    }
}
