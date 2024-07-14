using UnityEngine;
using DG.Tweening;

public class GoldMovement : MonoBehaviour
{
    private RectTransform uiTarget;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(RectTransform target, float moveDuration)
    {
        uiTarget = target;

        rectTransform.DOMove(uiTarget.position, moveDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
