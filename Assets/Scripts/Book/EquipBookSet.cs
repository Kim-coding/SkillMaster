using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
                rarity = "C등급 ";
                break;
            case 2:
                rarity = "B등급 ";
                break;
            case 3:
                rarity = "A등급 ";
                break;
            case 4:
                rarity = "S등급 ";
                break;
            case 5:
                rarity = "SS등급 ";
                break;
            case 6:
                rarity = "SSS등급 ";
                break;
        }

        string part = string.Empty;
        switch (setData.equipment_part)
        {
            case 1:
                part = "머리";
                break;
            case 2:
                part = "얼굴";
                break;
            case 3:
                part = "상의";
                break;
            case 4:
                part = "하의";
                break;
            case 5:
                part = "무기";
                break;
            case 6:
                part = "망토";
                break;
        }

        setNameText.text = rarity + part;
        setOptionType = setData.synergy_type;
        setOptionValue = setData.synergy_value;

        string option = string.Empty;
        switch (setOptionType)
        {
            case 1:
                option = "공격력 ";
                break;
            case 2:
                option = "치명타 확률 ";
                break;
            case 3:
                option = "최대 체력 ";
                break;
            case 4:
                option = "치명타 데미지 ";
                break;
            case 5:
                option = "방어력 ";
                break;
            case 6:
                option = "체력 회복 ";
                break;
        }

        setOptionText.text = option + setOptionValue + "%";
    }

    public void EquipSetCheck()
    {
        Color customColor = Color.white;
        if (getReward)
        {
            GetSetOption();
            return;
        }
        if (setClear)
        {
            return;
        }

        if (element1.saveData.state != ClearState.NotAcquired &&
           element2.saveData.state != ClearState.NotAcquired &&
           element3.saveData.state != ClearState.NotAcquired &&
           element4.saveData.state != ClearState.NotAcquired &&
           element5.saveData.state != ClearState.NotAcquired)
        {
            ColorUtility.TryParseHtmlString("#9E5B51", out customColor);
            setClear = true;
            rewardButton.interactable = true;
            rewardButtonText.text = "획득\n가능";
            rewardButtonImage.color = customColor;
        }

        else
        {
            rewardButton.interactable = false;
            rewardButtonText.text = "미획득";
        }
    }

    private void GetSetOption()
    {
        Color customColor = Color.white;
        ColorUtility.TryParseHtmlString("#EFA74D", out customColor);
        getReward = true;
        GameMgr.Instance.playerMgr.playerInfo.SetDatas[setID] = getReward;
        rewardButton.interactable = false;
        rewardButtonText.text = "획득\n완료";
        rewardButtonImage.color = customColor;

        GameMgr.Instance.playerMgr.playerInfo.SetOptionUpdate();
    }
}
