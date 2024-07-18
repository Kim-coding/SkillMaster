using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCurrency : MonoBehaviour, IDestructible
{
    Vector3 deathPosition;
    float moveDuration = 1.0f;
    public void OnDestruction(GameObject attacker)
    {
        deathPosition = gameObject.transform.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(deathPosition);
        // UI 캔버스 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameMgr.Instance.goldManager.uiCanvas.transform as RectTransform, screenPosition, null, out Vector2 uiPosition);
        // 골드 생성 및 이동 호출
        GameMgr.Instance.goldManager.CreateAndMoveGold(uiPosition, moveDuration);
    }
}
