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
    public UiTutorial uiTutorial;
    public UIBook uiBook;
    public UIDungeon uIDungeon;

    /// <summary>
    /// 밑에것들 정리 필요
    /// </summary>

    public Slider monsterSlider;
    public Button bossSpawnButton;
    public Slider bossHpBar;

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
    public bool goldLvMax = false;
    private BigInteger last;
    private BigInteger cutLine;
    private BigInteger preCutLine;

    public InputField playerNameInputField;
    public GameObject nameInputPanel;
    public Button nameSubmitButton;

    public GameObject UnlistedListPanel;
    public TextMeshProUGUI UnlistedList;

    public GameObject storyPanel;
    public GameObject[] stroyImages;
    private int storyIndex = 0;

    public bool isStory = false;

    public void Init()
    {
        uiInventory.Init();
        if(bossSpawnButton != null)
        {
            bossSpawnButton.onClick.AddListener(OnBossSpawnButtonClicked);
            bossSpawnButton.gameObject.SetActive(false);
        }
        var playerInfo = GameMgr.Instance.playerMgr.playerInfo;
        if (GameMgr.Instance.sceneMgr.dungeonScene != null)
        {
            maxTime = DataTableMgr.Get<DiaDungeonTable>(DataTableIds.diaDungeon).GetID(playerInfo.diaDungeonLv).timelimt;
        }
        if (GameMgr.Instance.sceneMgr.mainScene != null)
        {
            maxTime = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(GameMgr.Instance.sceneMgr.mainScene.stageId).timelimit;
        }
        if(GameMgr.Instance.sceneMgr.mainScene != null)
        {
            uIDungeon.Init();
            uiBook.Init();
        }
        var dungeonData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).goldDungeonDatas;
        last = new BigInteger(dungeonData[dungeonData.Count - 1].request_damage);
        cutLine = new BigInteger(0);
        preCutLine = new BigInteger(0);

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

    public void ShowBossHpBar()
    {
        monsterSlider.gameObject.SetActive(false);
        bossHpBar.gameObject.SetActive(true);
        bossHpBar.value = 1f;
    }

    public void ResetMonsterSlider()
    {
        bossHpBar.gameObject.SetActive(false);
        DungeonTimeSlider.gameObject.SetActive(false);
        monsterSlider.gameObject.SetActive(true);
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
    public void ScoreSliderUpdate(BigInteger currentDamage)
    {
        var dungeonData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).goldDungeonDatas;

        if(goldLvMax)
        {
            DungeonScoreSlider.value = 0;
            return;
        }

        if (currentDamage > cutLine)
        {
            int i = 0;
            foreach (var dungeon in dungeonData)
            {
                if (cutLine < currentDamage)
                {
                    i++;
                    preCutLine = new BigInteger(cutLine);
                    cutLine = new BigInteger(dungeon.request_damage);
                    if (cutLine >= last)
                    {
                        goldLvMax = true;
                    }
                }
                else
                {
                    break;
                }
            }
            dungeonScoreText.text = $"{i} 레벨";
            GameMgr.Instance.sceneMgr.dungeonScene.currentStage = i;
        }
        BigInteger curdamage = new BigInteger(currentDamage);
        BigInteger maxScore = new BigInteger(new BigInteger(cutLine) - new BigInteger(preCutLine));
        BigInteger curScore = new BigInteger(new BigInteger(curdamage) - new BigInteger(preCutLine));

        float percent = 0f;

        if (maxScore.factor >= curScore.factor + 2)
        {
            percent = 0f;
        }
        else if(maxScore.factor > curScore.factor)
        {
            float max = maxScore.numberList[maxScore.factor - 1] * 1000 + maxScore.numberList[maxScore.factor - 2];
            float damage = curScore.numberList[curScore.factor - 1];
            percent = damage / max;
        }
        else
        {
            percent = 
            (float)curScore.numberList[curScore.factor - 1] 
            / maxScore.numberList[maxScore.factor - 1];
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
    }

    public void CloseUnlistedListPanel()  //패널과 버튼
    {
        UnlistedListPanel.SetActive(false);
    }

    public void OnStory()
    {
        if(!isStory)
        {
            return;
        }

        storyPanel.SetActive(true);
        if (stroyImages.Length > 0)
        {
            storyIndex = 0;
            ShowStoryImage(storyIndex);
        }
    }

    public void NextStory()
    {
        if (storyIndex < stroyImages.Length)
        {
            stroyImages[storyIndex].gameObject.SetActive(false);
        }

        storyIndex++;

        if (storyIndex < stroyImages.Length)
        {
            ShowStoryImage(storyIndex);
        }
        else
        {
            storyPanel.SetActive(false);
        }
    }

    private void ShowStoryImage(int index)
    {
        if (index >= 0 && index < stroyImages.Length)
        {
            stroyImages[index].gameObject.SetActive(true);
        }
    }
}
