using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public Spawner spawner;
    public Monster monster;
    public GameObject BossMonsterPrefab;  //������������ ���� ������ ������ ������ �;� ��
    public Transform bossSpawnPoint;
    //public Stage stage;  // ���� �Ǿ�� �ϴ� ���� ���� ������ �ְ� �ؾ���
    private GameObject currentBoss;
    private bool bossStage = false;
    private MonsterPool monsterPool;

    public int stageCount;
    public int stageId;

    public void Init()
    {
        stageId = 50001; //TO-DO ����Ȱ����� ��������
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

    public void RemoveAllMonsters()  //������ ���� �� ��� ���� ����
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

    public void SpawnBoss()  //���� ���� ��ȯ
    {
        bossStage = true;
        currentBoss = spawner.BossSpawn(BossMonsterPrefab, bossSpawnPoint);
        AddMonsters(currentBoss);
        GameMgr.Instance.cam.SetTarget(currentBoss);
        //spawner.BossSpawn(Stage.BossMonsterPrefab, bossSpawnPoint);
    }

    public void RestartStage() //�������� �����
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
