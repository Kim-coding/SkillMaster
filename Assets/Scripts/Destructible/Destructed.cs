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
        gameObject.SetActive(false); // 몬스터 비활성화 또는 파괴

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

        // UI 캔버스 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameMgr.Instance.goldManager.uiCanvas.transform as RectTransform, screenPosition, null, out Vector2 uiPosition);

        // 골드 생성 및 이동 호출
        GameMgr.Instance.goldManager.CreateAndMoveGold(uiPosition, moveDuration);    
    }
}
