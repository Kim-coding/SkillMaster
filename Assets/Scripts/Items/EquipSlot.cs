using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public int slotIndex {  get; set; }
    public Button button;

    public Image itemImage;
    public Equip currentEquip = null;
    public Image rarityColor;

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
                case RarerityType.None :
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

            rarityColor.color = newColor;
        }

    }

    public void SetEmpty()
    {
        button.interactable = false;
        currentEquip = null;
        itemImage = null;
    }

    public void OnbuttonClick()
    {
        GameMgr.Instance.uiMgr.uiWindow.currentEquipmentPanel.gameObject.SetActive(true);
        GameMgr.Instance.uiMgr.uiWindow.currentEquipmentPanel.SetItemInfoPanel(currentEquip,this);
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
    }

}
