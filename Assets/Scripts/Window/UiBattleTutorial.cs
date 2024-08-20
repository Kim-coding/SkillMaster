using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TextPosition
{
    Top,
    Bottom,
}

public class UiBattleTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextPosition[] textPositions;

    public TextMeshProUGUI topText;
    public TextMeshProUGUI bottomText;

    public int currentTutorialID = 120221;
    public int currentTutorial = 0;

    private void Start()
    {

    }

    public void UiUpdate()
    {
        if (currentTutorialID > 120286)
        {
            return;
        }

        if (textPositions[currentTutorial] == TextPosition.Top)
        {
            bottomText.transform.gameObject.SetActive(false);
            topText.transform.gameObject.gameObject.SetActive(true);
            topText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(currentTutorialID);
        }
        else if (textPositions[currentTutorial] == TextPosition.Bottom)
        {
            topText.transform.gameObject.gameObject.SetActive(false);
            bottomText.transform.gameObject.gameObject.SetActive(true);
            bottomText.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(currentTutorialID);
        }

        currentTutorial++;
        currentTutorialID++;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            UiUpdate();
        }
    }
}
