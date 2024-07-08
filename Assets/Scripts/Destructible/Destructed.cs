using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructed : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        gameObject.SetActive(false); // 몬스터 비활성화 또는 파괴
        GameMgr.Instance.sceneMgr.mainScene.spawner.DestroyMonster(gameObject.GetComponent<MonsterAI>());
    }
}
