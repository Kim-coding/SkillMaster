using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor.PackageManager.UI;

public class UiWindow : MonoBehaviour
{
    public GameObject mergeWindow;
    public GameObject enhanceWindow;
    public GameObject invenWindow;
    public GameObject dungeonWindow;
    public GameObject pickUpWindow;

    public Button WindowButton;

    private Dictionary<Windows, GameObject> windows;
    private Windows? currentOpenWindow = null;
    private Windows? previousWindow = null;
    private bool isAnimating = false;

    private void Start()
    {
        windows = new Dictionary<Windows, GameObject>()
        {
            {Windows.Merge, mergeWindow },
            {Windows.Enhance, enhanceWindow},
            {Windows.Inven, invenWindow },
            {Windows.Dungeon, dungeonWindow },
            {Windows.PickUp, pickUpWindow },
        };

        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }
        mergeWindow.SetActive(true);
        currentOpenWindow = Windows.Merge;

        WindowButton.onClick.AddListener(OnWindowButtonClick);
    }

    private void OnWindowButtonClick()
    {
        if(currentOpenWindow != null)
        {
            AnimateCloseCurrentWindow();
        }
        else if(previousWindow != null)
        {
            AnimateOpenWindow((Windows)previousWindow);
        }
    }

    public void Open(Windows window)
    {
        if(isAnimating || (currentOpenWindow == window && windows[window].activeSelf))
            return;

        if (currentOpenWindow != null)
        {
            foreach (var win in windows)
            {
                win.Value.SetActive(false);
            }
            windows[window].SetActive(true);
            currentOpenWindow = window;
        }
        else
        {
            ResetWindowPositions();
            AnimateOpenWindow(window);
        }
    }

    private void ResetWindowPositions()
    {
        foreach (var win in windows)
        {
            RectTransform rectTransform = win.Value.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            win.Value.SetActive(false);
        }
    }

    private void AnimateOpenWindow(Windows window)
    {
        GameObject windowObj = windows[window];
        RectTransform rectTransform = windowObj.GetComponent<RectTransform>();
        windowObj.SetActive(true);
        rectTransform.anchoredPosition = new Vector2(0, -Screen.height); 
        isAnimating = true;
        rectTransform.DOAnchorPos(Vector2.zero, 0.3f).OnComplete(() =>
        {
            previousWindow = currentOpenWindow;
            currentOpenWindow = window;
            isAnimating = false;
        });
    }
    private void AnimateCloseWindow(GameObject window, TweenCallback onComplete = null)
    {
        RectTransform rectTransform = window.GetComponent<RectTransform>();
        var targetPos = new Vector2(0, -Screen.height);
        isAnimating = true;
        rectTransform.DOAnchorPos(targetPos, 0.3f).OnComplete(() =>
        {
            window.SetActive(false);
            isAnimating = false;
            onComplete?.Invoke();
        });
    }

    private void AnimateCloseCurrentWindow()
    {
        if (currentOpenWindow != null)
        {
            AnimateCloseWindow(windows[(Windows)currentOpenWindow]);
            previousWindow = currentOpenWindow;
            currentOpenWindow = null;
        }
    }

    public void MergeWindowOpen()
    {
        Open(Windows.Merge);
    }
    public void EnhanceWindowOpen()
    {
        Open(Windows.Enhance);
    }
    public void InvenWindowOpen()
    {
        Open(Windows.Inven);
    }
    public void DungeonWindowOpen()
    {
        Open(Windows.Dungeon);
    }
    public void PickUpWindowOpen()
    {
        Open(Windows.PickUp);
    }
}
