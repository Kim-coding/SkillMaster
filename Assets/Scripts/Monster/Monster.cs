using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private MonsterPool pool;
    private List<GameObject> monsters = new List<GameObject>();

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
