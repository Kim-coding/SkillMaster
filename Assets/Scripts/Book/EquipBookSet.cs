using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EquipBookSet : MonoBehaviour
{
    public EquipBookElement elementPrefab;

    public EquipBookElement element1;
    public EquipBookElement element2;
    public EquipBookElement element3;
    public EquipBookElement element4;
    public EquipBookElement element5;

    public GameObject equipPanel;

    public TextMeshProUGUI setNameText;
    public TextMeshProUGUI setOptionText;

    public Button rewardButton;
    public Image rewardButtonImage;
    public TextMeshProUGUI rewardButtonText;

    public int setOptionType;
    public float setOptionValue;

    public int setID;
    public bool setClear = false;
    public bool getReward = false;

    private void Awake()
    {
        rewardButton.onClick.AddListener(GetSetOption);
    }
    public void Init(int setID)
    {
        this.setID = setID;
        var setData = DataTableMgr.Get<EquipBookTable>(DataTableIds.equipBook).GetID(setID);
        if( setData == null )
        {
            return;
        }
        var dic = GameMgr.Instance.uiMgr.uiBook.equipBookDic;

        element1 = Instantiate(elementPrefab, equipPanel.transform);
        element1.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment1_id];
        element1.Init();
        dic.Add(setData.equipment1_id, element1);

        element2 = Instantiate(elementPrefab, equipPanel.transform);
        element2.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment2_id];
        element2.Init();
        dic.Add(setData.equipment2_id, element2);

        element3 = Instantiate(elementPrefab, equipPanel.transform);
        element3.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment3_id];
        element3.Init();
        dic.Add(setData.equipment3_id, element3);

        element4 = Instantiate(elementPrefab, equipPanel.transform);
        element4.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment4_id];
        element4.Init();
        dic.Add(setData.equipment4_id, element4);

        element5 = Instantiate(elementPrefab, equipPanel.transform);
        element5.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment5_id];
        element5.Init();
        dic.Add(setData.equipment5_id, element5);

        string rarity = string.Empty;
        switch (setData.equipment_order)
        {
            case 1:
                rarity = "CµÓ±ﬁ ";
                break;
            case 2:
                rarity = "BµÓ±ﬁ ";
                break;
            case 3:
                rarity = "AµÓ±ﬁ ";
                break;
            case 4:
                rarity = "SµÓ±ﬁ ";
                break;
            case 5:
                rarity = "SSµÓ±ﬁ ";
                break;
            case 6:
                rarity = "SSSµÓ±ﬁ ";
                break;
        }

        string part = string.Empty;
        switch (setData.equipment_part)
        {
            case 1:
                part = "∏”∏Æ";
                break;
            case 2:
                part = "æÛ±º";
                break;
            case 3:
                part = "ªÛ¿«";
                break;
            case 4:
                part = "«œ¿«";
                break;
            case 5:
                part = "π´±‚";
                break;
            case 6:
                part = "∏¡≈‰";
                break;
        }

        setNameText.text = rarity + part;
        setOptionType = setData.synergy_type;
        setOptionValue = setData.synergy_value;
    }

    public void EquipSetCheck()
    {
        if(getReward)
        {
            GetSetOption();
            return;
        }
        if(setClear)
        {
            return;
        }

        if(element1.saveData.state != ClearState.NotAcquired &&
           element2.saveData.state != ClearState.NotAcquired &&
           element3.saveData.state != ClearState.NotAcquired &&
           element4.saveData.state != ClearState.NotAcquired &&
           element5.saveData.state != ClearState.NotAcquired)
        {
            setClear = true;
            rewardButton.interactable = true;
            rewardButtonText.text = "»πµÊ\n∞°¥…";
            rewardButtonText.color = Color.black;
            rewardButtonImage.color = Color.yellow;
        }

        else
        {
            rewardButton.interactable = false;
            rewardButtonText.text = "πÃ»πµÊ";
        }
    }

    private void GetSetOption()
    {
        getReward = true;
        GameMgr.Instance.playerMgr.playerInfo.SetDatas[setID] = getReward;
        rewardButton.interactable = false;
        rewardButtonText.text = "»πµÊ\nøœ∑·";
        rewardButtonText.color = Color.white;
        rewardButtonImage.color = Color.yellow;
    }
}
