using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI topText;
    public TextMeshProUGUI bottomText;
    public int tutorialID;

    public void UiUpdate(int tutorialID)
    {
        if(topText.gameObject.activeSelf)
        {
            topText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(tutorialID);
        }
        else if(bottomText.gameObject.activeSelf)
        {
            bottomText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(tutorialID);
        }
    }

    private void Update()
    {
        
    }
}
