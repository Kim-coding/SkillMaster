using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Destructed : MonoBehaviour, IDestructible
{

    public void OnDestruction(GameObject attacker)
    {
        GameMgr.Instance.sceneMgr.mainScene.RemoveMonsters(gameObject);
        gameObject.SetActive(false); // ���� ��Ȱ��ȭ �Ǵ� �ı�

        EventMgr.TriggerEvent(QuestType.MonsterKill);

        var monsterAI = gameObject.GetComponent<MonsterAI>();
        if (monsterAI != null)
        {
            GameMgr.Instance.sceneMgr.mainScene.spawner.DestroyMonster(monsterAI);
        }
        else
        {
            Debug.LogError("MonsterAI component is missing on destroyed object.");
        }
        GameMgr.Instance.uiMgr.MonsterSliderUpdate(); 
    }
}
