using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClear : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        if (GameMgr.Instance.sceneMgr.mainScene.stageCount == 1)
        {
            Time.timeScale = 0f;
        }
        GameMgr.Instance.OnBossDefeated();
    }
}
