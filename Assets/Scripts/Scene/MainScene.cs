using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public Spawner spawner;
    public Monster monster;
    //public Stage stage;  // ���� �Ǿ�� �ϴ� ���� ���� ������ �ְ� �ؾ���

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
