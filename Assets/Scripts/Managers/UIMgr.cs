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

    public Slider DungeonScoreSlider;
    public Slider DungeonTimeSlider;
    public TextMeshProUGUI timeText;
    private float timer;
    private float maxTime;

    private BigInteger currentMaxDamage;
    public TextMeshProUGUI dungeonScoreText;

    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI diamondUI;
    public TextMeshProUGUI stageUI;

    public TextMeshProUGUI skillcount;
    public int stageCount = 1;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI goldLvText;
    public TextMeshProUGUI diaText;
    public TextMeshProUGUI diaLvText;

    public void Init()
    {
        uiInventory.Init();
        if(bossSpawnButton != null)
        {
            bossSpawnButton.onClick.AddListener(OnBossSpawnButtonClicked);
            bossSpawnButton.gameObject.SetActive(false);
        }
        var playerInfo = GameMgr.Instance.playerMgr.playerInfo;
        if (goldText != null)
        {
            goldLvText.text = playerInfo.goldDungeonLv.ToString();
            diaLvText.text = playerInfo.diaDungeonLv.ToString();
            goldText.text = new BigInteger(DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(playerInfo.goldDungeonLv).reward_value).ToStringShort();
            diaText.text = DataTableMgr.Get<DiaDungeonTable>(DataTableIds.diaDungeon).GetID(playerInfo.diaDungeonLv).reward_value;
        }
        if(GameMgr.Instance.sceneMgr.dungeonScene != null)
        {
            maxTime = DataTableMgr.Get<DiaDungeonTable>(DataTableIds.diaDungeon).GetID(playerInfo.diaDungeonLv).timelimt;
        }
        if (GameMgr.Instance.sceneMgr.mainScene != null)
        {
            maxTime = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(GameMgr.Instance.sceneMgr.mainScene.stageId).timelimit;
        }

    }

    public void AllUIUpdate(BigInteger g, BigInteger d)
    {
        if (bossSpawnButton != null)
            bossSpawnButton.gameObject.SetActive(false);
        GoldTextUpdate(g);
        DiaTextUpdate(d);
        
    }

    public void GoldTextUpdate(BigInteger gold)
    {
        goldUI.text = gold.ToStringShort();
    }

    public void DiaTextUpdate(BigInteger diamond)
    {
        diamondUI.text = diamond.ToString();
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
        Debug.Log(GameMgr.Instance.sceneMgr.mainScene.stageId);
        DungeonTimeSlider.gameObject.SetActive(false);
        monsterSlider.maxValue = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(GameMgr.Instance.sceneMgr.mainScene.stageId).huntvalue;
        monsterSlider.value = 0;
    }
    public void StageUpdate(int s)
    {
        stageCount = s;
        stageUI.text = $"스테이지 {stageCount}";
    }

    private void OnBossSpawnButtonClicked()
    {
        GameMgr.Instance.BossSpawn();
    }

    public void ShowBossSpawnButton()
    {
        bossSpawnButton.gameObject.SetActive(true);
    }
    public void HideBossSpawnButton()
    {
        bossSpawnButton.gameObject.SetActive(false);
    }
    public void ScoreSliderUpdate(BigInteger currentDamage, BigInteger maxDamage)
    {
        if (currentDamage > maxDamage)
        {
            currentDamage = new BigInteger(maxDamage);
        }

        float percent = 0f;

        if (maxDamage.factor - 1 > currentDamage.factor)
        {
            percent = 0f;
        }
        else if (maxDamage.factor > currentDamage.factor)
        {
            float max = maxDamage.numberList[maxDamage.factor - 1] * 1000 + maxDamage.numberList[maxDamage.factor - 2];
            float damage = currentDamage.numberList[currentDamage.factor - 1];
            percent = damage / max;
        }
        else if (maxDamage.factor < currentDamage.factor)
        {
            percent = 1f;
        }
        else
        {
            percent = 
            (float)currentDamage.numberList[currentDamage.factor - 1] 
            / maxDamage.numberList[maxDamage.factor - 1];
        }

        DungeonScoreSlider.value = percent;
    }
    public void InitializeNextStageSlider(int lv)
    {
        if (DungeonScoreSlider != null)
        {
            DungeonScoreSlider.value = 0;

            dungeonScoreText.text = $"{lv} 레벨";
        }
    }

    public void ResetTimer()
    {
        timer = maxTime;
        DungeonTimeSlider.maxValue = maxTime;
        DungeonTimeSlider.minValue = 0f;
        DungeonTimeSlider.value = maxTime;
    }
    public void TimeSliderUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timeText.text = $"{timer.ToString("F1")}s";

            DungeonTimeSlider.value = timer;
        }
        else
        {
            timer = 0;
            timeText.text = "0.0s";
            DungeonTimeSlider.value = 0;
        }

        if(timer == 0)
        {
            //보스전 타임 아웃 : 플레이어 패배 처리
            //메인씬, 던전씬 분기
        }
    }
}
