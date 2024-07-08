using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private static MonsterPool monsterPool;
    public MonsterAI monsterPrefab;
    public Transform poolParent;
    private List<GameObject> monsters = new List<GameObject>();

    private void Awake()
    {
        if (monsterPool == null)
        {
            InitializeMonsterPool();
        }
    }

    private void InitializeMonsterPool()
    {
        monsterPool = new MonsterPool(monsterPrefab, poolParent);
    }

    public MonsterPool GetMonsterPool()
    {
        return monsterPool;
    }

    public void AddMonsters(GameObject monster)
    {
        monsters.Add(monster);
    }

    public void RemoveMonsters(GameObject monster)
    {
        monsters.Remove(monster);
    }

    public GameObject[] GetMonsters()
    {
        return monsters.ToArray();
    }
}
