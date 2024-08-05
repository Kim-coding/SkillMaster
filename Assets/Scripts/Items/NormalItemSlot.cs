using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NormalItemSlot : MonoBehaviour
{
    public int slotIndex { get; set; }
    public Button button;
    public Image selectedBorder;

    public Image itemImage;
    public Item currentItem = null;
    public int itemCount;
    public TextMeshProUGUI itemCountText;

    public bool onSelected = false;

    private void Awake()
    {
        TryGetComponent<Button>(out button);
        if (button != null)
        {
            button.onClick.AddListener(OnbuttonClick);
        }
    }

    public void itemCountUpdate()
    {
        itemCountText.text = itemCount.ToString();
    }

    public void OnbuttonClick()
    {
        GameMgr.Instance.uiMgr.uiWindow.normalItemPanel.gameObject.SetActive(true);
        GameMgr.Instance.uiMgr.uiWindow.normalItemPanel.SetItemInfoPanel(currentItem);
    }
}
