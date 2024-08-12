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
    public PlayerInventory playerinventory;

    public List<SkillBallController> skillBallControllers = new List<SkillBallController>();

    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI diamondUI;

    private float spawnTime = 0f;
    private float baseSpawnCooldown;
    public float spawnCooldown; //TO-DO 테이블에서

    public PlayerSkills playerSkills;

    public void Start()
    {
        playerSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>();
    }

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

    public void SetBase(float duration)
    {
        baseSpawnCooldown = duration;
    }

    public List<SkillBallController> GetList()
    {
        return skillBallControllers;
    }

    public void Init()
    {
        gameMgr = GameMgr.Instance;
        currency = new PlayerCurrency();
        playerStat = new PlayerStat();
        playerEnhance = new PlayerEnhance();
        playerInfo = new PlayerInfomation();
        playerinventory = new PlayerInventory();
        currency.Init();
        StatSetting();
        EnhanceSetting();
        playerInfo.Init();
        playerinventory.Init();

        playerBaseStat.onSettingChange += StatSetting;
        playerBaseStat.onSettingChange += EnhanceSetting;
    }

    private void OnDestroy()
    {
        playerBaseStat.onSettingChange -= StatSetting;
        playerBaseStat.onSettingChange -= EnhanceSetting;
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
            playerBaseStat.baseCooldown,
            playerBaseStat.baseAttackSpeed,
            playerBaseStat.baseAttackRange,
            playerBaseStat.baseRecoveryDuration
            );
    }

    private void EnhanceSetting()
    {
        playerEnhance.Init(playerBaseStat.baseMaxReserveSkillCount,
           playerBaseStat.baseSkillSpawnCooldown,
           playerBaseStat.baseAutoSpawnCooldown,
           playerBaseStat.baseAutoMergeCooldown);
    }

    public void AddSpawnSkillCooldown(int level, float value)
    {
        spawnCooldown = baseSpawnCooldown - level * value;
    }
}
