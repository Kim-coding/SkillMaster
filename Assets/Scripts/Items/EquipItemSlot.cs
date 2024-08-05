using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemSlot : MonoBehaviour
{
    public int slotIndex { get; set; }
    public Button button;
    public Image selectedBorder;

    public Image itemImage;
    public Equip currentEquip = null;
    public int customOrder;

    public bool onSelected = false;

    private void Awake()
    {
        TryGetComponent<Button>(out button);
        if (button != null)
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
        if (GameMgr.Instance.uiMgr.uiInventory.decomposMode)
        {
            OnSelected(GameMgr.Instance.uiMgr.uiInventory.DecomposSelect(this));
            return;
        }

        GameMgr.Instance.uiMgr.uiWindow.equipmentItemPanel.OnItemSlotClick(currentEquip, this);
    }

    public void OnSelected(bool isOn)
    {
        selectedBorder.gameObject.SetActive(isOn);
        onSelected = true;
    }
}
