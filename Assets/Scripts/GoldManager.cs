using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public GameObject goldPrefab;
    public Camera mainCamera;
    public RectTransform uiTarget;
    public float updateInterval = 0.3f;

    private List<GoldMovement> goldMovements = new List<GoldMovement>();

    void Start()
    {
        StartCoroutine(UpdateGoldPositions());
    }

    public void StartMovingGold(GoldMovement goldMovement, float duration)
    {
        goldMovements.Add(goldMovement);
        goldMovement.StartMoving(duration);
    }

    public void StopMovingGold(GoldMovement goldMovement)
    {
        goldMovements.Remove(goldMovement);
    }

    private IEnumerator UpdateGoldPositions()
    {
        while (true)
        {
            foreach (var goldMovement in goldMovements)
            {
                goldMovement.UpdateTargetPosition();
            }

            yield return new WaitForSeconds(updateInterval);
        }
    }
}
