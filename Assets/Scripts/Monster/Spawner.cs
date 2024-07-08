using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MainScene Scene;
    private MonsterPool monsterPool;

    private void Start()
    {
        monsterPool = GameMgr.Instance.GetMonsterPool();
    }
    public void SpawnMonster()
    {
        BoxCollider box = GameMgr.Instance.sceneMgr.mainScene.monster.poolParent.GetComponent<BoxCollider>();

        if (box != null)
        {
            //GameObject monster = Instantiate(monsterPrefab.gameObject, transform.position, transform.rotation, parent.transform);
            MonsterAI monster = monsterPool.Get();
            monster.transform.position = transform.position;
            monster.transform.rotation = transform.rotation;
            Scene.AddMonsters(monster.gameObject);
        }
        else
        {
            Debug.LogError("Parent object does not have a BoxCollider component.");
        }
    }

    public void DestroyMonster(MonsterAI monster)
    {
        Scene.RemoveMonsters(monster.gameObject);
        monsterPool.Return(monster);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMonster();
        }
    }
}
