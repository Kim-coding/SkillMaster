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

    public int currentStage = 1; //저장된 정보를 받아 오기.
    public BigInteger currentScore = new BigInteger(0);
    public bool goldDungeon = false;
    public bool diaDungeon = false;

    //골드 던전
    public GameObject goldDungeonMonster;
    public Transform GoldDungeonSpawnPoint;
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
    public TextMeshProUGUI DungeonClearStageText;
    public GameObject DungeonClearPopUp;
    public Button EndButton;

    public void Init()
    {
        //받아와야 하는 정보 : 도전 스테이지, 선택한 던전 (골드, 다이아), 스킬
        currentStage = 1;

        if(goldDungeon)
        {
            goldDungeonData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(currentStage);
            EndButton.onClick.AddListener(LoadMainScene);
        }
        if(diaDungeon)
        {
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

        }
        startPanel.SetActive(true);
    }

    private void Start()
    {
        if (goldDungeon)
        {
            var sandBag = Instantiate(goldDungeonMonster, GoldDungeonSpawnPoint.position, Quaternion.identity, Parent);
            monster.Add(sandBag);
        }
        if (diaDungeon)
        {
            SpawnNextBoss();
        }
        
    }

    void Update()
    {
        if (goldDungeon)
        {
            timer += Time.deltaTime;
            if (timer > endTime)
            {
                Time.timeScale = 0f;
                EndGoldDungeon(true, currentStage - 1);
            }
        }
        if (diaDungeon)
        {
            timer += Time.deltaTime;
            if (timer > endTime)
            {
                EndDiaDungeon(true,currentStage - 1);
            }
        }
        GameMgr.Instance.uiMgr.TimeSliderUpdate();
    }

    public GameObject[] GetMonsters()
    {
        return monster.ToArray();
    }

    private void EndGoldDungeon(bool cleared, int stage)
    {
        isCleared = cleared;
        clearedStage = stage;

        ShowEndPopup();
    }

    public void EndDiaDungeon(bool cleared, int stage)
    {
        isCleared = cleared;
        clearedStage = stage;

        ShowEndPopup();
    }

    private void ShowEndPopup()
    {
        Time.timeScale = 0f;

        DungeonClearPopUp.SetActive(true);
        DungeonClearStageText.text = clearedStage.ToString();
        var reward = "0";
        if (clearedStage != 0)
        {
            reward = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(clearedStage).reward_value;
            clearRewardText.text = reward;
        }

        if(isCleared)
        {
            clearText.text = "VICTORY";
        }
        else
        {
            clearText.text = "DEFEAT";
        }

        if (goldDungeon)
        {
            dungeonText.text = "골드 던전";
        }
        if (diaDungeon)
        {
            dungeonText.text = "다이아 던전";
        }

        //메인 씬으로 넘겨야 하는 정보 : reward, DungeonClearStageText.text (클리어 보상, 클리어한 스테이지)
    }

    private void SpawnNextBoss()
    {
        if (currentBossIndex >= bossIds.Count)
        {
            EndDiaDungeon(true, currentStage);
            return;
        }

        int bossid = bossIds[currentBossIndex];
        var bossName = DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(bossid).Asset;

        if (bossPrefabDictionary.TryGetValue(bossName, out GameObject bossPrefab))
        {

            var currentBoss = Instantiate(bossPrefab, diaDungeonSpawnPoints[currentBossIndex].position, Quaternion.identity, Parent);
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

    private void LoadMainScene()
    {
        Addressables.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }
}
