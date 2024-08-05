using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : Item
{
    public NormalItem() { }
    public NormalItem(Sprite[] texture, string itemName, int itemNum, int itemValue) : base(texture, itemName)
    {
        itemNumber = itemNum;
        this.texture = texture;
        var Image = texture[0].texture;
        icon = Sprite.Create(Image, new Rect(0, 0, Image.width, Image.height), new Vector2(0.5f, 0.5f));
        this.itemValue = itemValue;
    }

    public int itemNumber;
    public int itemValue;

    public void Init(Sprite[] texture, string itemName, int itemNum, int itemValue)
    {
        this.texture = texture;
        this.itemName = itemName;
        var Image = texture[0].texture;
        icon = Sprite.Create(Image, new Rect(0, 0, Image.width, Image.height), new Vector2(0.5f, 0.5f));
        itemNumber = itemNum;
        this.itemValue = itemValue;
    }
}
