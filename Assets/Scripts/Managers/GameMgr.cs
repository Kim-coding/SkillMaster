using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; }

    public SceneMgr sceneMgr;
    public PlayerMgr playerMgr;
    public UIMgr uiMgr;
    public DebugMode debugMode;
    public GoldMgr goldManager;
    public CameraMove cam;
    public RewardMgr rewardMgr;
    public SoundMgr soundMgr;
    public WebTimeMgr webTimeMgr;
    public NetworkConnect networkConnect;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitGame();
    }

    private void InitGame()
    {
        sceneMgr.Init();
        playerMgr.Init();
        uiMgr.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            debugMode.gameObject.SetActive(!debugMode.isActiveAndEnabled);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(SaveAndQuit());
        }
    }
    private IEnumerator SaveAndQuit()
    {
        yield return webTimeMgr.GetEndTime();
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
        sceneMgr.mainScene.playerDefeatedByBoss = false;
        uiMgr.ResetMonsterSlider();
        sceneMgr.mainScene.RestartStage();
        sceneMgr.mainScene.AddStage();
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
}
