using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInventory : MonoBehaviour
{
    private readonly string[] sortinOptions =
{
        "ȹ�� �� ����",
        "���� �� ����",
        "��� �� ����",
    };

    private readonly System.Comparison<Equip>[] comparison =
{
        (x, y) => x.equipType.CompareTo(y.equipType),
        (x, y) => y.rarerityType.CompareTo(x.rarerityType),
    };

    private readonly string[] filteringOptions =
{
        "����",
        "����",
        "���",
        "��",
        "����",
        "����",
        "����",
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
        // ���� ���͸�
    }

    private void Awake()
    {
        //������ ���̺� ȣ��
        //Ű ȣ��

        var iconimage = Resources.Load<Sprite>($"Icon_Helmet02");
        var equip = new Equip(iconimage, "Icon_Helmet02", "����");
        equip.SetEquipItem(EquipType.Hair, RarerityType.S);
        var newSlot = Instantiate(prefabSlot, inventoryPanel.transform);
        newSlot.SetData(equip);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.N))
        {
            var iconimage = Resources.Load<Sprite>($"Icon_Helmet02");
            var equip = new Equip(iconimage, "Icon_Helmet02", "����");
            equip.SetEquipItem(EquipType.Hair, RarerityType.S);
            var newSlot = Instantiate(prefabSlot, inventoryPanel.transform);
            newSlot.SetData(equip);
        }
    }
}
