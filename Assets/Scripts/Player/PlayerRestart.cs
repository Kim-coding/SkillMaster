using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestart : MonoBehaviour
{
    public PlayerAI player;
    public void Restart()
    {
        GameMgr.Instance.sceneMgr.mainScene.RestartStage();
        player.characterStat.Health = new BigInteger(player.characterStat.maxHealth);
        player.characterStat.Ondeath = false;
        player.UpdateHpBar(1f);
        player.CheckAndChangeState();
        player.Animator.SetTrigger("Restart");
    }
}
