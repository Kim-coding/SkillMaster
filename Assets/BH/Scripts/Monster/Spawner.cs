using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public event Action<GameObject> OnMonsterSpawned;
    public event Action<GameObject> OnMonsterDestroyed;
    public GameObject monsterPrefab;
    public Transform parent;
    public void SpawnMonster(GameObject monsterPrefab)
    {
        GameObject monster = Instantiate(monsterPrefab, transform.position, transform.rotation, parent);
        OnMonsterSpawned?.Invoke(monster);
    }

    public void DestroyMonster(GameObject monster)
    {
        OnMonsterDestroyed?.Invoke(monster);
        Destroy(monster);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMonster(monsterPrefab);
        }
    }
}
