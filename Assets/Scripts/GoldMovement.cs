using UnityEngine;
using DG.Tweening;

public class GoldMovement : MonoBehaviour
{
    private BigInteger gold;
    private RectTransform uiTarget;
    private RectTransform rectTransform;
    private float moveDuration;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(RectTransform target, float duration, string value)
    {
        gold = new BigInteger(value);
        uiTarget = target;
        moveDuration = duration;

        transform.SetParent(uiTarget.parent);

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector2 targetPosition = uiTarget.localPosition;

        rectTransform.DOAnchorPos(targetPosition, moveDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            GameMgr.Instance.playerMgr.currency.AddGold(gold);
            Destroy(gameObject);
        });
    }
}
