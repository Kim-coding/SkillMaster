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

    public void HandleMonsterSpawned(GameObject monster)
    {
        sceneMgr.mainScene.monster.AddMonsters(monster);
    }

    public void HandleMonsterDestroyed(GameObject monster)
    {
        sceneMgr.mainScene.monster.RemoveMonsters(monster);
    }

}
