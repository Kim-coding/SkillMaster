using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItemsPanel : MonoBehaviour
{

    public Button exitArea;
    public Button pickUpButton_1;
    public Button pickUpButton_10;
    public Button pickUpButton_30;

    private List<Equip> pickUpitemEquips = new List<Equip>();
    private List<PickUpSlot> slotList = new List<PickUpSlot>();
    public PickUpSlot prefabSlot;
    public GameObject pickUpPanel;

    private float duration = 0.1f;
    private Coroutine resultCorutine;

    private void Awake()
    {
        exitArea.onClick.AddListener(ClosePanel);
        var pickUpWindow = GameMgr.Instance.uiMgr.uiWindow.pickUpWindow.GetComponent<PickUp>();
        pickUpButton_1.onClick.AddListener(() => { pickUpWindow.PickUpItem(1); });
        pickUpButton_10.onClick.AddListener(() => { pickUpWindow.PickUpItem(10); });
        pickUpButton_30.onClick.AddListener(() => { pickUpWindow.PickUpItem(30); });

    }

    public void ResultListClear()
    {
        pickUpitemEquips.Clear();
        while(slotList.Count > 0)
        {
            Destroy(slotList[0].gameObject);
            slotList.RemoveAt(0);
        }
    }

    public void AddResult(Equip equip)
    {
        pickUpitemEquips.Add(equip);
    }

    public void ShowResult()
    {
        resultCorutine = StartCoroutine(CoResult());
    }

    public IEnumerator CoResult()
    {
        while(pickUpitemEquips.Count > 0)
        {
            var newSlot = Instantiate(prefabSlot, pickUpPanel.transform);
            newSlot.SetData(pickUpitemEquips[0]);
            pickUpitemEquips.RemoveAt(0);
            slotList.Add(newSlot);
            yield return new WaitForSecondsRealtime(duration); 
        }

        yield break;
    }

    public void ClosePanel()
    {
        StopCoroutine(resultCorutine);
        ResultListClear();
        gameObject.SetActive(false);
    }
}
