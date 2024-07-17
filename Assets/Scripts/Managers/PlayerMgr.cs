using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    private GameMgr gameMgr;

    public List<CharacterStat> characters = new List<CharacterStat>(); // 플레이어 캐릭터들 접근

    public PlayerStat playerStat;
    public PlayerBaseStat playerBaseStat;
    public PlayerEnhance playerEnhance;
    public PlayerCurrency currency;
    public PlayerInfomation playerInfo;

    public List<SkillBallController> skillBallControllers = new List<SkillBallController>();

    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI diamondUI;

    private float spawnTime = 0f;
    private float spawnCooldown = 2f; //TO-DO 테이블에서

    private void Update()
    {
        spawnTime += Time.deltaTime;
        {
            if (spawnTime > spawnCooldown)
            {
                spawnTime = 0f;
                if (GameMgr.Instance.playerMgr.playerEnhance.currentSpawnSkillCount >= GameMgr.Instance.playerMgr.playerEnhance.maxSpawnSkillCount)
                {
                    return;
                }
                if(skillBallControllers.Count < GameMgr.Instance.playerMgr.playerEnhance.maxReserveSkillCount)
                {
                    GameMgr.Instance.uiMgr.uiMerge.SpawnButtonUpdate(true);
                }
                GameMgr.Instance.playerMgr.playerEnhance.currentSpawnSkillCount++;
                GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();
            }
        }


        if(Input.GetKeyDown(KeyCode.Space)) {
        
            Debug.Log(currency.gold.ToString());
            Debug.Log(playerEnhance.attackPowerCost.ToString());
        }

    }

    public void Init()
    {
        gameMgr = GameMgr.Instance;
        currency = new PlayerCurrency();
        playerStat = new PlayerStat();
        playerEnhance = new PlayerEnhance();
        playerInfo = new PlayerInfomation();
        currency.Init();
        StatSetting();
        playerEnhance.Init();
        playerInfo.Init();

        playerBaseStat.onSettingChange += StatSetting;
    }

    private void OnDestroy()
    {
        playerBaseStat.onSettingChange -= StatSetting;
    }

    public GameObject[] RequestMonsters()
    {
        if (gameMgr == null)
        { return null; }
        return gameMgr.GetMonsters();
    }


    private void StatSetting()
    {
        playerStat.Init(
            playerBaseStat.basePlayerAttackPower,
            playerBaseStat.basePlayerMaxHealth,
            playerBaseStat.basePlayerDefence,
            playerBaseStat.basePlayerHealthRecovery,
            playerBaseStat.basePlayerCriticalPercent,
            playerBaseStat.basePlayerCriticalMultiple,
            playerBaseStat.baseSpeed,
            playerBaseStat.baseAttackSpeed,
            playerBaseStat.baseAttackRange);
    }
}
