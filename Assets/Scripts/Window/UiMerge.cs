using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiMerge : MonoBehaviour
{
    public SkillSpawner skillSpawner;
    public TextMeshProUGUI skillcount;

    public void SkillCountUpdate()
    {
        skillcount.text = "��ȯ\n" + GameMgr.Instance.playerMgr.skillBallControllers.Count + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxSpawnCount;
    }

}
