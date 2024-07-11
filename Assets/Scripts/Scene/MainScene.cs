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

    public void RemoveAllMonsters()  //������ ���� �� ��� ���� ����
    {
        GameObject[] monsters = GetMonsters();
        foreach (GameObject monster in monsters)
        {
            if (monster.GetComponent<MonsterAI>())
            {
                if (!IsBossBattle())
                {
                    monster.gameObject.SetActive(false);
                    monsterPool.Return(monster.GetComponent<MonsterAI>());
                }
            }
        }
    }

    public void SpawnBoss()  //���� ���� ��ȯ
    {
        bossStage = true;
        currentBoss = spawner.BossSpawn(BossMonsterPrefab, bossSpawnPoint);
        AddMonsters(currentBoss);
        //spawner.BossSpawn(Stage.BossMonsterPrefab, bossSpawnPoint);
    }

    public void RestartStage() //�������� �����
    {
        if(currentBoss != null)
        {
            RemoveMonsters(currentBoss);
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
