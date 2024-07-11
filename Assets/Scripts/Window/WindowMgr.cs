using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindowMgr : MonoBehaviour
{
    public GameObject mergeWindow;
    public GameObject enhanceWindow;
    public GameObject invenWindow;
    public GameObject dungeonWindow;
    public GameObject pickUpWindow;

    private Dictionary<Windows, GameObject> windows;

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
    }

    public void Open(Windows window)
    {
        if (windows[window].activeSelf)
            return;

        foreach (var win in windows)
        {
            win.Value.SetActive(false);
        }

        if (windows.ContainsKey(window))
        {
            //windows[window].SetActive(true);
            AnimateWindow(windows[window]);
        }
    }

    private void AnimateWindow(GameObject window)
    {
        RectTransform rectTransform = window.GetComponent<RectTransform>();
        window.SetActive(true);
        rectTransform.anchoredPosition = new Vector2(0, -Screen.height); // ���� ��ġ �Ʒ��� ����
        rectTransform.DOAnchorPos(Vector2.zero, 1f);//.SetEase(Ease.OutFlash); // �ݵ� ȿ���� �߾����� �̵�
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
