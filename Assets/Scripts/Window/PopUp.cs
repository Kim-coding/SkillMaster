using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPopUp : MonoBehaviour
{

    public TextMeshProUGUI text;


    public void SetText(string message)
    {
        text.text = message;
    }

    
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }


}
