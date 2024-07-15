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

        playerMgr.Init();
    }
    private void Update()
    {
      if(Input.GetKeyUp(KeyCode.Escape))
        {

            debugMode.gameObject.SetActive(!debugMode.isActiveAndEnabled);
        }   
    }

    public MonsterPool GetMonsterPool()
    {
        return sceneMgr.mainScene.monster.GetMonsterPool();
    }

    public GameObject[] GetMonsters()
    {
        return sceneMgr.mainScene.GetMonsters();
    }

    public void OnBossDefeated()
    {
        sceneMgr.mainScene.RestartStage();
        uiMgr.AddStage();
    }

    public void OnBossSpawn()
    {
        uiMgr.bossSpawnButton.gameObject.SetActive(false);
        
        sceneMgr.mainScene.RemoveAllMonsters();
        
        sceneMgr.mainScene.SpawnBoss();
    }
}
