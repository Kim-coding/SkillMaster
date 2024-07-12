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
    private float spawnCooldown = 2f; //TO-DO ���̺���


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

                GameMgr.Instance.playerMgr.playerEnhance.currentSpawnSkillCount++; //TO-DO �̰� UI ���� �ؾߵǳ�?
                SkillCountUpdate();
            }
        }
    }
    public void SkillCountUpdate()
    {
        skillReserveCount.text = "���� (" + GameMgr.Instance.playerMgr.skillBallControllers.Count + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxReserveSkillCount + ")";
        skillSpawnCount.text = "��ȯ\n (" + GameMgr.Instance.playerMgr.playerEnhance.currentSpawnSkillCount + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxSpawnSkillCount + ")";
    }

}
