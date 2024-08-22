using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public int slotIndex { get; set; }
    public Button button;

    public Image itemImage;
    public Equip currentEquip = null;
    public Image rarityColor;
    public TextMeshProUGUI slotLevel;

    public bool baseEquip = true;

    private void Awake()
    {
        TryGetComponent<Button>(out button);
        button.onClick.AddListener(OnbuttonClick);
    }

    public void SetData(Equip equipData)
    {
        if (equipData != null)
        {
            currentEquip = equipData;
            itemImage.sprite = equipData.icon;
            currentEquip.SetEquipItem(equipData.equipType, equipData.rarerityType, equipData.reinforceStoneValue);
            Color newColor = Color.white;
            switch (equipData.rarerityType)
            {
                case RarerityType.None:
                    ColorUtility.TryParseHtmlString("#C4C4C4", out newColor);
                    break;
                case RarerityType.C:
                    ColorUtility.TryParseHtmlString("#97846B", out newColor);
                    break;
                case RarerityType.B:
                    ColorUtility.TryParseHtmlString("#6AAC8D", out newColor);
                    break;
                case RarerityType.A:
                    ColorUtility.TryParseHtmlString("#A4BDFF", out newColor);
                    break;
                case RarerityType.S:
                    ColorUtility.TryParseHtmlString("#C188D7", out newColor);
                    break;
                case RarerityType.SS:
                    ColorUtility.TryParseHtmlString("#F4C56B", out newColor);
                    break;
                case RarerityType.SSS:
                    ColorUtility.TryParseHtmlString("#C74B46", out newColor);
                    break;
            }
            if(GameMgr.Instance.sceneMgr.mainScene != null)
            {
                rarityColor.color = newColor;
            }
        }
       
        SlotLevelUpdate();
        
    }

    public void SetEmpty()
    {
        button.interactable = false;
        currentEquip = null;
        itemImage = null;
    }

    public void OnbuttonClick()
    {
        if (baseEquip)
        {
            return;
        }
        GameMgr.Instance.uiMgr.uiWindow.currentEquipmentPanel.gameObject.SetActive(true);
        GameMgr.Instance.uiMgr.uiWindow.currentEquipmentPanel.SetItemInfoPanel(currentEquip, this);
    }


    public void RemoveEquip()
    {
        Equip equip = currentEquip;
        switch (currentEquip.equipType)
        {

            case EquipType.None:
                break;
            case EquipType.Hair:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseHair)
                {
                    return;
                }
                else
                {
                    SetData(GameMgr.Instance.playerMgr.playerinventory.baseHair);
                    GameMgr.Instance.playerMgr.playerinventory.UnEquipItem(EquipType.Hair);
                    GameMgr.Instance.uiMgr.uiInventory.UiSlotUpdate(currentEquip.equipType);
                }
                break;
            case EquipType.Face:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseFace)
                {
                    return;
                }
                else
                {
                    SetData(GameMgr.Instance.playerMgr.playerinventory.baseFace);
                    GameMgr.Instance.playerMgr.playerinventory.UnEquipItem(EquipType.Face);
                    GameMgr.Instance.uiMgr.uiInventory.UiSlotUpdate(currentEquip.equipType);
                }
                break;
            case EquipType.Cloth:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseCloth)
                {
                    return;
                }
                else
                {
                    SetData(GameMgr.Instance.playerMgr.playerinventory.baseCloth);
                    GameMgr.Instance.playerMgr.playerinventory.UnEquipItem(EquipType.Cloth);
                    GameMgr.Instance.uiMgr.uiInventory.UiSlotUpdate(currentEquip.equipType);
                }
                break;
            case EquipType.Pants:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.basePant)
                {
                    return;
                }
                else
                {
                    SetData(GameMgr.Instance.playerMgr.playerinventory.basePant);
                    GameMgr.Instance.playerMgr.playerinventory.UnEquipItem(EquipType.Pants);
                    GameMgr.Instance.uiMgr.uiInventory.UiSlotUpdate(currentEquip.equipType);
                }
                break;
            case EquipType.Weapon:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseWeapon)
                {
                    return;
                }
                else
                {
                    SetData(GameMgr.Instance.playerMgr.playerinventory.baseWeapon);
                    GameMgr.Instance.playerMgr.playerinventory.UnEquipItem(EquipType.Weapon);
                    GameMgr.Instance.uiMgr.uiInventory.UiSlotUpdate(currentEquip.equipType);
                }
                break;
            case EquipType.Cloak:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseCloak)
                {
                    return;
                }
                else
                {
                    SetData(GameMgr.Instance.playerMgr.playerinventory.baseCloak);
                    GameMgr.Instance.playerMgr.playerinventory.UnEquipItem(EquipType.Cloak);
                    GameMgr.Instance.uiMgr.uiInventory.UiSlotUpdate(currentEquip.equipType);
                }
                break;
        }
        baseEquip = true;
        equip.currentEquip = false;
        GameMgr.Instance.uiMgr.uiInventory.InstantiateSlot(equip);
        SlotLevelUpdate();
    }


    public void SlotLevelUpdate()
    {
        if (GameMgr.Instance.sceneMgr.mainScene != null)
            return;
        var Lv = EquipRarityCheck(currentEquip.rarerityType);
        Color LvColor = new Color(128 / 255f, 128 / 255f, 128 / 255f);
        switch (currentEquip.equipType)
        {

            case EquipType.None:
                break;
            case EquipType.Hair:
                var hair = GameMgr.Instance.playerMgr.playerinventory.hairSlotUpgrade;
                if (hair >= Lv)
                {
                    slotLevel.text = "Lv." + Lv.ToString();
                    slotLevel.color = Color.red;
                }
                else
                {
                    slotLevel.text = "Lv." + hair.ToString();
                    slotLevel.color = LvColor;
                }
                break;
            case EquipType.Face:
                var face = GameMgr.Instance.playerMgr.playerinventory.faceSlotUpgrade;
                if (face >= Lv)
                {
                    slotLevel.text = "Lv." + Lv.ToString();
                    slotLevel.color = Color.red;
                }
                else
                {
                    slotLevel.text = "Lv." + face.ToString();
                    slotLevel.color = LvColor;
                }
                break;
            case EquipType.Cloth:
                var cloth = GameMgr.Instance.playerMgr.playerinventory.clothSlotUpgrade;
                if (cloth >= Lv)
                {
                    slotLevel.text = "Lv." + Lv.ToString();
                    slotLevel.color = Color.red;
                }
                else
                {
                    slotLevel.text = "Lv." + cloth.ToString();
                    slotLevel.color = LvColor;
                }
                break;
            case EquipType.Pants:
                var pants = GameMgr.Instance.playerMgr.playerinventory.pantSlotUpgrade;
                if (pants >= Lv)
                {
                    slotLevel.text = "Lv." + Lv.ToString();
                    slotLevel.color = Color.red;
                }
                else
                {
                    slotLevel.text = "Lv." + pants.ToString();
                    slotLevel.color = LvColor;
                }
                break;
            case EquipType.Weapon:
                var weapon = GameMgr.Instance.playerMgr.playerinventory.weaponSlotUpgrade;
                if (weapon >= Lv)
                {
                    slotLevel.text = "Lv." + Lv.ToString();
                    slotLevel.color = Color.red;
                }
                else
                {
                    slotLevel.text = "Lv." + weapon.ToString();
                    slotLevel.color = LvColor;
                }
                break;
            case EquipType.Cloak:
                var cloak = GameMgr.Instance.playerMgr.playerinventory.cloakSlotUpgrade;
                if (cloak >= Lv)
                {
                    slotLevel.text = "Lv." + Lv.ToString();
                    slotLevel.color = Color.red;
                }
                else
                {
                    slotLevel.text = "Lv." + cloak.ToString();
                    slotLevel.color = LvColor;
                }
                break;
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
