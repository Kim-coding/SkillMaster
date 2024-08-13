using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DungeonScene : MonoBehaviour
{
    public int currentStage = 1; //����� ������ �޾� ����.

    public GameObject GoldDungeonMonster;   // ���̺� ���� �ʿ�
    public GameObject[] DiaDungeonMonsters; // ���̺� ���� �ʿ�

    public Transform GoldDungeonSpawnPoint;
    public Transform[] DiaDungeonSpawnPoints;
    
    public bool goldDungeon = false;
    public bool diaDungeon = false;

    private List<GameObject> monster = new List<GameObject>();
    
    public Transform Parent;
    public Slider slider;

    public GoldDungeonData goldDungeonData;
    //public DiaDungeonData diaDungeonData;

    public void Init()
    {
        currentStage = 1;
        if(goldDungeon)
        {
            goldDungeonData = DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(currentStage);
        }
        if(diaDungeon)
        {
            //diaDungeonData = 
        }

    }

    private void Start()
    {
        if (goldDungeon)
        {
            var sandBag = Instantiate(GoldDungeonMonster, GoldDungeonSpawnPoint.position, Quaternion.identity, Parent);
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
