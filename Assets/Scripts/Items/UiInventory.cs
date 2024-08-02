using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInventory : MonoBehaviour
{

    private readonly System.Comparison<ItemSlot>[] comparison =
{
        (x, y) => x.currentEquip.itemNumber.CompareTo(y.currentEquip.itemNumber),
        (x, y) => x.currentEquip.equipType.CompareTo(y.currentEquip.equipType),
        (x, y) => y.currentEquip.rarerityType.CompareTo(x.currentEquip.rarerityType),
    };

    public ToggleGroup filteringOptions;
    public Toggle[] toggles;
    private int currentToggleNumber;

    private readonly System.Func<ItemSlot, bool>[] filter =
    {
        x => true,
        x => x.currentEquip.equipType == EquipType.Hair,
        x => x.currentEquip.equipType == EquipType.Face,
        x => x.currentEquip.equipType == EquipType.Cloth,
        x => x.currentEquip.equipType == EquipType.Pants,
        x => x.currentEquip.equipType == EquipType.Weapon,
        x => x.currentEquip.equipType == EquipType.Cloak,
    };

    public ItemSlot prefabSlot;
    public GameObject inventoryPanel;
    private List<ItemSlot> selectedSlots = new List<ItemSlot>();

    public SPUM_SpriteList invenSpriteList;
    public SPUM_SpriteList playerSpriteList;

    public EquipSlot hairSlot;
    public EquipSlot faceSlot;
    public EquipSlot clothSlot;
    public EquipSlot pantSlot;
    public EquipSlot weaponSlot;
    public EquipSlot cloakSlot;

    public TMP_Dropdown sortDropDown;


    public void SortItemSlots()
    {
        selectedSlots.Sort(comparison[sortDropDown.value]);
        foreach (var slot in selectedSlots)
        {
            slot.transform.SetAsLastSibling();
        }
    }

    public void FilteringItemSlots()
    {
        System.Func<ItemSlot, bool> currentFilter = filter[currentToggleNumber];

        foreach (var child in selectedSlots)
        {
            if (child != null)
            {
                if (currentFilter(child))
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    private void UpdateToggleColors()
    {
        foreach (var toggle in toggles)
        {
            if (toggle.isOn)
            {
                SetToggleColor(toggle, Color.blue);
            }
            else
            {
                SetToggleColor(toggle, Color.white);
            }
        }
    }

    private void SetToggleColor(Toggle toggle, Color color)
    {
        var background = toggle.targetGraphic;
        var checkmark = toggle.graphic;

        if (background != null)
        {
            background.color = color;
        }

        if (checkmark != null)
        {
            checkmark.color = color;
        }
    }

    public void UiSlotUpdate(EquipType type)
    {
        switch (type)
        {
            case EquipType.None:
                break;
            case EquipType.Hair:
                hairSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerHair);
                invenSpriteList._hairList[0].sprite = hairSlot.currentEquip.texture[0];
                playerSpriteList._hairList[0].sprite = hairSlot.currentEquip.texture[0];
                break;
            case EquipType.Face:
                faceSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerFace);
                invenSpriteList._eyeList[0].sprite = faceSlot.currentEquip.texture[0];
                invenSpriteList._eyeList[1].sprite = faceSlot.currentEquip.texture[0];
                invenSpriteList._eyeList[2].sprite = faceSlot.currentEquip.texture[1];
                invenSpriteList._eyeList[3].sprite = faceSlot.currentEquip.texture[1];

                playerSpriteList._eyeList[0].sprite = faceSlot.currentEquip.texture[0];
                playerSpriteList._eyeList[1].sprite = faceSlot.currentEquip.texture[0];
                playerSpriteList._eyeList[2].sprite = faceSlot.currentEquip.texture[1];
                playerSpriteList._eyeList[3].sprite = faceSlot.currentEquip.texture[1];
                break;
            case EquipType.Cloth:
                clothSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerCloth);
                invenSpriteList._clothList[0].sprite = clothSlot.currentEquip.texture[0];
                invenSpriteList._clothList[1].sprite = clothSlot.currentEquip.texture[1];
                invenSpriteList._clothList[2].sprite = clothSlot.currentEquip.texture[2];

                playerSpriteList._clothList[0].sprite = clothSlot.currentEquip.texture[0];
                playerSpriteList._clothList[1].sprite = clothSlot.currentEquip.texture[1];
                playerSpriteList._clothList[2].sprite = clothSlot.currentEquip.texture[2];
                break;
            case EquipType.Pants:
                pantSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerPant);
                invenSpriteList._pantList[0].sprite = pantSlot.currentEquip.texture[0];
                invenSpriteList._pantList[1].sprite = pantSlot.currentEquip.texture[1];

                playerSpriteList._pantList[0].sprite = pantSlot.currentEquip.texture[0];
                playerSpriteList._pantList[1].sprite = pantSlot.currentEquip.texture[1];
                break;
            case EquipType.Weapon:
                weaponSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerWeapon);
                invenSpriteList._weaponList[0].sprite = weaponSlot.currentEquip.texture[0];

                playerSpriteList._weaponList[0].sprite = weaponSlot.currentEquip.texture[0];
                break;
            case EquipType.Cloak:
                cloakSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerCloak);
                invenSpriteList._backList[0].sprite = cloakSlot.currentEquip.texture[0];

                playerSpriteList._backList[0].sprite = cloakSlot.currentEquip.texture[0];

                break;
        }

    }

    private void Awake()
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
        UpdateToggleColors();
        //데이터 테이블 호출
        //키 호출

        AllSlotUpdate();

    }
    private void OnToggleValueChanged(bool isOn)
    {
        UpdateToggleColors();
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                currentToggleNumber = i;
                break;
            }
        }
        FilteringItemSlots();
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
        selectedSlots.Add(newSlot);
    }

}
