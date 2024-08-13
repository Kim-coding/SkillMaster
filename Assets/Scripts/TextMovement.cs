using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextMovement : MonoBehaviour
{
    public GameObject startPanel;
    private float speed = 300f;
    private float duration = 0.25f;

    private RectTransform rectTransform;
    private float screenWidth;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        screenWidth = Screen.width;

        Time.timeScale = 0f;

        var sequence = DOTween.Sequence();

        float movespeed = screenWidth / speed;

        //SetUpdate : DOTween을 TimeScale의 영향을 받지 않도록 설정
        sequence.Append(rectTransform.DOAnchorPosX(0, movespeed).SetEase(Ease.Linear)).SetUpdate(true); 

        sequence.AppendInterval(duration);

        sequence.Append(rectTransform.DOAnchorPosX(screenWidth + rectTransform.rect.width, movespeed).SetEase(Ease.Linear).SetUpdate(true)).OnComplete(End);
    }

    private void End()
    {
        Time.timeScale = 1f;
        startPanel.SetActive(false);
    }
}
