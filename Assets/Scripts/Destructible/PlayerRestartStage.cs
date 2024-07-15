using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRestartStage : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        GameMgr.Instance.sceneMgr.mainScene.RestartStage();
        var characterHp = gameObject.GetComponent<PlayerAI>();
        characterHp.characterStat.Health = new BigInteger(characterHp.characterStat.maxHealth);
        characterHp.characterStat.Ondeath = false;
        characterHp.UpdateHpBar(1f);
    }
}
