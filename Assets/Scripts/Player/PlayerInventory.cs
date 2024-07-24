using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory
{

    public Equip baseHair;
    public Equip baseFace;
    public Equip baseCloth;
    public Equip basePant;
    public Equip baseWeapon;
    public Equip baseCloak;


    public Equip playerHair;
    public Equip playerFace;
    public Equip playerCloth;
    public Equip playerPant;
    public Equip playerWeapon;
    public Equip playerCloak;

    public int maxSlots = 150;

    public Equip EquipItem(Equip equip)
    {
        Equip currentEquip = new Equip();
        switch (equip.equipType)
        {

            case EquipType.None:
                return null;
            case EquipType.Hair:
                currentEquip = playerHair;
                playerHair = equip;
                break;
            case EquipType.Face:
                currentEquip = playerFace;
                playerFace = equip;
                break;
            case EquipType.Cloth:
                currentEquip = playerCloth;
                playerCloth = equip;
                break;
            case EquipType.Pants:
                currentEquip = playerPant;
                playerPant = equip;
                break;
            case EquipType.Weapon:
                currentEquip = playerWeapon;
                playerWeapon = equip;
                break;
            case EquipType.Cloak:
                currentEquip = playerCloak;
                playerCloak = equip;
                break;

        }
        return currentEquip;
    }
    public void RemoveItem(EquipType type)
    {
        switch (type)
        {
            case EquipType.None:
                break;
            case EquipType.Hair:
                playerHair = baseHair;
                break;
            case EquipType.Face:
                playerFace = baseFace;
                break;
            case EquipType.Cloth:
                playerCloth = baseCloth;
                break;
            case EquipType.Pants:
                playerPant = basePant;
                break;
            case EquipType.Weapon:
                playerWeapon = baseWeapon;
                break;
            case EquipType.Cloak:
                playerCloak = baseCloak;
                break;
        }
    }

    public List<Equip> GetPlayerEquips()
    {
        return playerEquips;
    }
}
