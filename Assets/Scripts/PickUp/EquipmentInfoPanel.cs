using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentInfoPanel : MonoBehaviour
{
    public ItemInfoPanel currentItemPanel;
    public ItemInfoPanel newItemPanel;

    public ItemSlot newItemSlot;

    public Button equipButton;

    Dictionary<EquipType, Equip> baseEquipments;


    private void Awake()
    {
        equipButton.onClick.AddListener(ChangeEquip);

        baseEquipments = new Dictionary<EquipType, Equip>
        {
            { EquipType.Hair, GameMgr.Instance.playerMgr.playerinventory.baseHair },
            { EquipType.Face, GameMgr.Instance.playerMgr.playerinventory.baseFace },
            { EquipType.Cloth, GameMgr.Instance.playerMgr.playerinventory.baseCloth },
            { EquipType.Pants, GameMgr.Instance.playerMgr.playerinventory.basePant },
            { EquipType.Weapon, GameMgr.Instance.playerMgr.playerinventory.baseWeapon },
            { EquipType.Cloak, GameMgr.Instance.playerMgr.playerinventory.baseCloak }
        };
    }

    public void Init()
    {
    }
    public void OnItemSlotClick(Equip equip, ItemSlot itemSlot)
    {
        newItemSlot = itemSlot;
        newItemPanel.SetItemInfoPanel(equip);
        switch (equip.equipType)
        {
            case EquipType.None:
                return;
            case EquipType.Hair:
                currentItemPanel.SetItemInfoPanel(GameMgr.Instance.playerMgr.playerinventory.playerHair);
                break;
            case EquipType.Face:
                currentItemPanel.SetItemInfoPanel(GameMgr.Instance.playerMgr.playerinventory.playerFace);
                break;
            case EquipType.Cloth:
                currentItemPanel.SetItemInfoPanel(GameMgr.Instance.playerMgr.playerinventory.playerCloth);
                break;
            case EquipType.Pants:
                currentItemPanel.SetItemInfoPanel(GameMgr.Instance.playerMgr.playerinventory.playerPant);
                break;
            case EquipType.Weapon:
                currentItemPanel.SetItemInfoPanel(GameMgr.Instance.playerMgr.playerinventory.playerWeapon);
                break;
            case EquipType.Cloak:
                currentItemPanel.SetItemInfoPanel(GameMgr.Instance.playerMgr.playerinventory.playerCloak);
                break;

        }

        gameObject.SetActive(true);
    }



    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }



    public void ChangeEquip()
    {
        newItemSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.EquipItem(newItemSlot.currentEquip));
        GameMgr.Instance.uiMgr.uiInventory.UiSlotUpdate(newItemSlot.currentEquip.equipType);

        if (baseEquipments.TryGetValue(newItemSlot.currentEquip.equipType, out var baseEquip) &&
          newItemSlot.currentEquip == baseEquip)
        {
            Destroy(newItemSlot.gameObject);
        }
        ClosePanel();
    }
}
