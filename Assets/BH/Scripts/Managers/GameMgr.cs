using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; }

    public SceneMgr sceneMgr;
    public PlayerMgr playerMgr;

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
    }
    private void Start()
    {
        sceneMgr.mainScene.spawner.OnMonsterSpawned += HandleMonsterSpawned;
        sceneMgr.mainScene.spawner.OnMonsterDestroyed += HandleMonsterDestroyed;
    }

    private void OnDestroy()
    {
        if (sceneMgr != null && sceneMgr.mainScene != null && sceneMgr.mainScene.spawner != null)
        {
            sceneMgr.mainScene.spawner.OnMonsterSpawned -= HandleMonsterSpawned;
            sceneMgr.mainScene.spawner.OnMonsterDestroyed -= HandleMonsterDestroyed;
        }
    }

    private void HandleMonsterSpawned(GameObject monster)
    {
        sceneMgr.mainScene.monster.AddMonsters(monster);
    }

    private void HandleMonsterDestroyed(GameObject monster)
    {
        sceneMgr.mainScene.monster.RemoveMonsters(monster);
    }

}
