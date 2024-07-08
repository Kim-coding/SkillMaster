using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    private GameMgr gameMgr;
    public PlayerStat playerStat;

    private void Start()
    {
        gameMgr = GameMgr.Instance;
        playerStat = new PlayerStat();
        playerStat.Init();
    }

    public GameObject[] RequestMonsters()
    {
        return gameMgr.GetMonsters();
    }
}
