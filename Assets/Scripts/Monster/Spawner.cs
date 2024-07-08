using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MainScene Scene;
    public GameObject monsterPrefab;
    public Transform parent;
    public void SpawnMonster()
    {
        BoxCollider box = parent.GetComponent<BoxCollider>();

        if (box != null)
        {
            GameObject monster = Instantiate(monsterPrefab, transform.position, transform.rotation, parent.transform);
            Scene.AddMonsters(monster);
        }
        else
        {
            Debug.LogError("Parent object does not have a BoxCollider component.");
        }
    }
    public void DestroyMonster(GameObject monster)
    {
        Scene.RemoveMonsters(monster);
        Destroy(monster);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMonster();
        }
    }
}
