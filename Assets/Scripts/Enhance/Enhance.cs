using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enhance : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI enhanceName;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nextPercentText;
    public TextMeshProUGUI nextCostText;

    private float clickTime;
    private float holdInterval = 0.1f;
    private bool isClick = false;
    private bool lonkClick = false;

    public delegate void ButtonClickAction();
    public event ButtonClickAction buttonClick;

    public void Init(string name)
    {
        enhanceName.text = name;
    }

    public void TextUpdate(int Level,int currentPercent, int nextPercent , BigInteger cost)
    {
        levelText.text = Level.ToString();
        nextPercentText.text = $"{currentPercent} -> {nextPercent}";
        nextCostText.text = cost.ToStringShort();
    }

    public void TextUpdate(int Level, float currentPercent, float nextPercent, BigInteger cost)
    {
        levelText.text = Level.ToString();
        currentPercent = Mathf.Round(currentPercent * 100f)/100f;
        nextPercent = Mathf.Round(nextPercent * 100f)/100f;
        nextPercentText.text = $"{currentPercent} -> {nextPercent}";
        nextCostText.text = cost.ToStringShort();
    }

    public void PercentTextUpdate(int Level, float currentPercent, float nextPercent, BigInteger cost)
    {
        levelText.text = Level.ToString();
        currentPercent = Mathf.Round(currentPercent * 100f) / 100f;
        nextPercent = Mathf.Round(nextPercent * 100f) / 100f;
        nextPercentText.text = $"{currentPercent}% -> {nextPercent}%";
        nextCostText.text = cost.ToStringShort();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
        lonkClick = false;
        clickTime = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClick = false;
        if(lonkClick == false)
        {
            buttonClick.Invoke();
        }
    }

    private void Update()
    {
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
