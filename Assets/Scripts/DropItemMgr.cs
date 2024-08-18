using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemMgr : MonoBehaviour
{
    public RectTransform uiGoldTarget;
    public RectTransform uiDiaTarget;
    public GameObject goldPrefab;
    public GameObject diaPrefab;
    public Canvas uiCanvas;

    public void CreateAndMoveGold(Vector3 startPosition, float duration, string value)
    {
        GameObject goldObject = Instantiate(goldPrefab, uiCanvas.transform);
        RectTransform goldRectTransform = goldObject.GetComponent<RectTransform>();

        goldRectTransform.anchoredPosition = startPosition;

        DropItemMovement goldMovement = goldObject.GetComponent<DropItemMovement>();
        goldMovement.Initialize(uiGoldTarget, duration, value, true);
    }

    public void CreateAndMoveDia(Vector3 startPosition, float duration, string value)
    {
        GameObject goldObject = Instantiate(diaPrefab, uiCanvas.transform);
        RectTransform goldRectTransform = goldObject.GetComponent<RectTransform>();

        goldRectTransform.anchoredPosition = startPosition;

        DropItemMovement diaMovement = goldObject.GetComponent<DropItemMovement>();
        diaMovement.Initialize(uiDiaTarget, duration, value, false);
    }
}
