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



        DungeonUIUpdate();

    }



    public void DungeonUIUpdate()
    {
        var playerInfo = GameMgr.Instance.playerMgr.playerInfo;

        goldLvText.text = playerInfo.goldDungeonLv.ToString();
        diaLvText.text = playerInfo.diaDungeonLv.ToString();
        goldText.text = new BigInteger(DataTableMgr.Get<GoldDungeonTable>(DataTableIds.goldDungeon).GetID(playerInfo.goldDungeonLv).reward_value).ToStringShort();
        diaText.text = DataTableMgr.Get<DiaDungeonTable>(DataTableIds.diaDungeon).GetID(playerInfo.diaDungeonLv).reward_value;
    }


    private void Dungeon()
    {
        GameMgr.Instance.webTimeMgr.SaveTime();
        SaveLoadSystem.Save();
        SceneManager.LoadScene("Dungeon");
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
            return;
        }

        key.itemValue--;
        GameMgr.Instance.playerMgr.playerInfo.dungeonMode = true;
        EventMgr.TriggerEvent(QuestType.GoldDungeon);
        Dungeon();
    }

    public void SetDiaDungeon()
    {
        GameMgr.Instance.playerMgr.playerInfo.dungeonMode = false;
        EventMgr.TriggerEvent(QuestType.DiaDungeon);
        Dungeon();
    }

}
