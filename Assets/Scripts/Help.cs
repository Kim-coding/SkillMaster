using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    public GameObject infoPopupPanel;
    public TextMeshProUGUI infoPopupText;
    public bool imageInfo = false;

    public void OpendPopup(int stringID)
    {
        infoPopupPanel.SetActive(true);
        if (!imageInfo)
        {
            infoPopupText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(stringID);
        }
    }

    public void ClosePopup()
    {
        infoPopupPanel.SetActive(false);
    }
}
