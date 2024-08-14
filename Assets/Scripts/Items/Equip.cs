using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Equip : Item
{
    public Equip() { }

    public Equip(Sprite[] texture, string Name, int value, bool Face = false) 
    {
        this.itemNumber = 0;
        equipData = null;
        this.texture = texture;
        var Image = texture[0].texture;
        icon = Sprite.Create(Image, new Rect(0, 0, Image.width, Image.height), new Vector2(0.5f, 0.5f));
        itemName = Name;
        reinforceStoneValue = value;
        if (Face)
        {
            icon = texture[0];
        }

    }

    public Equip(EquipData equipData, int itemNumber)
    {
        this.itemNumber = itemNumber;
        this.equipData = equipData;
        texture = equipData.GetTexture;
        icon = equipData.Geticon;
        itemName = equipData.GetItemName;

        switch (equipData.equipmenttype)
        {
            case 1:
                equipType = EquipType.Cloth;
                break;
            case 2:
                equipType = EquipType.Weapon;
                break;
            case 3:
                equipType = EquipType.Pants;
                break;
            case 4:
                equipType = EquipType.Face;
                break;
            case 5:
                equipType = EquipType.Hair;
                break;
            case 6:
                equipType = EquipType.Cloak;
                break;
        }
        if (equipData.equipment_rating == "C")
        { rarerityType = RarerityType.C; }
        else if (equipData.equipment_rating == "B")
        { rarerityType = RarerityType.B; }
        else if (equipData.equipment_rating == "A")
        { rarerityType = RarerityType.A; }
        else if (equipData.equipment_rating == "S")
        { rarerityType = RarerityType.S; }
        else if (equipData.equipment_rating == "SS")
        { rarerityType = RarerityType.SS; }
        else
        { rarerityType = RarerityType.SSS; }
        reinforceStoneValue = equipData.reinforcement_value;


        if(equipType == EquipType.Face)
        {
            icon = texture[0];
        }

    }
    public EquipData equipData = new EquipData();
    public EquipType equipType;
    public RarerityType rarerityType;
    public int reinforceStoneValue;
    private EquipOption equipOption = new EquipOption();
    public int itemNumber;
    public EquipOption EquipOption { get { return equipOption; } }

    public void Init(EquipData equipData)
    {
        this.equipData = equipData;
        //texture = Resources.LoadAll<Sprite>("Equipment/" + equipData.equipment_esset);
        texture = equipData.GetTexture;
        icon = equipData.Geticon;
        itemName = equipData.GetItemName;

        switch (equipData.equipmenttype)
        {
            case 1:
                equipType = EquipType.Cloth;
                break;
            case 2:
                equipType = EquipType.Weapon;
                break;
            case 3:
                equipType = EquipType.Pants;
                break;
            case 4:
                equipType = EquipType.Face;
                break;
            case 5:
                equipType = EquipType.Hair;
                break;
            case 6:
                equipType = EquipType.Cloak;
                break;
        }
        if (equipData.equipment_rating == "C")
        { rarerityType = RarerityType.C; }
        else if (equipData.equipment_rating == "B")
        { rarerityType = RarerityType.B; }
        else if (equipData.equipment_rating == "A")
        { rarerityType = RarerityType.A; }
        else if (equipData.equipment_rating == "S")
        { rarerityType = RarerityType.S; }
        else if (equipData.equipment_rating == "SS")
        { rarerityType = RarerityType.SS; }
        else
        { rarerityType = RarerityType.SSS; }
        reinforceStoneValue = equipData.reinforcement_value;

        if (equipType == EquipType.Face)
        {
            icon = texture[0];
        }
    }

    public bool SetEquipStat((OptionType, float) a)
    {
        return equipOption.AddOption(a.Item1, a.Item2);
    }

    public void SetEquipItem(EquipType equipType, RarerityType rarerityType, int reinforceStone)
    {
        this.equipType = equipType;
        this.rarerityType = rarerityType;
        reinforceStoneValue = reinforceStone;
    }
}
