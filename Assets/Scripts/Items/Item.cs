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
public abstract class Item : MonoBehaviour
{
    public Sprite icon;
    public string path;
    public string itemName;

    public Item() { }
    public Item(Sprite icon, string path, string itemName)
    {
        this.icon = icon;
        this.path = path;
        this.itemName = itemName;
    }
}
