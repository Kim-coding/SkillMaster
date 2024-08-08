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
    public TextMeshProUGUI maxLevelText;
    public TextMeshProUGUI currentPercentText;
    public TextMeshProUGUI nextPercentText;
    public TextMeshProUGUI nextCostText;

    private float clickTime;
    private float holdInterval = 0.1f;
    private bool isClick = false;
    private bool lonkClick = false;
    public bool disableEvents = false;

    public delegate void ButtonClickAction();
    public event ButtonClickAction buttonClick;

    public void Init(string name)
    {
        enhanceName.text = name;
    }

    private void MaxLvCheck(int lv,int maxlv)
    {
        if(lv == maxlv)
        {
            nextPercentText.text = "max";
            nextCostText.text = "max";
            GetComponent<Button>().interactable = false;
            disableEvents = true;
        }
    }

    public void TextUpdate(int Level,int maxLevel,int currentPercent, int nextPercent , BigInteger cost)
    {
        levelText.text = "Lv." + Level.ToString();
        maxLevelText.text = "(maxLv." + maxLevel.ToString() + ")";
        currentPercentText.text = $"{currentPercent}";
        nextPercentText.text = $"{nextPercent}";
        nextCostText.text = cost.ToStringShort();
        MaxLvCheck(Level, maxLevel);
    }

    public void TextUpdate(int Level, int maxLevel, float currentPercent, float nextPercent, BigInteger cost)
    {
        levelText.text = "Lv." + Level.ToString();
        maxLevelText.text = "(maxLv." + maxLevel.ToString() + ")";
        currentPercent = Mathf.Round(currentPercent * 100f)/100f;
        nextPercent = Mathf.Round(nextPercent * 100f)/100f;
        currentPercentText.text = $"{currentPercent}";
        nextPercentText.text = $"{nextPercent}";
        nextCostText.text = cost.ToStringShort();
        MaxLvCheck(Level, maxLevel);

    }

    public void PercentTextUpdate(int Level, int maxLevel, float currentPercent, float nextPercent, BigInteger cost)
    {
        levelText.text = "Lv." + Level.ToString();
        maxLevelText.text = "(maxLv." + maxLevel.ToString() + ")";
        currentPercent = Mathf.Round(currentPercent * 100f) / 100f;
        nextPercent = Mathf.Round(nextPercent * 100f) / 100f;
        currentPercentText.text = $"{currentPercent}%";
        nextPercentText.text = $"{nextPercent}%";
        nextCostText.text = cost.ToStringShort();
        MaxLvCheck(Level, maxLevel);

    }

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
        if(lonkClick == false)
        {
            buttonClick.Invoke();
        }
    }

    private void Update()
    {
        if(disableEvents)
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
