using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enhance : MonoBehaviour
{
    public TextMeshProUGUI enhanceName;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI nextPercentText;
    public TextMeshProUGUI nextCostText;

    public void Init(string name)
    {
        enhanceName.text = name;
    }

    public void TextUpdate(int nextLevel,int currentPercent, int nextPercent , BigInteger cost)
    {
        nextLevelText.text = nextLevel.ToString();
        nextPercentText.text = $"{currentPercent}% -> {nextPercent}%";
        nextCostText.text = cost.ToStringShort();
    }

    public void TextUpdate(int nextLevel, float currentPercent, float nextPercent, BigInteger cost)
    {
        nextLevelText.text = nextLevel.ToString();
        currentPercent = Mathf.Round(currentPercent * 100f)/100f;
        nextPercent = Mathf.Round(nextPercent * 100f)/100f;
        nextPercentText.text = $"{currentPercent}% -> {nextPercent}%";
        nextCostText.text = cost.ToStringShort();
    }
}
