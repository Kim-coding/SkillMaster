using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Destructed : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        GameMgr.Instance.sceneMgr.mainScene.RemoveMonsters(gameObject);
        gameObject.SetActive(false); // 몬스터 비활성화 또는 파괴
                                     
        //GameMgr.Instance.sceneMgr.mainScene.spawner.DestroyMonster(gameObject.GetComponent<MonsterAI>());
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
