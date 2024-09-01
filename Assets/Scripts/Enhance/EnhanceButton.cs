using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhanceButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ScrollRect scrollRect;

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
        scrollRect.enabled = false;
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
        scrollRect.enabled = true;
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
