using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MainScene scene;
    private MonsterPool monsterPool;

    public Transform[] spawnPoints;
    private Transform BossParent;

    private int deathCount = 0;
    private const int deathThreshold = 3;

    //float timer;
    //float duration = 1f;

    private void Start()
    {
        monsterPool = GameMgr.Instance.GetMonsterPool();

        InitialSpawn();
    }

    public void InitialSpawn()
    {
        int startSpawnMonsterCount = 25;
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
        if (GameMgr.Instance.sceneMgr.mainScene.IsBossBattle()) return;
        if (monsterPool.pool.Count >= monsterPool.MaxCapacity)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            MonsterAI monster = monsterPool.Get();
            monster.transform.position = zone.position;
            monster.transform.rotation = Quaternion.identity;
            scene.AddMonsters(monster.gameObject);
        }
    }

    public void OnMonsterDeath()
    {
        if (GameMgr.Instance.sceneMgr.mainScene.IsBossBattle()) return;

        deathCount++;
        if (deathCount >= deathThreshold)
        {
            deathCount = 0;
            int randomZoneIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            SpawnMonsters(spawnPoints[randomZoneIndex], 3);
        }
    }

    public GameObject BossSpawn(GameObject bossPrefab, Transform spawnPoint)
    {
        BossParent = scene.monster.poolParent;
        return Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity, BossParent);
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
}
