using System.Collections;
using System.Collections.Generic;
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
    }

    public void Init()
    {
        gameMgr = GameMgr.Instance;
        currency = new PlayerCurrency();
        playerStat = new PlayerStat();
        playerEnhance = new PlayerEnhance();
        currency.Init();
        playerStat.Init(1,1,1,1,10f,2f, playerBaseStat.baseSpeed, playerBaseStat.baseAttackSpeed, playerBaseStat.baseAttackRange);
        playerEnhance.Init();

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
        playerStat.Init(1, 1, 1, 1, 10f, 2f, playerBaseStat.baseSpeed, playerBaseStat.baseAttackSpeed, playerBaseStat.baseAttackRange);
    }
}
