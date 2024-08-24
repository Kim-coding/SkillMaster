using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentInfoPanel : MonoBehaviour
{
    public ItemInfoPanel currentItemPanel;
    public ItemInfoPanel newItemPanel;

    public EquipItemSlot newItemSlot;

    public Button equipButton;


    private void Awake()
    {
        equipButton.onClick.AddListener(ChangeEquipOnPanel);
    }

    public void Init()
    {
    }
    public void OnItemSlotClick(Equip equip, EquipItemSlot itemSlot)
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

    public void DecomposPanel()
    {
        GameMgr.Instance.uiMgr.uiInventory.OnDecomposMode();
        GameMgr.Instance.uiMgr.uiInventory.DecomposSelect(newItemSlot);
        GameMgr.Instance.uiMgr.uiInventory.OpenDecomposPanel();
        ClosePanel();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void ChangeEquipOnPanel()
    {
        GameMgr.Instance.uiMgr.uiInventory.ChangeEquip(newItemSlot);
        ClosePanel();
    }
}
