using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public PickUpSlot prefabSlot;
    public GameObject pickUpPanel;

    private List<PickUpSlot> pickUpitems = new List<PickUpSlot>();
    public Button pickUpButton_1;
    public Button pickUpButton_10;
    public Button pickUpButton_30;
    GachaGradeData gachaData = new GachaGradeData();

    private float C_percent;
    private float B_percent;
    private float A_percent;
    private float S_percent;
    private float SS_percent;
    private float SSS_percent;

    public GameObject probabilityGuidancePanel;
    public TextMeshProUGUI gachaTicketText;
    public TextMeshProUGUI gachaLevelText;
    public TextMeshProUGUI gachaExpText;
    public TextMeshProUGUI rewardText;
    public Image gachaExp;
    public Button explainButton;
    public Button probabilityGuidanceButton;

    private void Awake()
    {
        pickUpButton_1.onClick.AddListener(() => { PickUpItem(1); });
        pickUpButton_10.onClick.AddListener(() => { PickUpItem(10); });
        pickUpButton_30.onClick.AddListener(() => { PickUpItem(30); });
        explainButton.onClick.AddListener(OpenProbabilityGuidance);
        probabilityGuidanceButton.onClick.AddListener(CloseProbabilityGuidance);

        var playerInfo = GameMgr.Instance.playerMgr.playerInfo;
        UIUpdate(playerInfo.gachaLevel, playerInfo.gachaExp, playerInfo.gachaMaxExp, DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(playerInfo.gachaLevel).gachaRequestValue);
    }

    public void PickUpItem(int i)
    {
        if(i + GameMgr.Instance.playerMgr.playerinventory.playerEquipItemList.Count > GameMgr.Instance.playerMgr.playerinventory.maxSlots)
        {
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.gameObject.SetActive(true);
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.SetText("인벤토리가 가득 찼습니다!");

            return;
        }

        BigInteger diaCost = new BigInteger(i * 100);
        if (GameMgr.Instance.playerMgr.currency.diamond < diaCost)
        {
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.gameObject.SetActive(true);
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.SetText("다이아가 부족합니다!");

            return;
        }
        GameMgr.Instance.playerMgr.currency.RemoveDia(diaCost);
        GameMgr.Instance.uiMgr.uiWindow.pickUpResultPanel.ResultListClear();

            SetGachaPercent();

        while (pickUpitems.Count > 0)
        {
            Destroy(pickUpitems[0].gameObject);
            pickUpitems.RemoveAt(0);
        }
        for (int j = 0; j < i; j++)
        {
            float randomRarityNum = Random.Range(0.0f, 100.0f);
            // 소수점 한 자리까지 반올림
            randomRarityNum = Mathf.Round(randomRarityNum * 10f) / 10f;
            GachaPartData partData = new GachaPartData();

            if (randomRarityNum <= C_percent)
            {
                partData = DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(gachaData.Gacha1_ID);
            }
            else if (randomRarityNum <= C_percent + B_percent)
            {
                partData = DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(gachaData.Gacha2_ID);
            }
            else if (randomRarityNum <= C_percent + B_percent + A_percent)
            {
                partData = DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(gachaData.Gacha3_ID);
            }
            else if (randomRarityNum <= C_percent + B_percent + A_percent + S_percent)
            {
                partData = DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(gachaData.Gacha4_ID);
            }
            else if (randomRarityNum <= C_percent + B_percent + A_percent + S_percent + SS_percent)
            {
                partData = DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(gachaData.Gacha5_ID);
            }
            else
            {
                partData = DataTableMgr.Get<GachaPartTable>(DataTableIds.gachaPart).GetID(gachaData.Gacha6_ID);
            }

            float randomTypeNum = Random.Range(0.0f, 100.0f);
            randomTypeNum = Mathf.Round(randomTypeNum * 10f) / 10f;
            GachaItemData itemData = new GachaItemData();

            if (randomTypeNum <= partData.Gacha1_Odds)
            {
                itemData = DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(partData.Gacha1_ID);
            }
            else if (randomTypeNum <= partData.Gacha1_Odds + partData.Gacha2_Odds)
            {
                itemData = DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(partData.Gacha2_ID);
            }
            else if (randomTypeNum <= partData.Gacha1_Odds + partData.Gacha2_Odds + partData.Gacha3_Odds)
            {
                itemData = DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(partData.Gacha3_ID);
            }
            else if (randomTypeNum <= partData.Gacha1_Odds + partData.Gacha2_Odds + partData.Gacha3_Odds + partData.Gacha4_Odds)
            {
                itemData = DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(partData.Gacha4_ID);
            }
            else if (randomTypeNum <= partData.Gacha1_Odds + partData.Gacha2_Odds + partData.Gacha3_Odds + partData.Gacha4_Odds + partData.Gacha5_Odds)
            {
                itemData = DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(partData.Gacha5_ID);
            }
            else
            {
                itemData = DataTableMgr.Get<GachaItemTable>(DataTableIds.gachaItem).GetID(partData.Gacha6_ID);
            }

            float randomColorNum = Random.Range(0.0f, 100.0f);
            randomColorNum = Mathf.Round(randomColorNum * 10f) / 10f;

            EquipData equipData = new EquipData();

            if (randomColorNum <= itemData.Gacha1_Odds)
            {
                equipData = DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(itemData.Gacha1_ID);
            }
            else if (randomColorNum <= itemData.Gacha1_Odds + itemData.Gacha2_Odds)
            {
                equipData = DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(itemData.Gacha2_ID);
            }
            else if (randomColorNum <= itemData.Gacha1_Odds + itemData.Gacha2_Odds + itemData.Gacha3_Odds)
            {
                equipData = DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(itemData.Gacha3_ID);
            }
            else if (randomColorNum <= itemData.Gacha1_Odds + itemData.Gacha2_Odds + itemData.Gacha3_Odds + itemData.Gacha4_Odds)
            {
                equipData = DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(itemData.Gacha4_ID);
            }
            else
            {
                equipData = DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(itemData.Gacha5_ID);
            }

            var equip = new Equip(equipData, ++GameMgr.Instance.playerMgr.playerInfo.obtainedItem);
            int optionCount = equipData.option_value;
            while (optionCount > 0)
            {
                OptionData optionData = equipData.GetOption;
                OptionType optionType;
                float optionValue;
                OptionNumberData optionNumberData;

                float randomOption = Random.Range(0.0f, 100.0f);
                // 소수점 한 자리까지 반올림
                randomOption = Mathf.Round(randomOption * 10f) / 10f;
                if (randomOption <= optionData.option1_persent)
                {
                    optionNumberData = optionData.GetOption1_value;
                }
                else if (randomOption <= optionData.option1_persent + optionData.option2_persent)
                {
                    optionNumberData = optionData.GetOption2_value;
                }
                else if (randomOption <= optionData.option1_persent + optionData.option2_persent + optionData.option3_persent)
                {
                    optionNumberData = optionData.GetOption3_value;
                }
                else
                {
                    optionNumberData = optionData.GetOption4_value;
                }
                optionType = (OptionType)(optionNumberData.option_state - 1);
                optionValue = Random.Range(optionNumberData.option_min, optionNumberData.option_max);

                switch (optionType)
                {
                    case OptionType.attackPower:
                    case OptionType.maxHealth:
                    case OptionType.deffence:
                    case OptionType.recovery:
                        optionValue = Mathf.Round(optionValue);
                        break;
                    case OptionType.criticalPercent:
                    case OptionType.criticalMultiple:
                    case OptionType.goldIncrease:      
                        optionValue = Mathf.Round(optionValue * 10f) / 10f;
                        break;
                    case OptionType.speed:
                    case OptionType.attackRange:
                    case OptionType.attackSpeed:
                        optionValue = Mathf.Round(optionValue * 100f) / 100f;
                        break;
                }

                if (equip.SetEquipStat((optionType, optionValue)))
                {
                    optionCount--;
                    var CPData = DataTableMgr.Get<EquipmentCPTable>(DataTableIds.CP).GetID((int)optionType + 1);
                    var maxStat = CPData.GetMaxStat;
                    equip.CP += (int)(optionValue / maxStat * CPData.equipment_option_value * 10000);
                }

            }
            GameMgr.Instance.playerMgr.playerinventory.AddEquipItem(equip);
            InstantiateSlot(equip);
            GameMgr.Instance.uiMgr.uiInventory.InstantiateSlot(equip);
            GameMgr.Instance.uiMgr.uiWindow.pickUpResultPanel.gameObject.SetActive(true);
            GameMgr.Instance.uiMgr.uiWindow.pickUpResultPanel.AddResult(equip);
        }

        GameMgr.Instance.uiMgr.uiWindow.pickUpResultPanel.ShowResult();
        GameMgr.Instance.playerMgr.playerInfo.GetGachaExp(i);
        GameMgr.Instance.uiMgr.uiInventory.SortItemSlots();
        GameMgr.Instance.uiMgr.uiInventory.FilteringItemSlots();

        SaveLoadSystem.Save();
    }

    public void InstantiateSlot(Equip equip)
    {
        var newSlot = Instantiate(prefabSlot, pickUpPanel.transform);
        newSlot.SetData(equip);
        pickUpitems.Add(newSlot);
    }

    private void SetGachaPercent()
    {
        int currentLv = GameMgr.Instance.playerMgr.playerInfo.gachaLevel;
        gachaData = DataTableMgr.Get<GachaTable>(DataTableIds.gachaLevel).GetID(currentLv).getGachaGrade;
        C_percent = gachaData.Gacha1_Odds;
        B_percent = gachaData.Gacha2_Odds;
        A_percent = gachaData.Gacha3_Odds;
        S_percent = gachaData.Gacha4_Odds;
        SS_percent = gachaData.Gacha5_Odds;
        SSS_percent = gachaData.Gacha6_Odds;
    }

    public void UIUpdate(int level, int exp, int maxExp, int rewardValue)
    {
        gachaLevelText.text = level.ToString();
        gachaExp.fillAmount = (float)exp / maxExp;
        gachaExpText.text = exp.ToString() + " / " + maxExp.ToString();
        rewardText.text = rewardValue.ToString();
        if (maxExp == -1)
        {
            gachaExpText.text = exp.ToString() + " / MAX";
            rewardText.text = string.Empty;
        }
    }

    private void OpenProbabilityGuidance()
    {
        probabilityGuidancePanel.SetActive(true);
    }

    private void CloseProbabilityGuidance()
    {
        probabilityGuidancePanel.SetActive(false);
    }
}
