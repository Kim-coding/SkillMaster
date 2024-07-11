using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    private GameMgr gameMgr;
    public PlayerStat playerStat;
    public PlayerEnhance playerEnhance;
    public PlayerCurrency currency;

    public List<SkillBallController> skillBallControllers = new List<SkillBallController>();

    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI diamondUI;

    public void Init()
    {
        gameMgr = GameMgr.Instance;
        currency = new PlayerCurrency();
        playerStat = new PlayerStat();
        playerEnhance = new PlayerEnhance();
        currency.Init();
        playerStat.Init();
        playerEnhance.Init();
    }

    public GameObject[] RequestMonsters()
    {
        if (gameMgr == null)
        { return null; }
        return gameMgr.GetMonsters();
    }
}
