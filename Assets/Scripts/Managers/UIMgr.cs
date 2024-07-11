using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public SkillSpawner skillSpawner;
    public Slider monsterSlider;
    public Button bossSpawnButton;
    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI diamondUI;

    public TextMeshProUGUI skillcount;

    public void AllUIUpdate(BigInteger g, BigInteger d)
    {
        bossSpawnButton.gameObject.SetActive(false);
        GoldTextUpdate(g);
        DiaTextUpdate(d);
    }

    public void GoldTextUpdate(BigInteger gold)
    {
        goldUI.text = gold.ToString() + " Gold";
    }

    public void DiaTextUpdate(BigInteger diamond)
    {
        diamondUI.text = diamond.ToString() + " Dia";
    }

    public void MonsterSliderUpdate()
    {
        if (monsterSlider.value < monsterSlider.maxValue)
        {
            monsterSlider.value += 1;
        }
        else if (monsterSlider.value == monsterSlider.maxValue)
        {
            GameMgr.Instance.OnBossSpawn();
            monsterSlider.value = 0;
        }
    }

    public void ResetMonsterSlider()
    {
        monsterSlider.value = 0;
    }


    public void SkillCountUpdate()
    {
        skillcount.text = "¼ÒÈ¯\n" + GameMgr.Instance.playerMgr.skillBallControllers.Count + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxSpawnCount;
    }
}
