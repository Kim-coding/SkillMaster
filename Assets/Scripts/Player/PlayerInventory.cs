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


    public int hairSlotUpgrade;
    public int faceSlotUpgrade;
    public int clothSlotUpgrade;
    public int pantSlotUpgrade;
    public int weaponSlotUpgrade;
    public int cloakSlotUpgrade;

    public int upgradeFailCount;

    public List<Equip> playerEquipItemList = new List<Equip> { };
    public List<NormalItem> playerNormalItemList = new List<NormalItem> { };
    public int maxSlots = 150;

    public float itemAttackPower;
    public float itemDeffence;
    public float itemMaxHealth;
    public float itemCriticalPercent;
    public float itemCriticalMultiple;
    public float itemRecovery;
    public float itemAttackSpeed;
    public float itemSpeed;
    public float itemGoldIncrease;
    public float itemAttackRange;

    public void Init()
    {
        //데이터 테이블 / 키값 받아오기
        var iconimage = Resources.LoadAll<Sprite>("Equipment/Hair_Basic");
        baseHair = new Equip(iconimage, "기본 머리", 0);
        baseHair.SetEquipItem(EquipType.Hair, RarerityType.C, 0);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Eye_Basic");
        baseFace = new Equip(iconimage, "기본 눈", 0);
        baseFace.SetEquipItem(EquipType.Face, RarerityType.C, 0);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Cloth_Basic");
        baseCloth = new Equip(iconimage, "기본 상의", 0);
        baseCloth.SetEquipItem(EquipType.Cloth, RarerityType.C, 0);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Pant_Basic");
        basePant = new Equip(iconimage, "기본 바지", 0);
        basePant.SetEquipItem(EquipType.Pants, RarerityType.C, 0);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Weapon_Basic");
        baseWeapon = new Equip(iconimage, "기본 무기", 0);
        baseWeapon.SetEquipItem(EquipType.Weapon, RarerityType.C, 0);
        iconimage = Resources.LoadAll<Sprite>("Equipment/Back_Basic");
        baseCloak = new Equip(iconimage, "기본 망토", 0);
        baseCloak.SetEquipItem(EquipType.Cloak, RarerityType.C, 0);

        //저장 받아오기 없으면
        playerHair = baseHair;
        playerFace = baseFace;
        playerCloth = baseCloth;
        playerPant = basePant;
        playerWeapon = baseWeapon;
        playerCloak = baseCloak;


        hairSlotUpgrade = 0;
        faceSlotUpgrade = 0;
        clothSlotUpgrade = 0;
        pantSlotUpgrade = 0;
        weaponSlotUpgrade = 0;
        cloakSlotUpgrade = 0;

        upgradeFailCount = 0;

        ItemOptionsUpdate();

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
        ItemOptionsUpdate();
        return currentEquip;
    }
    public void UnEquipItem(EquipType type)
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
        ItemOptionsUpdate();
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

    public void AddEquipItem(Equip equip)
    {
        playerEquipItemList.Add(equip);
    }

    public void RemoveEquipItem(Equip equip)
    {
        playerEquipItemList.Remove(equip);
    }

    public void ItemOptionsUpdate()
    {
        itemAttackPower = 1;
        itemDeffence = 1;
        itemMaxHealth = 1;
        itemCriticalPercent = 0;
        itemCriticalMultiple = 0;
        itemRecovery = 1;
        itemAttackSpeed = 0;
        itemSpeed = 0;
        itemGoldIncrease = 0;
        itemAttackRange = 0;

        optionSearch(playerHair);
        optionSearch(playerFace);
        optionSearch(playerCloth);
        optionSearch(playerPant);
        optionSearch(playerWeapon);
        optionSearch(playerCloak);

        GameMgr.Instance.playerMgr.playerStat.playerStatUpdate();
    }
    private void optionSearch(Equip equip)
    {
        float equipSlotUpgradePercent = 1f;
        int equipSlotUpgradeLevel = 0;

        switch (equip.equipType)
        {
            case EquipType.None:
                break;
            case EquipType.Hair:
                equipSlotUpgradeLevel = Mathf.Min(hairSlotUpgrade, EquipRarityCheck(equip.rarerityType));
                break; 
            case EquipType.Face:
                equipSlotUpgradeLevel = Mathf.Min(faceSlotUpgrade, EquipRarityCheck(equip.rarerityType));
                break;
            case EquipType.Cloth:
                equipSlotUpgradeLevel = Mathf.Min(clothSlotUpgrade, EquipRarityCheck(equip.rarerityType));
                break;
            case EquipType.Pants:
                equipSlotUpgradeLevel = Mathf.Min(pantSlotUpgrade, EquipRarityCheck(equip.rarerityType));
                break;
            case EquipType.Weapon:
                equipSlotUpgradeLevel = Mathf.Min(weaponSlotUpgrade, EquipRarityCheck(equip.rarerityType));
                break;
            case EquipType.Cloak:
                equipSlotUpgradeLevel = Mathf.Min(cloakSlotUpgrade, EquipRarityCheck(equip.rarerityType));
                break;
        }

        var currentUpgradeData = DataTableMgr.Get<EquipUpgradeTable>(DataTableIds.equipmentUpgrade).GetID(equipSlotUpgradeLevel);

        equipSlotUpgradePercent = currentUpgradeData.option_raise;

        foreach (var option in equip.EquipOption.currentOptions)
        {
            switch (option.Item1)
            {
                case OptionType.attackPower:
                    itemAttackPower += option.Item2 * equipSlotUpgradePercent;
                    break;
                case OptionType.criticalPercent:
                    itemCriticalPercent += option.Item2 * equipSlotUpgradePercent;
                    break;
                case OptionType.criticalMultiple:
                    itemCriticalMultiple += option.Item2 * equipSlotUpgradePercent;
                    break;
                case OptionType.attackSpeed:
                    itemAttackSpeed += option.Item2 * equipSlotUpgradePercent;
                    break;
                case OptionType.deffence:
                    itemDeffence += option.Item2 * equipSlotUpgradePercent;
                    break;
                case OptionType.maxHealth:
                    itemMaxHealth += option.Item2 * equipSlotUpgradePercent;
                    break;
                case OptionType.speed:
                    itemSpeed += option.Item2 * equipSlotUpgradePercent;
                    break;
                case OptionType.goldIncrease:
                    itemGoldIncrease += option.Item2 * equipSlotUpgradePercent;
                    break;
                case OptionType.recovery:
                    itemRecovery += option.Item2 * equipSlotUpgradePercent;
                    break;
                case OptionType.attackRange:
                    itemAttackRange += option.Item2 * equipSlotUpgradePercent;
                    break;
            }
        }
    }



    private int EquipRarityCheck(RarerityType rarerity)
    {
        switch (rarerity)
        {
            case RarerityType.C:
                return 10;
            case RarerityType.B:
                return 20;
            case RarerityType.A:
                return 30;
            case RarerityType.S:
                return 40;
            case RarerityType.SS:
                return 50;
            case RarerityType.SSS:
                return 60;
        }
        return int.MaxValue;
    }
}
