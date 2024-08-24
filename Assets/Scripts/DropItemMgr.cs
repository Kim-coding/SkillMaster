using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemMgr : MonoBehaviour
{
    public RectTransform uiGoldTarget;
    public RectTransform uiDiaTarget;
    public DropItemMovement goldPrefab;
    public DropItemMovement diaPrefab;
    public Transform parent;

    private static DropItemPool goldItemPool;
    private static DropItemPool diaItemPool;

    private void Start()
    {
        if (goldPrefab == null)
        {
            goldPrefab = Resources.Load<DropItemMovement>("Gold");
        }

        if (diaPrefab == null)
        {
            diaPrefab = Resources.Load<DropItemMovement>("Dia");
        }

        if (GameMgr.Instance.sceneMgr.mainScene != null)
        {
            goldItemPool = new DropItemPool(goldPrefab, parent);
            diaItemPool = new DropItemPool(diaPrefab, parent,1);
        }
    }

    public void CreateAndMoveGold(Vector3 startPosition, float duration, string value)
    {
        var gold = goldItemPool.GetItem();
        RectTransform goldRectTransform = gold.GetComponent<RectTransform>();

        goldRectTransform.anchoredPosition = startPosition;

        DropItemMovement goldMovement = gold.GetComponent<DropItemMovement>();
        goldMovement.Init(goldItemPool, uiGoldTarget, duration, value, true);
    }

    public void CreateAndMoveDia(Vector3 startPosition, float duration, string value)
    {
        var dia = diaItemPool.GetItem();
        RectTransform goldRectTransform = dia.GetComponent<RectTransform>();

        goldRectTransform.anchoredPosition = startPosition;

        DropItemMovement diaMovement = dia.GetComponent<DropItemMovement>();
        diaMovement.Init(diaItemPool, uiDiaTarget, duration, value, false);
    }
}
