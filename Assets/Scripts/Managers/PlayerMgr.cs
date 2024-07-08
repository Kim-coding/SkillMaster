using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    private GameMgr gameMgr;
    public PlayerStat playerStat;
    public PlayerCurrency currency;

    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI diamondUI;

    private void Start()
    {
        gameMgr = GameMgr.Instance;
        playerStat = new PlayerStat();
        currency = new PlayerCurrency();
        currency.Init();
        playerStat.Init();
        textUpdate();
    }

    public GameObject[] RequestMonsters()
    {
        return gameMgr.GetMonsters();
    }

    public void textUpdate()
    {
        goldUI.text = currency.gold.ToString() + " Gold";
        diamondUI.text = currency.diamond.ToString() + " Dia";
    }

}
