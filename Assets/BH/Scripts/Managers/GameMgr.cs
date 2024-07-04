using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; }

    public SceneMgr sceneMgr;
    public PlayerMgr playerMgr;
    public MonsterMgr monsterMgr;

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

    public void AddMonster(GameObject monster)
    {
        playerMgr.AddMonster(monster);
    }

    public void RemoveMonster(GameObject monster)
    {
        playerMgr.RemoveMonster(monster);
    }
}
