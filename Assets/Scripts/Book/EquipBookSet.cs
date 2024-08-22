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
    public TextMeshProUGUI rewardButtonText;


    public void Init(int setID)
    {
        var setData = DataTableMgr.Get<EquipBookTable>(DataTableIds.equipBook).GetID(setID);
        if( setData == null )
        {
            return;
        }

        element1 = Instantiate(elementPrefab, equipPanel.transform);
        element1.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment1_id];
        element1.Init();

        element2 = Instantiate(elementPrefab, equipPanel.transform);
        element2.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment2_id];
        element2.Init();

        element3 = Instantiate(elementPrefab, equipPanel.transform);
        element3.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment3_id];
        element3.Init();

        element4 = Instantiate(elementPrefab, equipPanel.transform);
        element4.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment4_id];
        element4.Init();

        element5 = Instantiate(elementPrefab, equipPanel.transform);
        element5.saveData = GameMgr.Instance.playerMgr.playerInfo.equipBookDatas[setData.equipment5_id];
        element5.Init();


    }
}
