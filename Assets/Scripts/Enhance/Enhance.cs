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
    public Image costIcon;
    public Image enhanceIcon;

    public EnhanceButton button;

    public Image closePanel;
    public TextMeshProUGUI panelText;

    public void Init(UpgradeData data)
    {
        enhanceName.text = data.GetStringID;
        costIcon.sprite = Resources.Load<Sprite>("EnhanceIcon/Icon_Gold");
        var image = data.UpgradeIcon;
        enhanceIcon.sprite = Resources.Load<Sprite>($"EnhanceIcon/{image}");
    }

    public void Init(CombinationUpgradeData data)
    {
        enhanceName.text = data.GetCbnName;
        if (data.Pay == 1)
        {
            costIcon.sprite = Resources.Load<Sprite>($"EnhanceIcon/Icon_Gold");
        }
        else
        {
            costIcon.sprite = Resources.Load<Sprite>($"EnhanceIcon/Icon_Gem03_Diamond_Blue");
        }

        var image = data.UpgradeIcon;
        enhanceIcon.sprite = Resources.Load<Sprite>($"EnhanceIcon/{image}");
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

    public void TextUpdate(int Level, int maxLevel, string currentPercent, string nextPercent, BigInteger cost)
    {
        levelText.text = "Lv." + Level.ToString();
        maxLevelText.text = "(maxLv." + maxLevel.ToString() + ")";
        currentPercentText.text = currentPercent;
        nextPercentText.text = nextPercent;
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
