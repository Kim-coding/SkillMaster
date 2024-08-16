using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPopup : MonoBehaviour
{

    float timer = 0f;
    public float duration = 0.5f;
    public PlayerAI player;
    private void OnEnable()
    {
        timer = 0f;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > duration)
        {
            var scene = GameMgr.Instance.sceneMgr;
            if (scene.mainScene != null)
            {
                scene.mainScene.RestartStage();
                player.Restart();
                player.Animator.SetTrigger("Restart");
                gameObject.SetActive(false);
            }
            if(scene.dungeonScene != null)
            {
                scene.dungeonScene.EndDungeon(false, scene.dungeonScene.currentStage);
            }
            
        }
    }
}
