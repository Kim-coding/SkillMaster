using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enhance : MonoBehaviour
{

    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI nextPercentText;
    public TextMeshProUGUI nextCostText;

    public void TextUpdate(int nextLevel,int currentPercent, int nextPercent , BigInteger cost)
    {
        nextLevelText.text = nextLevel.ToString();
        nextPercentText.text = $"{currentPercent}% -> {nextPercent}%";
        nextCostText.text = cost.ToStringShort();

    }
}
