using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equip : Item
{
    public Equip() { }
    public Equip(Sprite[] texture, string itemName, int itemNum) : base(texture, itemName)
    {
        itemNumber = itemNum;
        this.texture = texture;
        var Image = texture[0].texture;
        icon = Sprite.Create(Image, new Rect(0, 0, Image.width, Image.height), new Vector2(0.5f, 0.5f));
    }
    public EquipType equipType;
    public RarerityType rarerityType;
    public int reinforceStoneValue;
    private EquipOption equipOption = new EquipOption();
    public int itemNumber;
    public EquipOption EquipOption { get { return equipOption; } }

    public void Init(Sprite[] texture, string itemName)
    {
        this.texture = texture;
        this.itemName = itemName;
        var Image = texture[0].texture;
        icon = Sprite.Create(Image, new Rect(0, 0, Image.width, Image.height), new Vector2(0.5f, 0.5f));
    }

    public bool SetEquipStat((OptionType, float) a)
    {
        return equipOption.AddOption(a.Item1, a.Item2);
    }

    public void SetEquipItem(EquipType equipType, RarerityType rarerityType)
    {
        this.equipType = equipType;
        this.rarerityType = rarerityType;
    }
}
