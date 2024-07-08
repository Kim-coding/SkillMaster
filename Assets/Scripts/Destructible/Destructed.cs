using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructed : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        gameObject.SetActive(false); // ���� ��Ȱ��ȭ �Ǵ� �ı�
        GameMgr.Instance.sceneMgr.mainScene.spawner.DestroyMonster(gameObject.GetComponent<MonsterAI>());
    }
}
