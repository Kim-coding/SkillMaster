using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    private List<GameObject> monsters = new List<GameObject>();

    public void AddMonster(GameObject monster)
    {
        if (!monsters.Contains(monster))
        {
            monsters.Add(monster);
        }
    }
    public void RemoveMonster(GameObject monster)
    {
        if (monsters.Contains(monster))
        {
            monsters.Remove(monster);
        }
    }

    public GameObject[] GetMonsters()
    {
        return monsters.ToArray();
    }
}
