using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiMerge : MonoBehaviour
{
    public SkillSpawner skillSpawner;
    public TextMeshProUGUI skillReserveCount;
    public TextMeshProUGUI skillSpawnCount;

    public Button spawnButton;

    public void SkillCountUpdate()
    {
        skillReserveCount.text = "���� (" + GameMgr.Instance.playerMgr.skillBallControllers.Count + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxReserveSkillCount + ")";
        skillSpawnCount.text = "��ȯ\n (" + GameMgr.Instance.playerMgr.playerEnhance.currentSpawnSkillCount + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxSpawnSkillCount + ")";
    }

    public void SpawnButtonUpdate(bool a)
    {
        spawnButton.interactable = a;
    }

}
