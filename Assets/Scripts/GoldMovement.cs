using UnityEngine;
using DG.Tweening;

public class GoldMovement : MonoBehaviour
{
    private Camera mainCamera;
    private RectTransform uiTarget;
    private float duration;
    private Vector3 startScreenPosition;

    private GoldManager goldManager;

    void Start()
    {
        goldManager = GameMgr.Instance.goldManager;
    }

    public void Initialize(Camera camera, RectTransform target, float moveDuration)
    {
        mainCamera = camera;
        uiTarget = target;
        duration = moveDuration;

        startScreenPosition = mainCamera.WorldToScreenPoint(transform.position);
        if(goldManager ==  null)
        {
            Debug.Log("goldManager NULL");
            return;
        }
        goldManager.StartMovingGold(this, duration);
    }

    public void StartMoving(float duration)
    {
        Vector3 targetScreenPosition = uiTarget.position;

        Vector3 targetWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(targetScreenPosition.x, targetScreenPosition.y, startScreenPosition.z));

        transform.DOMove(targetWorldPosition, duration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            goldManager.StopMovingGold(this); 
            Destroy(gameObject);
        });
    }

    public void UpdateTargetPosition()
    {
        Vector3 targetScreenPosition = uiTarget.position;

        Vector3 targetWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(targetScreenPosition.x, targetScreenPosition.y, startScreenPosition.z));

        transform.position = targetWorldPosition;
    }
}
