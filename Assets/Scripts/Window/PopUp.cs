using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIPopUp : MonoBehaviour
{

    public TextMeshProUGUI text;
    public float fadeOutTime = 2f;
    public CanvasGroup canvasGroup;

    public void SetText(string message)
    {
        text.text = message;
        gameObject.SetActive(true);
        canvasGroup.DOKill();
        canvasGroup.DOFade(1f, 0.3f)
            .OnComplete(() =>
            {
                canvasGroup.DOFade(0f, 0.5f).SetDelay(fadeOutTime)
                    .OnComplete(() =>
                    {
                        ClosePanel();
                    });
            });
    }

    
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }


}
