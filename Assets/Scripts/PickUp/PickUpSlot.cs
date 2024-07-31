using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSlot : MonoBehaviour
{
    public Button button;
    public Image selectedBorder;

    public Image itemImage;
    public Equip currentEquip = null;

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

    public void OnbuttonClick()
    {
        GameMgr.Instance.uiMgr.uiWindow.pickUpItemPanel.gameObject.SetActive(true);
        GameMgr.Instance.uiMgr.uiWindow.pickUpItemPanel.SetItemInfoPanel(currentEquip);
    }


}
