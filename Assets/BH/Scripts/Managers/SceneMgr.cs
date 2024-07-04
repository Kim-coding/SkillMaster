using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public Spawner spawner;

    private void Start()
    {
        
    }

    private void HandleMonsterSpawned(GameObject monster)
    {
        GameMgr.Instance.AddMonster(monster);
    }

    private void HandleMonsterDestroyed(GameObject monster)
    {
        GameMgr.Instance.RemoveMonster(monster);
    }
}
