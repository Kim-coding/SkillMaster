using DG.Tweening;
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

    public Camera mainCam;
    public GameObject savingPowerPanel;

    private float saveTimer;
    private float saveWebTimer;

    private float bgmValue;
    private float sfxValue;

    private void Awake()
    {
        if (Instance == null)
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
        if (saveTimer > 60f)
        {
            saveTimer = 0;
            SaveLoadSystem.Save();
        }

        saveWebTimer += Time.deltaTime;
        if (saveWebTimer > 300f)
        {
            saveWebTimer = 0;
            webTimeMgr.SaveTime();
        }

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    debugMode.gameObject.SetActive(!debugMode.isActiveAndEnabled);
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveAndQuit();
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (Time.timeScale == 1f)
        //    {
        //        Time.timeScale = 0f;
        //    }
        //    else
        //    {
        //        Time.timeScale = 1f;
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    SaveLoadSystem.Save();
        //    webTimeMgr.SaveTime();
        //    Debug.Log("save");
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    SaveLoadSystem.Load();
        //    Instance.playerMgr.currency = SaveLoadSystem.CurrSaveData.savePlay.saveCurrency;
        //    Instance.playerMgr.currency.Init();
        //    Debug.Log("load");
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    SaveLoadSystem.DeleteSaveData();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    sceneMgr.tutorial.OnTutorial();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    EnterPowerSavingMode();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    ExitPowerSavingMode();
        //}
    }
    public void SaveAndQuit()
    {
        SaveLoadSystem.Save();
        StartCoroutine(webTimeMgr.GetExitTime());
    }
    public MonsterPool GetMonsterPool()
    {
        return sceneMgr.mainScene.monster.GetMonsterPool();
    }

    public GameObject[] GetMonsters()
    {
        if (sceneMgr.mainScene != null)
        {
            return sceneMgr.mainScene.GetMonsters();
        }
        if (sceneMgr.dungeonScene != null)
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
        uiMgr.ShowBossHpBar();
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

    public int CalculateAttackIncrease(int value, int range, int level)
    {
        int baseIncrease = value;
        int increasePerLevel = 0;

        for (int i = 1; i <= level; i++)
        {
            if (i % range == 0)
            {
                baseIncrease += value;
            }
            increasePerLevel += baseIncrease;
        }

        return increasePerLevel;
    }

    public BigInteger CalculateCost(int defalutValue, int increase, int increaseValue, int range, int level)
    {
        BigInteger baseIncrease = new BigInteger(increase);
        BigInteger increasePerLevel = new BigInteger(defalutValue);

        for (int i = 1; i <= level; i++)
        {
            if (i % range == 0)
            {
                baseIncrease += increaseValue;
            }
            increasePerLevel += baseIncrease;
        }

        return increasePerLevel;
    }

    public BigInteger cbnCalculateCost(int defalutValue, int increase, int increaseValue, int level)
    {
        int baseIncrease = increase;
        BigInteger increasePerLevel = new BigInteger(defalutValue);

        for (int i = 0; i <= level; i++)
        {
            baseIncrease += increaseValue;
            increasePerLevel += baseIncrease;
        }

        return increasePerLevel;
    }


    public void EnterPowerSavingMode()
    {
        GameMgr.Instance.soundMgr.PlaySFX("Button");
        bgmValue = soundMgr.bgmSlider.value;
        sfxValue = soundMgr.sfxSlider.value;

        savingPowerPanel.gameObject.SetActive(true);
        savingPowerPanel.GetComponent<CanvasGroup>().DOFade(1f, 0.5f)
        .OnComplete(() =>
        {
            soundMgr.bgmSlider.value = 0f;
            soundMgr.sfxSlider.value = 0f;
            mainCam.enabled = false;  // 메인 카메라 비활성화 (렌더링 중지)
        });
    }

    public void ExitPowerSavingMode()
    {
        mainCam.enabled = true;  // 메인 카메라 활성화 (렌더링 재개)
        savingPowerPanel.GetComponent<CanvasGroup>().DOFade(0f, 0.5f)
        .OnComplete(() =>
        {
            soundMgr.bgmSlider.value = bgmValue;
            soundMgr.sfxSlider.value = sfxValue;
            savingPowerPanel.gameObject.SetActive(false);

        });
    }
}
