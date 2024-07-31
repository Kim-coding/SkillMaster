using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class Equip : Item
{
    public Equip() { }
    public Equip(Sprite[] icon, string itemName, int itemNum) :
         base(icon, itemName) {
        itemNumber = itemNum;
    }

    public EquipType equipType;
    public RarerityType rarerityType;
    public int reinforceStoneValue;
    private EquipOption equipOption = new EquipOption();
    public int itemNumber;
    public EquipOption EquipOption { get { return equipOption; }}

    public void Init(Sprite[] icon, string itemName)
    {
        this.icon = icon;
        this.itemName = itemName;
    }

    public void SetEquipStat((OptionType, float) a)
    {
        equipOption.AddOption(a.Item1, a.Item2);
    }

    public void SetEquipItem(EquipType equipType, RarerityType rarerityType)
    {
        this.equipType = equipType;
        this.rarerityType = rarerityType;
    }
}
