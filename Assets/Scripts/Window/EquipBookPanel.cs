using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class EquipBookPanel : MonoBehaviour
{
    public Image icon;
    public Image rarityColor;

    public Button exit;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemInfo;


    public void SetEquipBookPanel(int equipID)
    {
        gameObject.SetActive(true);
        var equipData = DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(equipID);

        icon.sprite = equipData.Geticon;
        if (equipData.equipmenttype == 4)
        {
            icon.sprite = equipData.GetTexture[0];
        }
        Color newColor = Color.white;
        switch (equipData.equipment_rating)
        {
            case "Basic":
                ColorUtility.TryParseHtmlString("#C4C4C4", out newColor);
                break;
            case "C":
                ColorUtility.TryParseHtmlString("#97846B", out newColor);
                break;
            case "B":
                ColorUtility.TryParseHtmlString("#6AAC8D", out newColor);
                break;
            case "A":
                ColorUtility.TryParseHtmlString("#A4BDFF", out newColor);
                break;
            case "S":
                ColorUtility.TryParseHtmlString("#C188D7", out newColor);
                break;
            case "SS":
                ColorUtility.TryParseHtmlString("#F4C56B", out newColor);
                break;
            case "SSS":
                ColorUtility.TryParseHtmlString("#C74B46", out newColor);
                break;
        }

        rarityColor.color = newColor;


        itemName.text = equipData.GetItemName;
        itemInfo.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(equipData.explain_id);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
