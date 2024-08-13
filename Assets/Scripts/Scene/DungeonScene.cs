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

    public GameObject GoldDungeonMonster;   // 테이블 연결 필요
    public GameObject[] DiaDungeonMonsters; // 테이블 연결 필요

    public Transform GoldDungeonSpawnPoint;
    public Transform[] DiaDungeonSpawnPoints;
    
    public bool goldDungeon = false;
    public bool diaDungeon = false;

    private List<GameObject> monster = new List<GameObject>();
    
    public Transform Parent;
    public Slider slider;

    public GoldDungeonData goldDungeonData;
    //public DiaDungeonData diaDungeonData;

    private float timer = 0f;
    private float endTime = 30f;

    private bool isCleared = false;
    private int clearedStage;

    public GameObject goldDungeonClearPopUp;
    public Button goldEndButton;
    public TextMeshProUGUI clearCompensationText;

    public TextMeshProUGUI goldDungeonClearStageText;

    public void Init()
    {
        //받아와야 하는 정보 : 도전 스테이지, 선택한 던전 (골드, 다이아), 스킬
        currentStage = 1;

        if(goldDungeon)
        {
            goldDungeonData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(currentStage);
            goldEndButton.onClick.AddListener(LoadMainScene);
        }
        if(diaDungeon)
        {
            //diaDungeonData = 
        }
        startPanel.SetActive(true);
    }

    private void Start()
    {
        if (goldDungeon)
        {
            var sandBag = Instantiate(GoldDungeonMonster, GoldDungeonSpawnPoint.position, Quaternion.identity, Parent);
            monster.Add(sandBag);
        }
        if (diaDungeon)
        {
            
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
            GameMgr.Instance.uiMgr.TimeSliderUpdate();
        }
        if (diaDungeon)
        {
            timer += Time.deltaTime;
            if (timer > endTime)
            {
                EndDiaDungeon(true,currentStage - 1);
            }
        }
    }

    public GameObject[] GetMonsters()
    {
        if (goldDungeon)
        {
            return monster.ToArray();
        }
        if (diaDungeon)
        {
            return monster.ToArray();
        }
        return null;
    }

    private void EndGoldDungeon(bool cleared, int stage)
    {
        isCleared = cleared;
        clearedStage = stage;

        ShowEndPopup();
    }

    private void EndDiaDungeon(bool cleared, int stage)
    {
        isCleared = cleared;
        clearedStage = stage;

        ShowEndPopup() ;
    }

    private void ShowEndPopup()
    {
        if(goldDungeon)
        {
            goldDungeonClearPopUp.SetActive(true);
            goldDungeonClearStageText.text = clearedStage.ToString();
            var rewardGold = "0";
            if (clearedStage != 0)
            {
                rewardGold = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(clearedStage).reward_value;
                clearCompensationText.text = rewardGold;
            }
            //메인 씬으로 넘겨야 하는 정보 : rewardGold, goldDungeonClearStageText.text (클리어 보상, 클리어한 스테이지)
        }
        if (diaDungeon)
        {

        }
    }

    private void LoadMainScene()
    {
        //SceneManager.LoadScene("Main");
        Addressables.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }
}
