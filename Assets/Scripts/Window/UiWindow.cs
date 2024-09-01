using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;
public class UiWindow : MonoBehaviour
{
    public GameObject mergeWindow;
    public GameObject enhanceWindow;
    public GameObject invenWindow;
    public GameObject dungeonWindow;
    public GameObject pickUpWindow;
    public GameObject bookWindow;
    public CameraMove CameraMove;
    public GameObject option;

    public Button toggleWindowButton;

    public Image[] uiButtonCloseImages;
    public Image uiPickupLockImage;
    public Image uiDungeonLockImage;

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
    public AutoDecomposPanel autoDecomposSelectPanel;
    public EquipUpgradePanel equipUpgradePanel;
    public PickUpItemsPanel pickUpResultPanel;
    public SkillBookPanel skillBookPanel;
    public EquipBookPanel equipBookPanel;
    private bool hasShownPickupMessage = false;
    public bool hasShownDungeonMessage;

    private UIMgr uIMgr;
    private void Awake()
    {
        windows = new Dictionary<Windows, GameObject>()
        {
            {Windows.Enhance, enhanceWindow},
            {Windows.Inven, invenWindow },
            {Windows.Dungeon, dungeonWindow },
            {Windows.PickUp, pickUpWindow },
            {Windows.Book, bookWindow },
        };



        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }

        buttonPosition = toggleWindowButton.GetComponent<RectTransform>().anchoredPosition;

        toggleWindowButton.onClick.AddListener(OnWindowButtonClick);

        uIMgr = GameMgr.Instance.uiMgr;

        if (GameMgr.Instance.sceneMgr.mainScene != null)
        {
            if (uIMgr.uiGuideQuest.currentQuest.QuestID < 60023)
            {
                uiPickupLockImage.gameObject.SetActive(true);
                uiPickupLockImage.transform.parent.GetComponent<Button>().interactable = false;
            }
            if (uIMgr.uiGuideQuest.currentQuest.QuestID < 60050)
            {
                uiDungeonLockImage.gameObject.SetActive(true);
                uiDungeonLockImage.transform.parent.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void UnLock()
    {
        if (GameMgr.Instance.uiMgr.uiGuideQuest.currentQuest.QuestID == 60023)
        {
            if(!hasShownPickupMessage)
            {
                GameMgr.Instance.uiMgr.UnlistedListPanel.SetActive(true);
                GameMgr.Instance.uiMgr.UnlistedList.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(120556);
                hasShownPickupMessage = true;
            }
            
            uiPickupLockImage.gameObject.SetActive(false);
            uiPickupLockImage.transform.parent.GetComponent<Button>().interactable = true;
        }
        if (GameMgr.Instance.uiMgr.uiGuideQuest.currentQuest.QuestID == 60050)
        {
            if (!hasShownDungeonMessage)
            {
                GameMgr.Instance.uiMgr.UnlistedListPanel.SetActive(true);
                GameMgr.Instance.uiMgr.UnlistedList.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(120557);
                hasShownDungeonMessage = true;
            }
            uiDungeonLockImage.gameObject.SetActive(false);
            uiDungeonLockImage.transform.parent.GetComponent<Button>().interactable = true;
        }
    }

    public void OnGameWindowClick()
    {
        //if(!OnMergeWindow)
        //{
        //    return;
        //}

        //AnimateCloseMergeWindow();
        //CameraMove.isToggle = false;
        //CameraMove.CurrentCameraView();
        if(currentOpenWindow != Windows.Enhance)
        {
            return;
        }
        Open(Windows.Enhance);
    }

    private void OnWindowButtonClick()
    {
        GameMgr.Instance.soundMgr.PlaySFX("Button");
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
        rectTransform.DOAnchorPos(Vector2.zero, 0.3f).SetUpdate(true).OnComplete(() =>
        {
            previousWindow = currentOpenWindow;
            currentOpenWindow = window;
            isAnimating = false;
        });
        if (uIMgr.uiGuideQuest.currentQuest.QuestID == 60023 && window == Windows.PickUp)
        {
            SaveLoadSystem.Save();
            GameMgr.Instance.sceneMgr.tutorial.OnTutorial();
        }
        if (uIMgr.uiGuideQuest.currentQuest.QuestID == 60050 && window == Windows.Dungeon && !uIMgr.uiTutorial.isDungeonOpen)
        {
            SaveLoadSystem.Save();
            GameMgr.Instance.sceneMgr.tutorial.OnTutorial();
            uIMgr.uiTutorial.isDungeonOpen = true;
        }
    }


    private void AnimateCloseWindow(GameObject window)
    {
        isAnimating = true;
        currentOpenWindow = null;
        groundUISort(false);
        RectTransform rectTransform = window.GetComponent<RectTransform>();
        var targetPos = new Vector2(0, -Screen.height);
        rectTransform.DOAnchorPos(targetPos, 0.3f).SetUpdate(true).OnComplete(() =>
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

    public void BookWindowOpen()
    {
        Open(Windows.Book);
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

    public void OnOption()
    {
        GameMgr.Instance.soundMgr.PlaySFX("Button");
        option.SetActive(true);
    }
}
