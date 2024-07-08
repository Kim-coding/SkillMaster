using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    private GameMgr gameMgr;

    private void Start()
    {
        gameMgr = GameMgr.Instance;
    }

    public GameObject[] RequestMonsters()
    {
        return gameMgr.GetMonsters();
    }
}
