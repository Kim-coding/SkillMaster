using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Destructed : MonoBehaviour, IDestructible
{

    public void OnDestruction(GameObject attacker)
    {
        Vector3 deathPosition = transform.position;
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
        
        float moveDuration = 1.0f;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(deathPosition);

        // UI ĵ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameMgr.Instance.goldManager.uiCanvas.transform as RectTransform, screenPosition, null, out Vector2 uiPosition);

        // ��� ���� �� �̵� ȣ��
        GameMgr.Instance.goldManager.CreateAndMoveGold(uiPosition, moveDuration);    
    }
}
