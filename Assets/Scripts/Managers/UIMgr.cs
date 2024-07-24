using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public UiWindow uiWindow;
    public UiMerge uiMerge;
    public UiEnhance uiEnhance;
    public UiGuideQuest uiGuideQuest;
    public UiInventory uiInventory;

    /// <summary>
    /// 밑에것들 정리 필요
    /// </summary>

    public Slider monsterSlider;
    public Button bossSpawnButton;
    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI diamondUI;
    public TextMeshProUGUI stageUI;

    public TextMeshProUGUI skillcount;
    public int stageCount = 1;


    public void AllUIUpdate(BigInteger g, BigInteger d)
    {
        bossSpawnButton.gameObject.SetActive(false);
        GoldTextUpdate(g);
        DiaTextUpdate(d);
    }

    public void GoldTextUpdate(BigInteger gold)
    {
        goldUI.text = gold.ToStringShort() + " Gold";
    }

    public void DiaTextUpdate(BigInteger diamond)
    {
        diamondUI.text = diamond.ToStringShort() + " Dia";
    }

    public void MonsterSliderUpdate()
    {
        if (GameMgr.Instance.sceneMgr.mainScene.IsBossBattle()) return;

        if (monsterSlider.value < monsterSlider.maxValue)
        {
            monsterSlider.value += 1;
        }
        else if (monsterSlider.value >= monsterSlider.maxValue)
        {
            GameMgr.Instance.OnBossSpawn();
        }
    }

    public void ResetMonsterSlider()
    {
        monsterSlider.value = 0;
    }
    public void StageUpdate(int s)
    {
        stageCount = s;
        stageUI.text = $"Stage {stageCount}";
    }


}
