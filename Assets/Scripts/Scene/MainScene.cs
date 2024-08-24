
using UnityEngine;
using UnityEngine.UIElements;

public class MainScene : MonoBehaviour
{
    public Spawner spawner;
    public Monster monster;
    
    public Transform bossSpawnPoint;
    public SpriteRenderer background;
    
    private GameObject currentBoss;
    public bool bossStage = false;
    private MonsterPool monsterPool;

    private StageData stageData;

    private string backgroundAsset;
    public int appearBossMonster;
    public int stageCount;
    public int stageId;

    public ClearPopup clearPopup;

    private CharacterStat playerCharacter;

    public bool playerDefeatedByBoss = false;
    public void Init()
    {
        var data = SaveLoadSystem.CurrSaveData.savePlay;

        if(data != null)
        {
            stageId = data.stageId;
        }
        else
        {
            stageId = 50001;
        }
        stageData = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(stageId);
        if(stageData != null)
        {
            stageCount = stageData.StageLv;
            appearBossMonster = stageData.appearBossMonster;
            backgroundAsset = stageData.Asset;
        }

        background.sprite = Resources.Load<Sprite>($"Background/{backgroundAsset}");
        GameMgr.Instance.uiMgr.StageUpdate(stageCount);
    }

    private void Start()
    {
        monsterPool = GameMgr.Instance.GetMonsterPool();
        playerCharacter = GameMgr.Instance.playerMgr.characters[0];
    }

    private void Update()
    {
        if(bossStage)  //TO-DO : MainScene에서 TimeSlider 붙이기.
        {
            GameMgr.Instance.uiMgr.TimeSliderUpdate();
        }
    }

    public GameObject[] GetMonsters()
    {
        return monster.GetMonsters();
    }

    public void AddMonsters(GameObject m)
    {
        monster.AddMonsters(m);
    }

    public void RemoveMonsters(GameObject m)
    {
        monster.RemoveMonsters(m);
    }

    public void StageWarp(int i)
    {
        stageId = 50000 + i;
        stageData = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(stageId);
        if (stageData == null)
        {
            return;
        }

        stageCount = stageData.StageLv;
        appearBossMonster = stageData.appearBossMonster;
        backgroundAsset = stageData.Asset;

        var monsters = monster.GetMonsters();
        foreach (GameObject monsterai in monsters)
        {
            if (monsterai.GetComponent<MonsterAI>() == null)
            {
                continue;
            }
            monsterai.GetComponent<MonsterAI>().monsterStat.SetID(stageData.appearMonster);
            monsterai.GetComponent<MonsterAI>().monsterStat.Init();
        }

        background.sprite = Resources.Load<Sprite>($"Background/{backgroundAsset}");
        GameMgr.Instance.uiMgr.StageUpdate(stageCount);
        RestartStage();

        SaveLoadSystem.DeleteSaveData();
    }


    public void AddStage()
    {
        clearPopup.gameObject.SetActive(true);
        EventMgr.TriggerEvent(QuestType.Stage);
        if(stageId - 50000 != DataTableMgr.Get<StageTable>(DataTableIds.stage).stageDatas.Count)
        {
            stageId++;
        }
        stageData = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(stageId);
        if(stageData != null)
        {
            stageCount = stageData.StageLv;
            appearBossMonster = stageData.appearBossMonster;
            backgroundAsset = stageData.Asset;
        }

        var monsters = monster.GetMonsters();
        foreach (GameObject monsterai in monsters) 
        {
            if (monsterai.GetComponent<MonsterAI>() == null)
            {
                continue;
            }
            monsterai.GetComponent<MonsterAI>().monsterStat.SetID(stageData.appearMonster);
            monsterai.GetComponent<MonsterAI>().monsterStat.Init();
        }

        background.sprite = Resources.Load<Sprite>($"Background/{backgroundAsset}");
        GameMgr.Instance.uiMgr.StageUpdate(stageCount);
    }

    public void RemoveAllMonsters()  //보스전 진입 전 모든 몬스터 정리
    {
        GameObject[] monsters = GetMonsters();
        foreach (GameObject monster in monsters)
        {
            if (monster != null && monster.TryGetComponent<MonsterAI>(out var monsterAI))
            {
                if (!IsBossBattle() && !monsterAI.isReturnedToPool)
                {
                    monsterAI.isReturnedToPool = true;
                    monsterAI.gameObject.SetActive(false);
                    monsterPool.Return(monsterAI);
                    RemoveMonsters(monsterAI.gameObject);
                }
            }
            else
            {
                RemoveMonsters(monster.gameObject);
            }
        }
    }

    public void SpawnBoss()  //보스 몬스터 소환
    {
        bossStage = true;

        var bossData = DataTableMgr.Get<BossTable>(DataTableIds.boss).GetID(appearBossMonster);
        string bossPrefab = null;
        if( bossData != null )
        {
            bossPrefab = bossData.Asset;
        }

        var boss = Resources.Load<GameObject>($"Boss/{ bossPrefab}");

        currentBoss = spawner.BossSpawn(boss, bossSpawnPoint);
        var bossAi = currentBoss.GetComponent<BossAI>();
        bossAi.bossStat.SetBossID(appearBossMonster);
        bossAi.bossStat.Init();
        AddMonsters(currentBoss);
        GameMgr.Instance.cam.SetTarget(currentBoss.transform.GetChild(0).gameObject);
        GameMgr.Instance.soundMgr.PlaySFX("Boss");

        playerCharacter.GetComponent<PlayerSkills>().SetList();
        GameMgr.Instance.uiMgr.DungeonTimeSlider.gameObject.SetActive(true);
        GameMgr.Instance.uiMgr.ResetTimer();
    }

    public void RestartStage() //스테이지 재시작
    {
        RemoveAllMonsters();
        GameMgr.Instance.uiMgr.ResetMonsterSlider();
        if(currentBoss != null)
        {
            Destroy(currentBoss);
        }
        bossStage = false;
        spawner.InitialSpawn();
        playerCharacter.GetComponent<PlayerAI>().Restart();
        playerCharacter.GetComponent<PlayerSkills>().SetList();
    }

    public bool IsBossBattle()
    {
        return bossStage;
    }
    public void OnPlayerDefeated()
    {
        if (bossStage)
        {
            bossStage = false; // 보스전 종료
            playerDefeatedByBoss = true;
            GameMgr.Instance.ShowBossSpawnButton();
        }
    }
}
