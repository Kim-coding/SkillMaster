using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaDungeonBossClear : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        GameMgr.Instance.sceneMgr.dungeonScene.OnBossDeath();
    }
}
