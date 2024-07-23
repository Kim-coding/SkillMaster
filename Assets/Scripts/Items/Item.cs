using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public Image icon;
    public string path;
    public string itemName;

    public Item() { }
    public Item(Image icon, string path, string itemName)
    {
        this.icon = icon;
        this.path = path;
        this.itemName = itemName;
    }
}
