using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnhanceButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private float clickTime;
    private float holdInterval = 0.1f;
    private bool isClick = false;
    private bool lonkClick = false;
    public bool disableEvents = false;

    public delegate void ButtonClickAction();
    public event ButtonClickAction buttonClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (disableEvents)
        {
            return;
        }

        isClick = true;
        lonkClick = false;
        clickTime = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (disableEvents)
        {
            return;
        }

        isClick = false;
        if (lonkClick == false)
        {
            buttonClick.Invoke();
        }

        SaveLoadSystem.Save();

    }

    private void Update()
    {
        if (disableEvents)
        {
            return;
        }
        if (isClick)
        {
            clickTime += Time.deltaTime;
            if (clickTime >= holdInterval)
            {
                lonkClick = true;
                clickTime = 0f;
                buttonClick.Invoke();
            }
        }
    }
}
