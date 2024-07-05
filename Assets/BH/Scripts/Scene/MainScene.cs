using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public Spawner spawner;
    public Monster monster;
    //public Stage stage;  // 스폰 되어야 하는 몬스터 등을 가지고 있게 해야함

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
}
