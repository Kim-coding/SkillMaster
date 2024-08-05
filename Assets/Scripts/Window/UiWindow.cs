using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UiWindow : MonoBehaviour
{
    public GameObject mergeWindow;
    public GameObject enhanceWindow;
    public GameObject invenWindow;
    public GameObject dungeonWindow;
    public GameObject pickUpWindow;
    public CameraMove CameraMove;

    public Button toggleWindowButton;

    public Image[] uiButtonCloseImages;

    private Dictionary<Windows, GameObject> windows;
    private Windows? currentOpenWindow = null;
    public Windows? CurrentOpenWindow {  get { return currentOpenWindow; } }
    private Windows? previousWindow = null;
    private bool isAnimating = false;

    private bool OnMergeWindow = true;

    private Vector2 buttonPosition;

    public GameObject UIGuideQuest;
    public GameObject UIMonsterSlider;
    public GameObject UICameraButton;

    public ItemInfoPanel pickUpItemPanel;
    public EquipmentInfoPanel equipmentItemPanel;
    public ItemInfoPanel currentEquipmentPanel;
    public ItemInfoPanel normalItemPanel;
    public UIPopUp popUpUI;
    public DecomposPanel decomposPanel;
    private void Start()
    {
        windows = new Dictionary<Windows, GameObject>()
        {
            {Windows.Enhance, enhanceWindow},
            {Windows.Inven, invenWindow },
            {Windows.Dungeon, dungeonWindow },
            {Windows.PickUp, pickUpWindow },
        };



        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }

        buttonPosition = toggleWindowButton.GetComponent<RectTransform>().anchoredPosition;

        toggleWindowButton.onClick.AddListener(OnWindowButtonClick);
    }


    public void OnGameWindowClick()
    {
        if(!OnMergeWindow)
        {
            return;
        }

        AnimateCloseMergeWindow();
        CameraMove.isToggle = false;
        CameraMove.CurrentCameraView();
    }

    private void OnWindowButtonClick()
    {
        if (OnMergeWindow)
        {
            AnimateCloseMergeWindow();
            CameraMove.isToggle = false;
            CameraMove.CurrentCameraView();
        }
        else
        {
            AnimateOpenMergeWindow();
            CameraMove.isToggle = true;
            CameraMove.CurrentCameraView();
        }
    }

    public void Open(Windows window)
    {
        if(isAnimating)
        {
            return;
        }

        invenWindow.GetComponent<UiInventory>().OffDecomposMode();

        GameMgr.Instance.soundMgr.PlaySFX("Button");
        foreach(var closeimage in uiButtonCloseImages)
        {
            closeimage.gameObject.SetActive(false);
        }

        if (currentOpenWindow == window && windows[window].activeSelf)
        {
            AnimateCloseWindow(windows[window]);
            return;
        }

        groundUISort(true);

        if(window == Windows.Enhance)
        {
            UIGuideQuest.transform.SetAsLastSibling();
        }

        uiButtonCloseImages[(int)window].gameObject.SetActive(true);

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
        isAnimating = true;
        GameObject windowObj = windows[window];
        RectTransform rectTransform = windowObj.GetComponent<RectTransform>();
        windowObj.SetActive(true);
        rectTransform.anchoredPosition = new Vector2(0, -Screen.height);
        rectTransform.DOAnchorPos(Vector2.zero, 0.3f).OnComplete(() =>
        {
            previousWindow = currentOpenWindow;
            currentOpenWindow = window;
            isAnimating = false;
        });
    }


    private void AnimateCloseWindow(GameObject window)
    {
        isAnimating = true;
        currentOpenWindow = null;
        groundUISort(false);
        RectTransform rectTransform = window.GetComponent<RectTransform>();
        var targetPos = new Vector2(0, -Screen.height);
        rectTransform.DOAnchorPos(targetPos, 0.3f).OnComplete(() =>
        {
            window.SetActive(false);
            isAnimating = false;
        });
    }



    public void AnimateOpenMergeWindow()
    {
        if(OnMergeWindow)
        {
            return;
        }
        OnMergeWindow = true;
        CameraMove.isToggle = true;
        CameraMove.CurrentCameraView();
        RectTransform rectTransform = mergeWindow.GetComponent<RectTransform>();
        RectTransform buttonRectTransform = toggleWindowButton.GetComponent<RectTransform>();
        mergeWindow.SetActive(true);
        rectTransform.anchoredPosition = new Vector2(0, -rectTransform.sizeDelta.y);
        var buttonPos = new Vector2(buttonPosition.x, buttonRectTransform.rect.height);
        isAnimating = true;
        rectTransform.DOAnchorPos(Vector2.zero, 0.3f).OnComplete(() =>
        {
            isAnimating = false;
        });
        //  buttonRectTransform.DOAnchorPos(buttonPos, 0.3f);
        buttonRectTransform.DOScaleY(1, 0.3f);
    }


    public void AnimateCloseMergeWindow()
    {
        OnMergeWindow = false;
        CameraMove.isToggle = false;
        CameraMove.CurrentCameraView();
        RectTransform rectTransform = mergeWindow.GetComponent<RectTransform>();
        RectTransform buttonRectTransform = toggleWindowButton.GetComponent<RectTransform>();
        var targetPos = new Vector2(0, -rectTransform.sizeDelta.y);
        var buttonPos = new Vector2(buttonPosition.x, -rectTransform.rect.height);
        rectTransform.DOAnchorPos(targetPos, 0.3f).OnComplete(() =>
        {
            // mergeWindow.SetActive(false);
            isAnimating = false;
        });
        buttonRectTransform.localScale = new Vector3(1, -1, 1);
        // buttonRectTransform.DOAnchorPos(buttonPos, 0.15f);
    }



    public void AnimateCloseCurrentWindow()
    {
        if (currentOpenWindow != null)
        {
            AnimateCloseWindow(windows[(Windows)currentOpenWindow]);
            previousWindow = currentOpenWindow;
            currentOpenWindow = null;
        }
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

    public void groundUISort(bool onUIopen)
    {
        if (onUIopen)
        {
            UIGuideQuest.transform.SetAsFirstSibling();
            UIMonsterSlider.transform.SetAsFirstSibling();
            UICameraButton.transform.SetAsFirstSibling();
        }
        else
        {
            UIGuideQuest.transform.SetAsLastSibling();
            UIMonsterSlider.transform.SetAsLastSibling();
            UICameraButton.transform.SetAsLastSibling();

        }
    }
}
