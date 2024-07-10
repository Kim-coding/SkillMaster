using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private static MonsterPool monsterPool;
    public MonsterAI[] monsterPrefabs;
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
        monsterPool = new MonsterPool(monsterPrefabs[0], poolParent);
    }

    public MonsterPool GetMonsterPool()
    {
        return monsterPool;
    }

    public void AddMonsters(GameObject monster)
    {
        monsters.Add(monster);
        Debug.Log($"Add : {monsters.Count}");
    }

    public void RemoveMonsters(GameObject monster)
    {
        monsters.Remove(monster);
        Debug.Log($"Remove : {monsters.Count}");
    }

    public GameObject[] GetMonsters()
    {
        //Debug.Log(monsters.ToArray().Length);
        return monsters.ToArray();
    }
}
