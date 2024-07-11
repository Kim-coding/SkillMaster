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
            if (monster.TryGetComponent<MonsterAI>(out var monsterAI))
            {
                if (!IsBossBattle())
                {
                    monsterAI.gameObject.SetActive(false);
                    spawner.DestroyMonster(monsterAI);
                }
            }
        }
    }

    public void SpawnBoss()  //���� ���� ��ȯ
    {
        currentBoss = spawner.BossSpawn(BossMonsterPrefab, bossSpawnPoint);
        bossStage = true;
        //spawner.BossSpawn(Stage.BossMonsterPrefab, bossSpawnPoint);
    }

    public void RestartStage() //�������� �����
    {
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
