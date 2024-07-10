using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MainScene Scene;
    private MonsterPool monsterPool;

    public Transform[] spawnPoints;
    public int startSpawnMonsterCount = 25;

    private int deathCount = 0;
    private const int deathThreshold = 3;

    //float timer;
    //float duration = 1f;

    private void Start()
    {
        monsterPool = GameMgr.Instance.GetMonsterPool();

        InitialSpawn();
    }

    private void InitialSpawn()
    {
        int monstersPerZoneMin = 3;
        int monstersPerZoneMax = 4;

        foreach (var zone in spawnPoints) 
        {
            if(startSpawnMonsterCount <= 0)
                return;

            int monsterSpawnCount = UnityEngine.Random.Range(monstersPerZoneMin, monstersPerZoneMax + 1);
            monsterSpawnCount = Mathf.Min(monsterSpawnCount, startSpawnMonsterCount);
            SpawnMonsters(zone, monsterSpawnCount);
            startSpawnMonsterCount -= monsterSpawnCount;
        }
    }

    public void SpawnMonsters(Transform zone, int count)
    {
        if (monsterPool.pool.Count >= monsterPool.MaxCapacity)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            MonsterAI monster = monsterPool.Get();
            monster.transform.position = zone.position;
            monster.transform.rotation = Quaternion.identity;
            Scene.AddMonsters(monster.gameObject);
        }
    }

    public void OnMonsterDeath()
    {
        deathCount++;
        if (deathCount >= deathThreshold)
        {
            deathCount = 0;
            int randomZoneIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            SpawnMonsters(spawnPoints[randomZoneIndex], 3);
        }
    }

    public void SpawnMonster()
    {
        if ( monsterPool.pool.Count >= monsterPool.MaxCapacity)
        {
            return;
        }

        BoxCollider box = GameMgr.Instance.sceneMgr.mainScene.monster.poolParent.GetComponent<BoxCollider>();

        if (box != null)
        {
            MonsterAI monster = monsterPool.Get();
            monster.transform.position = transform.position;
            monster.transform.rotation = transform.rotation;
            Scene.AddMonsters(monster.gameObject);
        }
        else
        {
            Debug.LogError("Parent object does not have a BoxCollider component.");
        }
    }

    public void DestroyMonster(MonsterAI monster)
    {
        if (monsterPool == null)
        {
            monsterPool = GameMgr.Instance.GetMonsterPool();
        }

        monsterPool.Return(monster);
        OnMonsterDeath();
    }

    private void Update()
    {

    }
}
