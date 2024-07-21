using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private static MonsterPool monsterPool;
    public MonsterAI[] monsterPrefabs;
    public Transform poolParent;
    public List<GameObject> monsters = new List<GameObject>();

    private void Awake()
    {
        if (monsterPool == null)
        {
            InitializeMonsterPool();
        }
    }

    private void InitializeMonsterPool()
    {
        monsterPool = new MonsterPool(monsterPrefabs[0], poolParent);
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
