using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItemsPanel : MonoBehaviour
{

    public Button exitArea;
    public Button pickUpButton_1;
    public Button pickUpButton_10;
    public Button pickUpButton_30;

    public Image costImage_1;
    public Image costImage_10;
    public Image costImage_30;
    public TextMeshProUGUI costText_1;
    public TextMeshProUGUI costText_10;
    public TextMeshProUGUI costText_30;

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
            GameMgr.Instance.soundMgr.PlaySFX("GachaResult");
            var newSlot = Instantiate(prefabSlot, pickUpPanel.transform);
            newSlot.SetData(pickUpitemEquips[0]);
            pickUpitemEquips.RemoveAt(0);
            slotList.Add(newSlot);
            yield return new WaitForSecondsRealtime(duration); 
        }

        yield break;
    }

    public void TicketUpdate()
    {
        int ticketValue = 0;
        NormalItem ticket = null;
        foreach (var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
        {
            if (item.itemNumber == 220005)
            {
                ticket = item;
                ticketValue = ticket.itemValue;
                break;
            }
        }

        costImage_1.sprite = Resources.Load<Sprite>("EnhanceIcon/Icon_Gem03_Diamond_Blue");
        costImage_10.sprite = Resources.Load<Sprite>("EnhanceIcon/Icon_Gem03_Diamond_Blue");
        costImage_30.sprite = Resources.Load<Sprite>("EnhanceIcon/Icon_Gem03_Diamond_Blue");
        costText_1.text = "100개";
        costText_10.text = "1000개";
        costText_30.text = "3000개";
        if (ticketValue >= 1)
        {
            costImage_1.sprite = Resources.Load<Sprite>("ticket");
            costText_1.text = "1개";
        }
        if (ticketValue >= 10)
        {
            costImage_10.sprite = Resources.Load<Sprite>("ticket");
            costText_10.text = "10개";
        }
        if (ticketValue >= 30)
        {
            costImage_30.sprite = Resources.Load<Sprite>("ticket");
            costText_30.text = "30개";
        }
    }


    public void ClosePanel()
    {
        StopCoroutine(resultCorutine);
        ResultListClear();
        gameObject.SetActive(false);
    }
}
