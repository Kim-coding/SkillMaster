using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; }

    public SceneMgr sceneMgr;
    public PlayerMgr playerMgr;
    public UIMgr uiMgr;
    public DebugMode debugMode;
    public DropItemMgr dropItemMgr;
    public CameraMove cam;
    public RewardMgr rewardMgr;
    public SoundMgr soundMgr;
    public WebTimeMgr webTimeMgr;
    public NetworkConnect networkConnect;

    private float saveTimer;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitGame();
    }

    private void InitGame()
    {
        Time.timeScale = 1f;
        playerMgr.Init();
        sceneMgr.Init();
        uiMgr.Init();
    }

    private void Update()
    {

        saveTimer += Time.deltaTime;
        if(saveTimer > 300f)
        {
            saveTimer = 0;
            SaveLoadSystem.Save();
            webTimeMgr.SaveTime();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            debugMode.gameObject.SetActive(!debugMode.isActiveAndEnabled);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SaveAndQuit();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Loading");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveLoadSystem.Save();
            Debug.Log("save");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveLoadSystem.Load();
            Instance.playerMgr.currency = SaveLoadSystem.CurrSaveData.savePlay.saveCurrency;
            Instance.playerMgr.currency.Init();
            Debug.Log("load");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SaveLoadSystem.DeleteSaveData();
        }

    }
    public void SaveAndQuit()
    {
        webTimeMgr.GetEndTime();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public MonsterPool GetMonsterPool()
    {
        return sceneMgr.mainScene.monster.GetMonsterPool();
    }

    public GameObject[] GetMonsters()
    {
        if(sceneMgr.mainScene != null)
        {
            return sceneMgr.mainScene.GetMonsters();
        }
        if(sceneMgr.dungeonScene != null)
        {
            return sceneMgr.dungeonScene.GetMonsters();
        }
        return null;
    }

    public void OnBossDefeated()
    {
        cam.SetTarget();
        sceneMgr.mainScene.playerDefeatedByBoss = false;
        sceneMgr.mainScene.AddStage();
        sceneMgr.mainScene.RestartStage();
    }

    public void OnBossSpawn()
    {
        if (!sceneMgr.mainScene.playerDefeatedByBoss)
        {
            BossSpawn();
        }
        else
        {
            ShowBossSpawnButton();
        }
    }

    public void BossSpawn()
    {
        HideBossSpawnButton();
        sceneMgr.mainScene.RemoveAllMonsters();
        sceneMgr.mainScene.SpawnBoss();
    }
    public void ShowBossSpawnButton()
    {
        uiMgr.ShowBossSpawnButton();
    }
    public void HideBossSpawnButton()
    {
        uiMgr.HideBossSpawnButton();
    }

    public void FindWebTime()
    {
        webTimeMgr = GameObject.FindGameObjectWithTag("WebTime").GetComponent<WebTimeMgr>();
    }
}
