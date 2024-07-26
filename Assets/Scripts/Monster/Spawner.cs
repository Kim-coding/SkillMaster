using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MainScene scene;
    private MonsterPool monsterPool;

    private int startSpawnMonsterCount;
    public Transform[] spawnPoints;
    private Transform BossParent;

    private int deathCount = 0;
    private const int deathThreshold = 3;

    public float spawnRange;

    private float timer = 0f;
    private float duration = 0.2f;

    private bool isSpawn = false;
    private bool isDeathCount = false;

    private void Start()
    {
        monsterPool = GameMgr.Instance.GetMonsterPool();
        if (monsterPool == null)
        {
            Debug.LogError("Monster pool is not initialized properly.");
            return;
        }
        InitialSpawn();
    }

    public void InitialSpawn()
    {
        deathCount = 0;
        startSpawnMonsterCount = 25; //TO-DO : 테이블 연동 
        int monstersPerZoneMin = 3; // 최대수 / 8
        int monstersPerZoneMax = 4;

        foreach (var zone in spawnPoints) 
        {
            if(startSpawnMonsterCount <= 0)
                return;

            int monsterSpawnCount = UnityEngine.Random.Range(monstersPerZoneMin, monstersPerZoneMax + 1);
            monsterSpawnCount = Mathf.Min(monsterSpawnCount, startSpawnMonsterCount);
            Debug.Log(monsterSpawnCount);
            SpawnMonsters(zone, monsterSpawnCount);
            startSpawnMonsterCount -= monsterSpawnCount;
        }
    }

    public void SpawnMonsters(Transform zone, int count)
    {
        if (GameMgr.Instance.sceneMgr.mainScene.IsBossBattle()) return;
        if (monsterPool.pool.Count >= monsterPool.MaxCapacity)  // TO-DO : pool.Count가 매 스테이지 2씩 증가 MaxCapacity를 넘어서면 스폰이 정지
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            MonsterAI monster = monsterPool.Get();
            if (monster == null)
            {
                Debug.LogError("Monster retrieved from pool is null!");
                continue;
            }
            Vector2 randomPosition = UnityEngine.Random.insideUnitCircle * spawnRange;
            monster.transform.position = zone.position + new Vector3(randomPosition.x, randomPosition.y, 0);
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
            int randomZoneIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            SpawnMonsters(spawnPoints[randomZoneIndex], 3);
            deathCount -= 3;
        }
    }


    public GameObject BossSpawn(GameObject bossPrefab, Transform spawnPoint)
    {
        BossParent = scene.monster.poolParent;
        //GameMgr.Instance.soundMgr.PlaySFX("Boss");
        return Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity, BossParent);
    }

    public void DestroyMonster(MonsterAI monster)
    {
        if (monsterPool == null)
        {
            monsterPool = GameMgr.Instance.GetMonsterPool();
        }

        if (monster != null)
        {
            monsterPool.Return(monster);
            OnMonsterDeath();
        }
        else
        {
            Debug.LogError("Attempted to destroy a null monster.");
        }
    }
}
