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
        GameMgr.Instance.sceneMgr.mainScene.spawner.DestroyMonster(gameObject.GetComponent<MonsterAI>());
        GameMgr.Instance.uiMgr.MonsterSliderUpdate();

    }
}
