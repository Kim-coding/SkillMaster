using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum InfoStringID
{
    info47 = 120547,
    info48,
    info49,
    info50,
    info51,
    info52,
    info53,
    info54,
    info55,
}

public class Help : MonoBehaviour
{
    public GameObject infoPopupPanel;
    public TextMeshProUGUI infoPopupText;
    public InfoStringID infoStringID;
    public bool imageInfo = false;

    public void OpendPopup()
    {
        infoPopupPanel.SetActive(true);
        if (!imageInfo)
        {
            infoPopupText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID((int)infoStringID);
        }
    }

    public void ClosePopup()
    {
        infoPopupPanel.SetActive(false);
    }
}
