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
        // UI ĵ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameMgr.Instance.goldManager.uiCanvas.transform as RectTransform, screenPosition, null, out Vector2 uiPosition);
        // ��� ���� �� �̵� ȣ��
        GameMgr.Instance.goldManager.CreateAndMoveGold(uiPosition, moveDuration);
    }
}
