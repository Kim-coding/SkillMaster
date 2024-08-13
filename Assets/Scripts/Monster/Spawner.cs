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

    private bool shouldSpawn = false;
    private int spawnCount = 0;

    private float spawnDelayTimer = 0f;
    public float spawnDelay = 0.15f;

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
    private void Update()
    {
        if (shouldSpawn)
        {
            spawnDelayTimer += Time.deltaTime;
            if(spawnDelayTimer > spawnDelay)
            {
                shouldSpawn = false;
                spawnDelayTimer = 0f;
                int randomZoneIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
                SpawnMonsters(spawnPoints[randomZoneIndex], spawnCount);
            }
        }
        if (deathCount >= deathThreshold && !shouldSpawn)
        {
            deathCount -= 3;
            shouldSpawn = true;
            spawnCount = 3;
        }
    }
    public void InitialSpawn()
    {
        deathCount = 0;
        startSpawnMonsterCount = 25; //TO-DO : 테이블 연동 

        if (monsterPool.pool.Count > 0)
        {
            foreach (var monster in monsterPool.pool)
            {
                if (monster != null)
                {                
                    monster.gameObject.SetActive(false);
                }
            }
        }

        int monstersPerZoneMin = 3; // 최대수 / 8
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

        if (monster != null && !monster.isReturnedToPool)
        {
            monster.isReturnedToPool = true;
            monsterPool.Return(monster);
            OnMonsterDeath();
        }
    }
}
