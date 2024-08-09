using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMgr : MonoBehaviour
{
    public RectTransform uiTarget;
    public GameObject goldPrefab;
    public Canvas uiCanvas;

    public void CreateAndMoveGold(Vector3 startPosition, float duration, string value)
    {
        GameObject goldObject = Instantiate(goldPrefab, uiCanvas.transform);
        RectTransform goldRectTransform = goldObject.GetComponent<RectTransform>();

        goldRectTransform.anchoredPosition = startPosition;

        DropItemMovement goldMovement = goldObject.GetComponent<DropItemMovement>();
        goldMovement.Initialize(uiTarget, duration, value);
    }
}
