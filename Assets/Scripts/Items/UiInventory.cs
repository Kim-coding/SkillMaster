using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInventory : MonoBehaviour
{
    private readonly string[] sortinOptions =
{
        "획득 순 정렬",
        "부위 별 정렬",
        "등급 별 정렬",
    };

    private readonly System.Comparison<Equip>[] comparison =
{
        (x, y) => x.equipType.CompareTo(y.equipType),
        (x, y) => y.rarerityType.CompareTo(x.rarerityType),
    };

    private readonly string[] filteringOptions =
{
        "전부",
        "무기",
        "헤어",
        "얼굴",
        "상의",
        "하의",
        "망토",
    };
    private readonly System.Func<Equip, bool>[] filter =
    {
        x => true,
        x => x.equipType == EquipType.Weapon,
        x => x.equipType == EquipType.Hair,
        x => x.equipType == EquipType.Face,
        x => x.equipType == EquipType.Top,
        x => x.equipType == EquipType.Pants,
        x => x.equipType == EquipType.Cloak,
    };

    private int sortinOption;
    private int filteringOption;

    public ItemSlot prefabSlot;
    public TMP_Dropdown sorting;
    public Toggle currentToggle;

    public GameObject inventoryPanel;

    private List<Equip> sortedList = new List<Equip>();
    private List<ItemSlot> selectedSlots = new List<ItemSlot>();


    private void UpdateItemSlots()
    {
        // 정렬 필터링
    }

    private void Awake()
    {
        //데이터 테이블 호출
        //키 호출

        var iconimage = Resources.Load<Sprite>($"Icon_Helmet02");
        var equip = new Equip(iconimage, "Icon_Helmet02", "투구");
        equip.SetEquipItem(EquipType.Hair, RarerityType.S);
        var newSlot = Instantiate(prefabSlot, inventoryPanel.transform);
        newSlot.SetData(equip);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.N))
        {
            var iconimage = Resources.Load<Sprite>($"Icon_Helmet02");
            var equip = new Equip(iconimage, "Icon_Helmet02", "투구");
            equip.SetEquipItem(EquipType.Hair, RarerityType.S);
            var newSlot = Instantiate(prefabSlot, inventoryPanel.transform);
            newSlot.SetData(equip);
        }
    }
}
