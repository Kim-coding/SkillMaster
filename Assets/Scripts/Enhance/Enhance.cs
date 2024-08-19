using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Enhance : MonoBehaviour
{
    public TextMeshProUGUI enhanceName;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI maxLevelText;
    public TextMeshProUGUI currentPercentText;
    public TextMeshProUGUI nextPercentText;
    public TextMeshProUGUI nextCostText;
    public Image icon;

    public EnhanceButton button;

    public void Init(string name, int i)
    {
        enhanceName.text = name;
        if(i == 1)
        {
            icon.sprite = Resources.Load<Sprite>("Icon_Money");
        }
        else
        {
            icon.sprite = Resources.Load<Sprite>("Icon_Gem03_Diamond_Blue");
        }
    }

    private void MaxLvCheck(int lv,int maxlv)
    {
        if(lv == maxlv)
        {
            nextPercentText.text = "max";
            nextCostText.text = "max";
            button.GetComponent<Button>().interactable = false;
            button.disableEvents = true;
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

}
