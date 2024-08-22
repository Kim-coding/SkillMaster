using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EquipBookSaveData
{
    public int equipID { get; set; }
    public ClearState state { get; set; }

}


public class EquipBookElement : MonoBehaviour
{
    public EquipBookSaveData saveData = new EquipBookSaveData();

    public Image icon;
    public Image rarityColor;
    public Image reward;
    public Image blind;
    public TextMeshProUGUI rewardText;

    private int rewardValue = 0;

    public void Init()
    {
        var equipData = DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(saveData.equipID);
        if (equipData == null)
        {
            return;
        }

        icon.sprite = equipData.Geticon;
        if(equipData.equipmenttype == 4)
        {
            icon.sprite = equipData.GetTexture[0];
        }
        Color newColor = Color.white;
        switch (equipData.equipment_rating)
        {
            case "Basic":
                ColorUtility.TryParseHtmlString("#C4C4C4", out newColor);
                break;
            case "C":
                ColorUtility.TryParseHtmlString("#97846B", out newColor);
                break;
            case "B":
                ColorUtility.TryParseHtmlString("#6AAC8D", out newColor);
                break;
            case "A":
                ColorUtility.TryParseHtmlString("#A4BDFF", out newColor);
                break;
            case "S":
                ColorUtility.TryParseHtmlString("#C188D7", out newColor);
                break;
            case "SS":
                ColorUtility.TryParseHtmlString("#F4C56B", out newColor);
                break;
            case "SSS":
                ColorUtility.TryParseHtmlString("#C74B46", out newColor);
                break;
        }

        rarityColor.color = newColor;

        rewardValue = DataTableMgr.Get<EquipBookTable>(DataTableIds.equipBook).GetID(equipData.GetSetId).equipment_reward;
        reward.GetComponent<Button>().onClick.AddListener(GetReward);
        icon.GetComponent<Button>().onClick.AddListener(() => { GameMgr.Instance.uiMgr.uiWindow.equipBookPanel.SetEquipBookPanel(saveData.equipID); });
        blind.GetComponent<Button>().onClick.AddListener(() => { GameMgr.Instance.uiMgr.uiWindow.equipBookPanel.SetEquipBookPanel(saveData.equipID); });
        AcquiredCheck();
    }

    public void AcquiredCheck()
    {
        if (saveData.state == ClearState.NotAcquired)
        {
            reward.gameObject.SetActive(false);
            blind.gameObject.SetActive(true);
        }
        if (saveData.state == ClearState.Acquired)
        {
            reward.gameObject.SetActive(true);
            rewardText.text = rewardValue.ToString();
            blind.gameObject.SetActive(false);
        }
        if (saveData.state == ClearState.RewardAcquired)
        {
            reward.gameObject.SetActive(false);
            blind.gameObject.SetActive(false);
        }
    }


    private void GetReward()
    {
        GameMgr.Instance.playerMgr.currency.AddDia(new BigInteger(rewardValue));
        saveData.state = ClearState.RewardAcquired;
        AcquiredCheck();
    }
}
