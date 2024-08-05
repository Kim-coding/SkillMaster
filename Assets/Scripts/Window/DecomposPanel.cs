using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecomposPanel : MonoBehaviour
{

    private List<TextMeshProUGUI> itemTexts= new List<TextMeshProUGUI>();

    public TextMeshProUGUI explainText;
    public TextMeshProUGUI countText;

    public GameObject itemList;
    public TextMeshProUGUI itemTextPrefab;

    public Button confirmButton;
    public Button cancleButton;

    private int reinforeceCount;


    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        cancleButton.onClick.AddListener(ClosePanel);
    }

    public void SetDecompos(List<EquipItemSlot> list)
    {
        foreach(var text  in itemTexts) 
        {
            Destroy(text.gameObject);
        }
        itemTexts.Clear();

        reinforeceCount = 0;

        explainText.text = $"{list.Count}���� ��� �����Ͻðڽ��ϱ�? \n ������ ���� �ǵ��� �� �����ϴ�.";
        foreach (EquipItemSlot e in list)
        {
             var itemtext = Instantiate(itemTextPrefab, itemList.transform);
            itemtext.SetText(e.currentEquip.itemName + " - " + e.currentEquip.rarerityType);
            itemTexts.Add(itemtext);
            reinforeceCount += e.currentEquip.reinforceStoneValue;
        }

        countText.text = reinforeceCount.ToString() + " ��";
    }

    public void OnConfirmButtonClick()
    {
        GameMgr.Instance.uiMgr.uiInventory.Decompos(reinforeceCount);
        ClosePanel();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
