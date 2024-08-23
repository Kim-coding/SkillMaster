using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIDungeon : MonoBehaviour
{

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI goldLvText;
    public TextMeshProUGUI diaText;
    public TextMeshProUGUI diaLvText;
    public TextMeshProUGUI goldKeyText1;
    public TextMeshProUGUI goldKeyText2;
    public TextMeshProUGUI diaKeyText1;
    public TextMeshProUGUI diaKeyText2;

    public Button goldClearButton;
    public Button goldDungeonButton;
    public Button diaClearButton;
    public Button diaDungeonButton;

    public void Init()
    {
        goldDungeonButton.onClick.AddListener(SetGoldDungeon);
        goldClearButton.onClick.AddListener(ClearGoldDungeon);
        diaDungeonButton.onClick.AddListener(SetDiaDungeon);
        diaClearButton.onClick.AddListener(ClearDiaDungeon);
        DungeonUIUpdate();
    }



    public void DungeonUIUpdate()
    {
        var playerInfo = GameMgr.Instance.playerMgr.playerInfo;

        goldLvText.text = playerInfo.goldDungeonLv.ToString();
        diaLvText.text = playerInfo.diaDungeonLv.ToString();
        goldText.text = new BigInteger(DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(playerInfo.goldDungeonLv).reward_value).ToStringShort();
        diaText.text = DataTableMgr.Get<DiaDungeonTable>(DataTableIds.diaDungeon).GetID(playerInfo.diaDungeonLv).reward_value;

        goldKeyText1.text = "0";
        goldKeyText2.text = "0";
        diaKeyText1.text = "0";
        diaKeyText2.text = "0";

        if(playerInfo.diaDungeonLv == 1)
        {
            diaClearButton.interactable = false;
        }
        else
        {
            diaClearButton.interactable = true;
        }

        foreach (var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
        {
            if (item.itemNumber == 220003)
            {
                goldKeyText1.text = item.itemValue.ToString();
                goldKeyText2.text = item.itemValue.ToString();
            }
            if (item.itemNumber == 220004)
            {
                diaKeyText1.text = item.itemValue.ToString();
                diaKeyText2.text = item.itemValue.ToString();
            }
        }

    }


    private void Dungeon()
    {
        GameMgr.Instance.webTimeMgr.SaveTime();
        SaveLoadSystem.Save();
        SceneManager.LoadScene("Dungeon");
    }

    public void ClearGoldDungeon()
    {
        NormalItem key = null;
        foreach (var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
        {
            if (item.itemNumber == 220003)
            {
                key = item;
                break;
            }
        }
        if (key == null || key.itemValue == 0)
        {
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.SetText("열쇠가 부족합니다!");
            return;
        }

        key.itemValue--;
        BigInteger gold = new BigInteger(DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(GameMgr.Instance.playerMgr.playerInfo.goldDungeonLv).reward_value);
        GameMgr.Instance.rewardMgr.SetReward(gold.ToStringShort(),true);
        GameMgr.Instance.playerMgr.currency.AddGold(gold);
        GameMgr.Instance.uiMgr.uiInventory.NormalItemUpdate();
        EventMgr.TriggerEvent(QuestType.GoldDungeon);
        DungeonUIUpdate();
        SaveLoadSystem.Save();
    }

    public void SetGoldDungeon()
    {
        NormalItem key = null;
        foreach(var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
        {
            if(item.itemNumber == 220003)
            {
                key = item; 
                break;
            }
        }
        if( key == null  || key.itemValue == 0)
        {
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.SetText("열쇠가 부족합니다!");
            return;
        }

        GameMgr.Instance.uiMgr.uiInventory.NormalItemUpdate();
        GameMgr.Instance.playerMgr.playerInfo.dungeonMode = true;
        EventMgr.TriggerEvent(QuestType.GoldDungeon);
        Dungeon();
    }


    public void ClearDiaDungeon()
    {
        NormalItem key = null;
        foreach (var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
        {
            if (item.itemNumber == 220004)
            {
                key = item;
                break;
            }
        }
        if (key == null || key.itemValue == 0)
        {
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.SetText("열쇠가 부족합니다!");
            return;
        }

        key.itemValue--;
        BigInteger dia = new BigInteger(DataTableMgr.Get<DiaDungeonTable>(DataTableIds.diaDungeon).GetID(GameMgr.Instance.playerMgr.playerInfo.diaDungeonLv - 1).reward_value);
        GameMgr.Instance.rewardMgr.SetReward(dia.ToString());
        GameMgr.Instance.playerMgr.currency.AddDia(dia);
        GameMgr.Instance.uiMgr.uiInventory.NormalItemUpdate();
        EventMgr.TriggerEvent(QuestType.DiaDungeon);
        DungeonUIUpdate();
        SaveLoadSystem.Save();
    }

    public void SetDiaDungeon()
    {
        NormalItem key = null;
        foreach (var item in GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList)
        {
            if (item.itemNumber == 220004)
            {
                key = item;
                break;
            }
        }
        if (key == null || key.itemValue == 0)
        {
            GameMgr.Instance.uiMgr.uiWindow.popUpUI.SetText("열쇠가 부족합니다!");
            return;
        }

        GameMgr.Instance.uiMgr.uiInventory.NormalItemUpdate();
        GameMgr.Instance.playerMgr.playerInfo.dungeonMode = false;
        EventMgr.TriggerEvent(QuestType.DiaDungeon);
        Dungeon();
    }

}
