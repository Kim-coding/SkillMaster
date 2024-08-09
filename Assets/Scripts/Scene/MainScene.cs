
using UnityEngine;

public class MainScene : MonoBehaviour
{
    public Spawner spawner;
    public Monster monster;
    public GameObject[] BossMonsterPrefabs;  //������������ ���� ������ ������ ������ �;� ��
    public Transform bossSpawnPoint;
    //public Stage stage;  // ���� �Ǿ�� �ϴ� ���� ���� ������ �ְ� �ؾ���
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
        stageId = 50001; //TO-DO ����Ȱ����� ��������
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

    public void RemoveAllMonsters()  //������ ���� �� ��� ���� ����
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

    //public void SpawnBoss()  //���� ���� ��ȯ
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

    public void SpawnBoss()  //���� ���� ��ȯ
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

    public void RestartStage() //�������� �����
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
            bossStage = false; // ������ ����
            playerDefeatedByBoss = true;
            GameMgr.Instance.ShowBossSpawnButton();
        }
    }
}
