using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonScene : MonoBehaviour
{
    public GameObject startPanel;

    public int currentStage; //저장된 정보를 받아 오기.
    public BigInteger currentScore = new BigInteger(0);

    private bool dungeonMode;

    //골드 던전
    public GameObject goldDungeonMonster;
    public Transform goldDungeonSpawnPoint;
    public GoldDungeonData goldDungeonData;

    //다이아 던전
    private int currentBossIndex = 0;
    public GameObject[] diaDungeonMonsters;
    private Dictionary<string, GameObject> bossPrefabDictionary = new Dictionary<string, GameObject>();
    private List<int> bossIds = new List<int>();
    public Transform[] diaDungeonSpawnPoints;
    public DiaDungeonData diaDungeonData;

    //기타
    private List<GameObject> monster = new List<GameObject>();
    public Transform Parent;

    private float timer = 0f;
    private float endTime = 30f;

    private bool isCleared = false;
    private int clearedStage;

    public TextMeshProUGUI dungeonText;
    public TextMeshProUGUI clearText;
    public TextMeshProUGUI clearRewardText;
    public TextMeshProUGUI dungeonClearStageText;
    public GameObject startPopUp;
    public GameObject dungeonClearPopUp;
    public GameObject goldImage;
    public GameObject diaImage;
    public Button goldEndButton;
    public Button diaEndButton;
    public Button diaNextButton;

    private GameObject currentBoss;

    public int stageId;
    public int questId;
    public int questValue;
    public int tutorialId;
    public int tutorialIndex;
    public int rewardID;


    public void Init()
    {
        //받아와야 하는 정보 : 도전 스테이지, 선택한 던전 (골드, 다이아)

        var data = SaveLoadSystem.CurrSaveData.savePlay;
        tutorialId = data.tutorialID;
        tutorialIndex = data.tutorialIndex;
        stageId = data.stageId;
        questId = data.questID;
        questValue = data.questValue;
        rewardID = data.rewardID;
        dungeonMode = GameMgr.Instance.playerMgr.playerInfo.dungeonMode;

        var camera = GameObject.FindWithTag("MainCamera");
        camera.GetComponent<CameraMove>().isToggle = false;

        if (dungeonMode)
        {
            currentStage = 1;
            goldDungeonData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(currentStage);
            goldEndButton.gameObject.SetActive(true);
            goldEndButton.onClick.AddListener(LoadMainScene);
        }
        else
        {
            currentStage = GameMgr.Instance.playerMgr.playerInfo.diaDungeonLv;
            foreach (var monster in diaDungeonMonsters)
            {
                string bossName = monster.name;
                if (!bossPrefabDictionary.ContainsKey(bossName))
                {
                    bossPrefabDictionary.Add(bossName, monster);
                }
            }

            diaDungeonData = DataTableMgr.Get<DiaDungeonTable>(DataTableIds.diaDungeon).GetID(currentStage);
            
            bossIds.Add(diaDungeonData.boss1_id);
            bossIds.Add(diaDungeonData.boss2_id);
            bossIds.Add(diaDungeonData.boss3_id);

            diaEndButton.gameObject.SetActive(true);
            diaNextButton.gameObject.SetActive(true);
            diaEndButton.onClick.AddListener(LoadMainScene);
            diaNextButton.onClick.AddListener(NextDiaDungeon);
            GameMgr.Instance.uiMgr.InitializeNextStageSlider(currentStage);
        }
        startPanel.SetActive(true);
    }

    private void Start()
    {
        GameMgr.Instance.uiMgr.ResetTimer();
        if (dungeonMode)
        {
            var sandBag = Instantiate(goldDungeonMonster, goldDungeonSpawnPoint.position, Quaternion.identity, Parent);
            monster.Add(sandBag);
        }
        else
        {
            SpawnNextBoss();
        }
        
    }

    void Update()
    {
        if (dungeonMode)
        {
            timer += Time.deltaTime;
            if (timer > endTime)
            {
                EndDungeon(true, currentStage - 1);
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > endTime)
            {
                EndDungeon(false, currentStage);
            }
        }
        GameMgr.Instance.uiMgr.TimeSliderUpdate();
    }

    public GameObject[] GetMonsters()
    {
        return monster.ToArray();
    }

    public void EndDungeon(bool cleared, int stage)
    {
        isCleared = cleared;
        clearedStage = stage;

        if(isCleared)
        {
            NormalItem key = null;
            foreach (var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
            {
                if (item.itemNumber == 220003 && dungeonMode)
                {
                    key = item;
                    key.itemValue--;
                    break;
                }

                if (item.itemNumber == 220004 && !dungeonMode)
                {
                    key = item;
                    key.itemValue--;
                    break;
                }
            }
        }
        ShowEndPopup();
    }

    private void ShowEndPopup()
    {
        Time.timeScale = 0f;
        timer = 0f;

        dungeonClearPopUp.SetActive(true);
        dungeonClearStageText.text = clearedStage.ToString();
        var reward = "0";

        if(isCleared)
        {
            clearText.text = "VICTORY";
            diaNextButton.interactable = true;
            if (clearedStage != 0)
            {
                if(dungeonMode)
                {
                    reward = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(clearedStage).reward_value;
                    clearRewardText.text = new BigInteger(reward).ToStringShort();
                    GameMgr.Instance.playerMgr.playerInfo.goldDungeonLv = clearedStage;
                }
                else
                {
                    reward = DataTableMgr.Get<DiaDungeonTable>(DataTableIds.diaDungeon).GetID(clearedStage).reward_value;
                    clearRewardText.text = reward;
                    GameMgr.Instance.playerMgr.playerInfo.diaDungeonLv = ++clearedStage;
                }
            }
        }
        else
        {
            clearText.text = "DEFEAT";
            reward = "0";
            diaNextButton.interactable = false;
        }

        if (dungeonMode)
        {
            diaImage.SetActive(false);
            dungeonText.text = "골드 던전";
            GameMgr.Instance.playerMgr.currency.AddGold(new BigInteger(reward)); //골드 추가
        }
        else
        {
            goldImage.SetActive(false);
            dungeonText.text = "다이아 던전";
            GameMgr.Instance.playerMgr.currency.AddDia(new BigInteger(reward)); // 다이아 추가
        }
    }

    private void SpawnNextBoss()
    {
        if (currentBossIndex >= bossIds.Count)
        {
            EndDungeon(true, currentStage);
            return;
        }

        int bossid = bossIds[currentBossIndex];
        var bossName = DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(bossid).Asset;

        if (bossPrefabDictionary.TryGetValue(bossName, out GameObject bossPrefab))
        {

            currentBoss = Instantiate(bossPrefab, diaDungeonSpawnPoints[currentBossIndex].position, Quaternion.identity, Parent);
            var bossAi = currentBoss.GetComponent<BossAI>();
            bossAi.bossStat.SetBossID(bossid);
            bossAi.bossStat.Init(); 
            monster.Add(currentBoss);
            currentBossIndex++;
        }
        else
        {
            Debug.LogError($"Boss prefab not found for: {bossName}");
        }

    }

    private void NextDiaDungeon()
    {
        dungeonClearPopUp.SetActive(false);

        Time.timeScale =1f;
        GameMgr.Instance.uiMgr.ResetTimer();

        currentStage++;
        GameMgr.Instance.uiMgr.InitializeNextStageSlider(currentStage);
        diaDungeonData = DataTableMgr.Get<DiaDungeonTable>(DataTableIds.diaDungeon).GetID(currentStage);

        bossIds.Clear();
        bossIds.Add(diaDungeonData.boss1_id);
        bossIds.Add(diaDungeonData.boss2_id);
        bossIds.Add(diaDungeonData.boss3_id);

        currentBossIndex = 0;

        foreach (var m in monster)
        {
            Destroy(m);
        }
        monster.Clear();

        timer = 0f;

        SpawnNextBoss();
    }

    public void OnBossDeath()
    {
        monster.Remove(currentBoss);
        Destroy(currentBoss);
        SpawnNextBoss();
    }

    private void LoadMainScene()
    {
        SaveLoadSystem.Save();
        Addressables.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }
}
