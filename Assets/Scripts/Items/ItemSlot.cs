using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int slotIndex {  get; set; }
    public Button button;
    public Image selectedBorder;

    public Image itemImage;
    public Equip currentEquip = null;
    public int customOrder;

    public bool onSelected = false;

    private void Awake()
    {
        TryGetComponent<Button>(out button);
        if(button != null)
        {
            button.onClick.AddListener(OnbuttonClick);
        }
    }

    public void SetData(Equip equipData)
    {
        if (equipData != null)
        {
            currentEquip = equipData;
            itemImage.sprite = equipData.icon;
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
        Debug.Log(currentEquip.equipType + " / " + currentEquip.rarerityType + " / " + currentEquip.itemNumber + " / " + currentEquip.EquipOption.currentOptions.Count);
        foreach (var equip in currentEquip.EquipOption.currentOptions)
        {
            Debug.Log(equip.Item1 + " / " + equip.Item2);
        }
        SetData(GameMgr.Instance.playerMgr.playerinventory.EquipItem(currentEquip));
        GameMgr.Instance.uiMgr.uiInventory.UiSlotUpdate(currentEquip.equipType);

        switch(currentEquip.equipType)
        {

            case EquipType.None:
                break;
            case EquipType.Hair:
                if(currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseHair)
                {
                    Destroy(gameObject);
                }
                break;
            case EquipType.Face:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseFace)
                {
                    Destroy(gameObject);
                }
                break;
            case EquipType.Cloth:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseCloth)
                {
                    Destroy(gameObject);
                }
                break;
            case EquipType.Pants:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.basePant)
                {
                    Destroy(gameObject);
                }
                break;
            case EquipType.Weapon:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseWeapon)
                {
                    Destroy(gameObject);
                }
                break;
            case EquipType.Cloak:
                if (currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseCloak)
                {
                    Destroy(gameObject);
                }
                break;
        }

    }


}
