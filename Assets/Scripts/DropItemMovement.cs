using UnityEngine;
using DG.Tweening;

public class DropItemMovement : MonoBehaviour
{
    private BigInteger dropItem;
    private bool isGold;
    private RectTransform uiTarget;
    private RectTransform rectTransform;
    private float moveDuration;
    private UiWindow uiWindow;
    public Vector2 targetPosition;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(RectTransform target, float duration, string value, bool isGold)
    {
        uiWindow = GameMgr.Instance.uiMgr.uiWindow;
        dropItem = new BigInteger(value);
        this.isGold = isGold;
        if (this.isGold)
        {
            dropItem += (int)GameMgr.Instance.playerMgr.playerinventory.itemGoldIncrease;
            float goldIncrease = 1 + GameMgr.Instance.playerMgr.playerEnhance.goldValue * GameMgr.Instance.playerMgr.playerEnhance.goldLevel;
            dropItem *= goldIncrease;
        }
        uiTarget = target;
        moveDuration = duration;
        transform.SetParent(uiTarget.parent);

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        targetPosition = uiTarget.localPosition;

        rectTransform.DOAnchorPos(targetPosition, moveDuration)
            .SetEase(Ease.InOutQuad)
            .OnUpdate(() =>
            {
                if (uiWindow.invenWindow.activeSelf || 
                    uiWindow.dungeonWindow.activeSelf || 
                    uiWindow.pickUpWindow.activeSelf)
                {
                    gameObject.SetActive(false);
                }
            })
            .OnComplete(() =>
            {
                if(isGold)
                {
                    GameMgr.Instance.playerMgr.currency.AddGold(dropItem);
                }
                else
                {
                    GameMgr.Instance.playerMgr.currency.AddDia(dropItem);
                }
                Destroy(gameObject);
            });
    }
}
