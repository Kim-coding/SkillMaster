using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform parent;
    public void SpawnMonster()
    {
        BoxCollider box = parent.GetComponent<BoxCollider>();

        if (box != null)
        {
            Vector3 boxSize = box.size;
            Vector3 boxCenter = box.center;

            float randomX = UnityEngine.Random.Range(-boxSize.x / 3, boxSize.x / 2);
            float randomY = UnityEngine.Random.Range(-boxSize.y / 2, boxSize.y / 2);
            float randomZ = UnityEngine.Random.Range(-boxSize.z / 2, boxSize.z / 3);

            Vector3 spawnPosition = parent.transform.position + boxCenter + new Vector3(randomX, randomY, randomZ);

            GameObject monster = Instantiate(monsterPrefab, spawnPosition, transform.rotation, parent.transform);
            GameMgr.Instance.HandleMonsterSpawned(monster);
        }
        else
        {
            Debug.LogError("Parent object does not have a BoxCollider component.");
        }
    }
    public void DestroyMonster(GameObject monster)
    {
        GameMgr.Instance.HandleMonsterDestroyed(monster);
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
