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

    public void Init()
    {
        //데이터 테이블 / 키값 받아오기
        var iconimage = Resources.LoadAll<Sprite>("Equipment/Hair_Basic");
        baseHair = new Equip(iconimage,"기본 머리", 0);
        baseHair.SetEquipItem(EquipType.Hair, RarerityType.C);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Eye_Basic");
        baseFace = new Equip(iconimage, "기본 눈", 0);
        baseFace.SetEquipItem(EquipType.Face, RarerityType.C);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Cloth_Basic");
        baseCloth = new Equip(iconimage, "기본 상의", 0);
        baseCloth.SetEquipItem(EquipType.Cloth, RarerityType.C);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Pant_Basic");
        basePant = new Equip(iconimage, "기본 바지", 0);
        basePant.SetEquipItem(EquipType.Pants, RarerityType.C);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Weapon_Basic");
        baseWeapon = new Equip(iconimage, "기본 무기", 0);
        baseWeapon.SetEquipItem(EquipType.Weapon, RarerityType.C);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Back_Basic");
        baseCloak = new Equip(iconimage, "기본 망토", 0);
        baseCloak.SetEquipItem(EquipType.Cloak, RarerityType.C);

        //저장 받아오기 없으면
        playerHair = baseHair;
        playerFace = baseFace;
        playerCloth = baseCloth;
        playerPant = basePant;
        playerWeapon = baseWeapon;
        playerCloak = baseCloak;

    }


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

    public Equip GetPlayerEquips(EquipType type)
    {
        switch (type)
        {
            case EquipType.None:
                return null;
            case EquipType.Hair:
                return playerHair;
            case EquipType.Face:
                return playerFace;
            case EquipType.Cloth:
                return playerCloth;
            case EquipType.Pants:
                return playerPant;
            case EquipType.Weapon:
                return playerWeapon;
            case EquipType.Cloak:
                return playerCloak;
        }
        return null;
    }
}
