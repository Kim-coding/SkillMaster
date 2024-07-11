using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    private GameMgr gameMgr;
    public PlayerStat playerStat;
    public PlayerCurrency currency;

    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI diamondUI;

    public void Init()
    {
        gameMgr = GameMgr.Instance;
        currency = new PlayerCurrency();
        currency.Init();
        playerStat.Init();
    }

    public GameObject[] RequestMonsters()
    {
        if (gameMgr == null)
        { return null; }
        return gameMgr.GetMonsters();
    }
}
