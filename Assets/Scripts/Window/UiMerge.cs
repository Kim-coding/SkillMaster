using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class UiMerge : MonoBehaviour
{
    public SkillSpawner skillSpawner;
    public TextMeshProUGUI skillReserveCount;
    public TextMeshProUGUI skillSpawnCount;

    private float spawnTime = 0f;
    private float spawnCooldown = 2f; //TO-DO 테이블에서


    private void Update()
    {
        spawnTime += Time.deltaTime;
        {
            if (spawnTime > spawnCooldown)
            {
                spawnTime = 0f;
                if(GameMgr.Instance.playerMgr.playerEnhance.currentSpawnSkillCount >= GameMgr.Instance.playerMgr.playerEnhance.maxSpawnSkillCount)
                {
                    return;
                }

                GameMgr.Instance.playerMgr.playerEnhance.currentSpawnSkillCount++; //TO-DO 이걸 UI 에서 해야되나?
                SkillCountUpdate();
            }
        }
    }
    public void SkillCountUpdate()
    {
        skillReserveCount.text = "보유 (" + GameMgr.Instance.playerMgr.skillBallControllers.Count + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxReserveSkillCount + ")";
        skillSpawnCount.text = "소환\n (" + GameMgr.Instance.playerMgr.playerEnhance.currentSpawnSkillCount + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxSpawnSkillCount + ")";
    }

}
