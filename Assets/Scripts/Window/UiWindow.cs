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
    public CameraMove CameraMove;

    public Button toggleWindowButton;

    private Dictionary<Windows, GameObject> windows;
    private Windows? currentOpenWindow = null;
    private Windows? previousWindow = null;
    private bool isAnimating = false;

    private Vector2 buttonPosition;

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

        buttonPosition = toggleWindowButton.GetComponent<RectTransform>().anchoredPosition;

        toggleWindowButton.onClick.AddListener(OnWindowButtonClick);
    }

    private void OnWindowButtonClick()
    {
        if(currentOpenWindow != null)
        {
            AnimateCloseCurrentWindow();
            CameraMove.isToggle = false;
            CameraMove.CurrentCameraView();
        }
        else if(previousWindow != null)
        {
            AnimateOpenWindow((Windows)previousWindow);
            CameraMove.isToggle = true;
            CameraMove.CurrentCameraView();
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
            CameraMove.isToggle = true;
            CameraMove.CurrentCameraView();
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
        CameraMove.isToggle = true;
        CameraMove.CurrentCameraView();
        GameObject windowObj = windows[window];
        RectTransform rectTransform = windowObj.GetComponent<RectTransform>();
        RectTransform buttonRectTransform = toggleWindowButton.GetComponent<RectTransform>();
        windowObj.SetActive(true);
        rectTransform.anchoredPosition = new Vector2(0, -Screen.height);
        var buttonPos = new Vector2(buttonPosition.x, buttonRectTransform.rect.height);
        isAnimating = true;
        rectTransform.DOAnchorPos(Vector2.zero, 0.3f).OnComplete(() =>
        {
            previousWindow = currentOpenWindow;
            currentOpenWindow = window;
            isAnimating = false;
        });
        buttonRectTransform.DOAnchorPos(buttonPos, 0.3f);
        buttonRectTransform.DOScaleY(1, 0.3f);
    }
    private void AnimateCloseWindow(GameObject window)
    {
        CameraMove.isToggle = false;
        CameraMove.CurrentCameraView();
        RectTransform rectTransform = window.GetComponent<RectTransform>();
        RectTransform buttonRectTransform = toggleWindowButton.GetComponent<RectTransform>();
        var targetPos = new Vector2(0, -Screen.height);
        var buttonPos = new Vector2(buttonPosition.x, -rectTransform.rect.height);
        rectTransform.DOAnchorPos(targetPos, 0.3f).OnComplete(() =>
        {
            window.SetActive(false);
            isAnimating = false;
        });
        buttonRectTransform.localScale = new Vector3(1, -1, 1);
        buttonRectTransform.DOAnchorPos(buttonPos, 0.15f);
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
