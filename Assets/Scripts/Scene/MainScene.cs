
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public Spawner spawner;
    public Monster monster;
    public GameObject[] BossMonsterPrefabs;  //스테이지에서 현재 스폰될 보스를 가지고 와야 함
    public Transform bossSpawnPoint;
    //public Stage stage;  // 스폰 되어야 하는 몬스터 등을 가지고 있게 해야함
    private GameObject currentBoss;
    private bool bossStage = false;
    private MonsterPool monsterPool;

    private StageData stageData;

    private int appearBossMonster;
    public int stageCount;
    public int stageId;

    public ClearPopup clearPopup;

    private CharacterStat playerCharacter;

    public bool playerDefeatedByBoss = false;
    public void Init()
    {
        stageId = 50001; //TO-DO 저장된곳에서 가져오기
        stageData = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(stageId);
        if(stageData != null)
        {
            stageCount = stageData.StageLv;
            appearBossMonster = stageData.appearBossMonster;
        }
    }

    private void Start()
    {
        monsterPool = GameMgr.Instance.GetMonsterPool();
        playerCharacter = GameMgr.Instance.playerMgr.characters[0];
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

    public void AddStage()
    {
        clearPopup.gameObject.SetActive(true);
        EventMgr.TriggerEvent(QuestType.Stage);
        stageId++;
        stageData = DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID(stageId);
        if(stageData != null)
        {
            stageCount = stageData.StageLv;
            appearBossMonster = stageData.appearBossMonster;
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

    //public void SpawnBoss()  //보스 몬스터 소환
    //{
    //    bossStage = true;
    //    currentBoss = spawner.BossSpawn(BossMonsterPrefabs[0], bossSpawnPoint);
    //    var bossAi = currentBoss.GetComponent<BossAI>();
    //    bossAi.bossStat.SetBossID(DataTableMgr.Get<StageTable>(DataTableIds.stage).GetID
    //       (stageId).appearBossMonster);
    //    bossAi.bossStat.Init();
    //    AddMonsters(currentBoss);
    //    GameMgr.Instance.cam.SetTarget(currentBoss.transform.GetChild(0).gameObject);
    //    GameMgr.Instance.soundMgr.PlaySFX("Boss");

    //    playerCharacter.GetComponent<PlayerSkills>().SetList();
    //}

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
    }

    public void RestartStage() //스테이지 재시작
    {
        GameMgr.Instance.uiMgr.ResetMonsterSlider();
        RemoveAllMonsters();
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
