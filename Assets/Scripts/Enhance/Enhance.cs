using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enhance : MonoBehaviour
{
    public TextMeshProUGUI enhanceName;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nextPercentText;
    public TextMeshProUGUI nextCostText;

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
}
