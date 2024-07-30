using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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
        x => x.equipType == EquipType.Cloth,
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

    public SPUM_SpriteList invenSpriteList;
    public SPUM_SpriteList playerSpriteList;

    public EquipSlot hairSlot;
    public EquipSlot faceSlot;
    public EquipSlot clothSlot;
    public EquipSlot pantSlot;
    public EquipSlot weaponSlot;
    public EquipSlot cloakSlot;


    public void SortItemSlots()
    {
        // 정렬 필터링
    }

    public void UiSlotUpdate(EquipType type)
    {
        switch (type)
        {
            case EquipType.None:
                break;
            case EquipType.Hair:
                hairSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerHair);
                invenSpriteList._hairList[0].sprite = hairSlot.currentEquip.icon[0];
                playerSpriteList._hairList[0].sprite = hairSlot.currentEquip.icon[0];
                break;
            case EquipType.Face:
                faceSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerFace);
                invenSpriteList._eyeList[0].sprite = faceSlot.currentEquip.icon[0];
                invenSpriteList._eyeList[1].sprite = faceSlot.currentEquip.icon[0];
                invenSpriteList._eyeList[2].sprite = faceSlot.currentEquip.icon[1];
                invenSpriteList._eyeList[3].sprite = faceSlot.currentEquip.icon[1];

                playerSpriteList._eyeList[0].sprite = faceSlot.currentEquip.icon[0];
                playerSpriteList._eyeList[1].sprite = faceSlot.currentEquip.icon[0];
                playerSpriteList._eyeList[2].sprite = faceSlot.currentEquip.icon[1];
                playerSpriteList._eyeList[3].sprite = faceSlot.currentEquip.icon[1];
                break;
            case EquipType.Cloth:
                clothSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerCloth);
                invenSpriteList._clothList[0].sprite = clothSlot.currentEquip.icon[0];
                invenSpriteList._clothList[1].sprite = clothSlot.currentEquip.icon[1];
                invenSpriteList._clothList[2].sprite = clothSlot.currentEquip.icon[2];

                playerSpriteList._clothList[0].sprite = clothSlot.currentEquip.icon[0];
                playerSpriteList._clothList[1].sprite = clothSlot.currentEquip.icon[1];
                playerSpriteList._clothList[2].sprite = clothSlot.currentEquip.icon[2];
                break;
            case EquipType.Pants:
                pantSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerPant);
                invenSpriteList._pantList[0].sprite = pantSlot.currentEquip.icon[0];
                invenSpriteList._pantList[1].sprite = pantSlot.currentEquip.icon[1];

                playerSpriteList._pantList[0].sprite = pantSlot.currentEquip.icon[0];
                playerSpriteList._pantList[1].sprite = pantSlot.currentEquip.icon[1];
                break;
            case EquipType.Weapon:
                weaponSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerWeapon);
                invenSpriteList._weaponList[0].sprite = weaponSlot.currentEquip.icon[0];

                playerSpriteList._weaponList[0].sprite = weaponSlot.currentEquip.icon[0];
                break;
            case EquipType.Cloak:
                cloakSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerCloak);
                invenSpriteList._backList[0].sprite = cloakSlot.currentEquip.icon[0];

                playerSpriteList._backList[0].sprite = cloakSlot.currentEquip.icon[0];

                break;
        }

    }

    private void Awake()
    {
        //데이터 테이블 호출
        //키 호출

        AllSlotUpdate();
    }

    public void Init()
    {
        AllSlotUpdate();
    }

    public void AllSlotUpdate()
    {
        UiSlotUpdate(EquipType.Hair);
        UiSlotUpdate(EquipType.Face);
        UiSlotUpdate(EquipType.Cloth);
        UiSlotUpdate(EquipType.Pants);
        UiSlotUpdate(EquipType.Weapon);
        UiSlotUpdate(EquipType.Cloak);
    }


    public void InstantiateSlot(Equip equip)
    {
        var newSlot = Instantiate(prefabSlot, inventoryPanel.transform);
        newSlot.SetData(equip);
    }

}
