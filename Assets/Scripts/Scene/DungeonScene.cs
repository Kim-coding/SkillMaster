using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonScene : MonoBehaviour
{
    public GameObject[] GoldDungeonMonster;   // ���̺� ���� �ʿ�
    public GameObject[] DiaDungeonMonsters; // ���̺� ���� �ʿ�

    public int currentStage = 1; //TO-DO SaveLoad ���� �ʿ� 

    public Transform GoldDungeonSpawnPoint;
    public Transform[] DiaDungeonSpawnPoints;
    
    public bool goldDungeon = false;
    public bool diaDungeon = false;

    public Transform Parent;

    private List<GameObject> monster = new List<GameObject>();

    public void Init()
    {
        currentStage = 1;


    }

    private void Start()
    {
        if (goldDungeon)
        {
            var sandBag = Instantiate(GoldDungeonMonster[0], GoldDungeonSpawnPoint.position, Quaternion.identity, Parent);
            monster.Add(sandBag);
        }
        if (diaDungeon)
        {
            
        }
        
    }

    void Update()
    {
        
    }

    public GameObject[] GetMonsters()
    {
        if (goldDungeon)
        {
            return monster.ToArray();
        }
        if (diaDungeon)
        {
            return monster.ToArray();
        }
        return null;
    }
}
