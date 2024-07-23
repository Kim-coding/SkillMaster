using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class Equip : Item
{
    public Equip(Sprite icon, string path, string itemName) :
         base(icon, path, itemName) { }

    public EquipType equipType;
    public RarerityType rarerityType;
    public int reinforceStoneValue;
    private EquipOption equipOption;
    public EquipOption EquipOption { get { return equipOption; }}

    private void Awake()
    {
        equipOption = new EquipOption();
    }

    public void Init((OptionType, float) a, (OptionType, float) b, (OptionType, float) c, (OptionType, float) d)
    {
        equipOption.Init(a, b, c, d);
    }

    public void SetEquipItem(EquipType equipType, RarerityType rarerityType)
    {
        this.equipType = equipType;
        this.rarerityType = rarerityType;
    }
}
