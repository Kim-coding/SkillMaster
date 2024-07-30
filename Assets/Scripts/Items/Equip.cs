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
    private EquipOption equipOption;
    public int itemNumber;
    public EquipOption EquipOption { get { return equipOption; }}

    private void Awake()
    {
        equipOption = new EquipOption();
    }
    public void Init(Sprite[] icon, string itemName)
    {
        this.icon = icon;
        this.itemName = itemName;
    }

    public void SetEquipStat((OptionType, float) a, (OptionType, float) b, (OptionType, float) c, (OptionType, float) d)
    {
        equipOption.Init(a, b, c, d);
    }

    public void SetEquipItem(EquipType equipType, RarerityType rarerityType)
    {
        this.equipType = equipType;
        this.rarerityType = rarerityType;
    }
}
