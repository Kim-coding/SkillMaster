using System.Collections.Generic;
using System.Text;
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

    private float C_percent;
    private float B_percent;
    private float A_percent;
    private float S_percent;
    private float SS_percent;
    private float SSS_percent;

    private void Awake()
    {
        pickUpButton_1.onClick.AddListener(() => { PickUpItem(1); });
        pickUpButton_10.onClick.AddListener(() => { PickUpItem(10); });
        pickUpButton_30.onClick.AddListener(() => { PickUpItem(30); });
    }

    public void PickUpItem(int i)
    {
        if(i + GameMgr.Instance.playerMgr.playerinventory.playerEquipItemList.Count > GameMgr.Instance.playerMgr.playerinventory.maxSlots)
        {
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.gameObject.SetActive(true);
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.SetText("인벤토리가 가득 찼습니다!");

            return;
        }


        SetGachaPercent();

        while (pickUpitems.Count > 0)
        {
            Destroy(pickUpitems[0].gameObject);
            pickUpitems.RemoveAt(0);
        }
        for (int j = 0; j < i; j++)
        {
            StringBuilder sb = new StringBuilder();
            float randomRarityNum = Random.Range(0.0f, 100.0f);
            // 소수점 한 자리까지 반올림
            randomRarityNum = Mathf.Round(randomRarityNum * 10f) / 10f;

            var randomTypeNum = Random.Range(1, 7);
            switch (randomTypeNum)
            {
                case 1:
                    sb.Append("Hair");
                    break;
                case 2:
                    sb.Append("Eye");
                    break;
                case 3:
                    sb.Append("Cloth");
                    break;
                case 4:
                    sb.Append("Pant");
                    break;
                case 5:
                    sb.Append("Weapon");
                    break;
                case 6:
                    sb.Append("Back");
                    break;
            }

            if (randomRarityNum <= C_percent)
            {
                sb.Append("_C");
                randomRarityNum = 1;
            }
            else if (randomRarityNum <= C_percent + B_percent)
            {
                sb.Append("_B");
                randomRarityNum = 2;
            }
            else if (randomRarityNum <= C_percent + B_percent + A_percent)
            {
                sb.Append("_A");
                randomRarityNum = 3;
            }
            else if (randomRarityNum <= C_percent + B_percent + A_percent + S_percent)
            {
                sb.Append("_S");
                randomRarityNum = 4;
            }
            else if (randomRarityNum <= C_percent + B_percent + A_percent + S_percent + SS_percent)
            {
                sb.Append("_SS");
                randomRarityNum = 5;
            }
            else
            {
                sb.Append("_SSS");
                randomRarityNum = 6;
            }

            var randomColorNum = Random.Range(1, 6);
            switch (randomColorNum)
            {
                case 1:
                    sb.Append("_1");
                    break;
                case 2:
                    sb.Append("_2");
                    break;
                case 3:
                    sb.Append("_3");
                    break;
                case 4:
                    sb.Append("_4");
                    break;
                case 5:
                    sb.Append("_5");
                    break;
            }
            string sbString = sb.ToString();
            EquipData equipData = DataTableMgr.Get<EquipmentTable>(DataTableIds.equipment).GetID(sbString);

            var iconimage = Resources.LoadAll<Sprite>("Equipment/" + sbString);
            var equip = new Equip(iconimage, equipData.GetItemName, ++GameMgr.Instance.playerMgr.playerInfo.obtainedItem);
            equip.SetEquipItem((EquipType)randomTypeNum, (RarerityType)randomRarityNum, equipData.reinforcement_value);

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
                }

            }
            GameMgr.Instance.playerMgr.playerinventory.AddEquipItem(equip);
            InstantiateSlot(equip);
            GameMgr.Instance.uiMgr.uiInventory.InstantiateSlot(equip);
        }

        GameMgr.Instance.uiMgr.uiInventory.SortItemSlots();
        GameMgr.Instance.uiMgr.uiInventory.FilteringItemSlots();
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
        GachaData gachaData = DataTableMgr.Get<GachaTable>(DataTableIds.gacha).GetID(currentLv);
        C_percent = gachaData.C_Probability;
        B_percent = gachaData.B_Probability;
        A_percent = gachaData.A_Probability;
        S_percent = gachaData.S_Probability;
        SS_percent = gachaData.SS_Probability;
        SSS_percent = gachaData.SSS_Probability;
    }

}
