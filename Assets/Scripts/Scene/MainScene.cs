using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public Spawner spawner;
    public Monster monster;
    public GameObject BossMonsterPrefab;  //스테이지에서 현재 스폰될 보스를 가지고 와야 함
    public Transform bossSpawnPoint;
    //public Stage stage;  // 스폰 되어야 하는 몬스터 등을 가지고 있게 해야함
    private GameObject currentBoss;
    private bool bossStage = false;
    private MonsterPool monsterPool;

    public int stageCount;
    public int stageId;

    public void Init()
    {
        stageId = 50001; //TO-DO 저장된곳에서 가져오기
        stageCount = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(stageId).StageLv;
    }

    private void Start()
    {
        monsterPool = GameMgr.Instance.GetMonsterPool();
    }

    public GameObject[] GetMonsters()
    {
        return monster.GetMonsters();
    }

    public void AddMonsters(GameObject m)
    {
        monster.AddMonsters(m);
    }

    public void RemoveMonsters(GameObject m)
    {
        monster.RemoveMonsters(m);
    }

    public void AddStage()
    {
        EventMgr.TriggerEvent(QuestType.Stage);
        stageId++;
        stageCount = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(stageId).StageLv;
        var monsters = monster.GetMonsters();
        foreach (GameObject monsterai in monsters) 
        {
            if (monsterai.GetComponent<MonsterAI>() == null)
            {
                continue;
            }
            monsterai.GetComponent<MonsterAI>().monsterStat.SetID(DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID
           (GameMgr.Instance.sceneMgr.mainScene.stageId).appearMonster);
            monsterai.GetComponent<MonsterAI>().monsterStat.Init();
        }
        GameMgr.Instance.uiMgr.StageUpdate(stageCount);
    }

    public void RemoveAllMonsters()  //보스전 진입 전 모든 몬스터 정리
    {
        GameObject[] monsters = GetMonsters();
        foreach (GameObject monster in monsters)
        {
            if (monster != null && monster.TryGetComponent<MonsterAI>(out var monsterAI))
            {
                if (!IsBossBattle())
                {
                    monsterAI.gameObject.SetActive(false);
                    monsterPool.Return(monsterAI);
                    RemoveMonsters(monsterAI.gameObject);
                }
            }
            else
            {
                RemoveMonsters(monster.gameObject);
            }
        }
    }

    public void SpawnBoss()  //보스 몬스터 소환
    {
        bossStage = true;
        currentBoss = spawner.BossSpawn(BossMonsterPrefab, bossSpawnPoint);
        AddMonsters(currentBoss);
        GameMgr.Instance.cam.SetTarget(currentBoss);
        //spawner.BossSpawn(Stage.BossMonsterPrefab, bossSpawnPoint);
    }

    public void RestartStage() //스테이지 재시작
    {
        GameMgr.Instance.uiMgr.ResetMonsterSlider();
        RemoveAllMonsters();
        if(currentBoss != null)
        {
            Destroy(currentBoss);
        }
        bossStage = false;
        spawner.InitialSpawn();
    }

    public bool IsBossBattle()
    {
        return bossStage;
    }
}
