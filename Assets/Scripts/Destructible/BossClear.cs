using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClear : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        GameMgr.Instance.OnBossDefeated();
    }
}
