using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class UiInventory : MonoBehaviour
{
    /// <summary>
    /// 인벤토리 슬롯 관리
    /// </summary>
    private readonly System.Comparison<EquipItemSlot>[] comparison =
{
        (x, y) => x.currentEquip.itemNumber.CompareTo(y.currentEquip.itemNumber),
        //(x, y) => x.currentEquip.equipType.CompareTo(y.currentEquip.equipType),
        //(x, y) => y.currentEquip.rarerityType.CompareTo(x.currentEquip.rarerityType),
        (x, y) => { int rarityComparison = y.currentEquip.rarerityType.CompareTo(x.currentEquip.rarerityType);
                        if (rarityComparison != 0)
                        {
                             return rarityComparison;
                        }
                              int equipTypeComparison = x.currentEquip.equipType.CompareTo(y.currentEquip.equipType);

                         if (equipTypeComparison != 0)
                         {
                             return equipTypeComparison;
                         }
                             return x.currentEquip.itemNumber.CompareTo(y.currentEquip.itemNumber);
        }
    };

    private readonly System.Func<EquipItemSlot, bool>[] filter =
{
        x => true,
        x => x.currentEquip.equipType == EquipType.Hair,
        x => x.currentEquip.equipType == EquipType.Face,
        x => x.currentEquip.equipType == EquipType.Cloth,
        x => x.currentEquip.equipType == EquipType.Pants,
        x => x.currentEquip.equipType == EquipType.Weapon,
        x => x.currentEquip.equipType == EquipType.Cloak,
    };
    public Toggle ascendingToggle;
    public TextMeshProUGUI ascendingToggleText;

    public ToggleGroup filteringOptions;
    public Toggle[] filteringToggles;
    private int currentToggleNumber;

    public ToggleGroup inventoryModes;
    public Toggle[] inventoryModeToggles;
    private int inventoryModeToggleNumber;

    public GameObject equipButtons;
    public GameObject equipSlotPanel;
    public GameObject equipTogglePanel;
    public GameObject itemButtons;
    public GameObject itemSlotPanel;

    public EquipItemSlot prefabEquipSlot;
    public NormalItemSlot prefabNormalSlot;
    public GameObject normalInventoryPanel;
    public GameObject equipInventoryPanel;
    private List<EquipItemSlot> equipItemSlots = new List<EquipItemSlot>();
    private List<NormalItemSlot> normalItemSlots = new List<NormalItemSlot>();

    public TMP_Dropdown sortDropDown;
    public TextMeshProUGUI equipItemSlotCountText;
    public TextMeshProUGUI normalItemSlotCountText;


    /// <summary>
    /// 분해 관리
    /// </summary>

    public List<EquipItemSlot> selectedSlot = new List<EquipItemSlot>();

    public bool decomposMode = false;
    public Button decomposButton;

    public GameObject decomposPanel;
    public GameObject sortPanel;

    public Button autoDecomposButton;
    public Button cancleDecomposButton;
    public Button confirmDecomposButton;


    /// <summary>
    /// 장비 장착 관련
    /// </summary>
    public SPUM_SpriteList invenSpriteList;
    public SPUM_SpriteList playerSpriteList;

    public EquipSlot hairSlot;
    public EquipSlot faceSlot;
    public EquipSlot clothSlot;
    public EquipSlot pantSlot;
    public EquipSlot weaponSlot;
    public EquipSlot cloakSlot;

    Dictionary<EquipType, Equip> baseEquipments;


    /// <summary>
    /// 장비 강화 관련
    /// </summary>
    public Button equipUpgradeButton;

    public void SortItemSlots()
    {
        equipItemSlots.Sort(comparison[sortDropDown.value]);
        foreach (var slot in equipItemSlots)
        {
            if (ascendingToggle.isOn)
            {
                slot.transform.SetAsLastSibling();
            }
            else
            {
                slot.transform.SetAsFirstSibling();
            }
        }
    }

    public void FilteringItemSlots()
    {
        System.Func<EquipItemSlot, bool> currentFilter = filter[currentToggleNumber];

        foreach (var child in equipItemSlots)
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
        foreach (var toggle in filteringToggles)
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

        foreach (var toggle in inventoryModeToggles)
        {
            if (toggle.isOn)
            {
                SetToggleColor(toggle, new Color(0, 0, 0, 0));
            }
            else
            {
                SetToggleColor(toggle, new Color(0f, 107f / 255f, 255f / 255f, 255f / 255f));
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
                hairSlot.baseEquip = hairSlot.currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseHair;
                invenSpriteList._hairList[0].sprite = hairSlot.currentEquip.texture[0];
                playerSpriteList._hairList[0].sprite = hairSlot.currentEquip.texture[0];
                break;
            case EquipType.Face:
                faceSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerFace);
                faceSlot.baseEquip = faceSlot.currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseFace;
                invenSpriteList._eyeList[0].sprite = faceSlot.currentEquip.texture[1];
                invenSpriteList._eyeList[1].sprite = faceSlot.currentEquip.texture[1];
                invenSpriteList._eyeList[2].sprite = faceSlot.currentEquip.texture[2];
                invenSpriteList._eyeList[3].sprite = faceSlot.currentEquip.texture[2];

                playerSpriteList._eyeList[0].sprite = faceSlot.currentEquip.texture[1];
                playerSpriteList._eyeList[1].sprite = faceSlot.currentEquip.texture[1];
                playerSpriteList._eyeList[2].sprite = faceSlot.currentEquip.texture[2];
                playerSpriteList._eyeList[3].sprite = faceSlot.currentEquip.texture[2];
                break;
            case EquipType.Cloth:
                clothSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerCloth);
                clothSlot.baseEquip = clothSlot.currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseCloth;
                invenSpriteList._clothList[0].sprite = clothSlot.currentEquip.texture[0];
                invenSpriteList._clothList[1].sprite = clothSlot.currentEquip.texture[1];
                invenSpriteList._clothList[2].sprite = clothSlot.currentEquip.texture[2];

                playerSpriteList._clothList[0].sprite = clothSlot.currentEquip.texture[0];
                playerSpriteList._clothList[1].sprite = clothSlot.currentEquip.texture[1];
                playerSpriteList._clothList[2].sprite = clothSlot.currentEquip.texture[2];
                break;
            case EquipType.Pants:
                pantSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerPant);
                pantSlot.baseEquip = pantSlot.currentEquip == GameMgr.Instance.playerMgr.playerinventory.basePant;
                invenSpriteList._pantList[0].sprite = pantSlot.currentEquip.texture[0];
                invenSpriteList._pantList[1].sprite = pantSlot.currentEquip.texture[1];

                playerSpriteList._pantList[0].sprite = pantSlot.currentEquip.texture[0];
                playerSpriteList._pantList[1].sprite = pantSlot.currentEquip.texture[1];
                break;
            case EquipType.Weapon:
                weaponSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerWeapon);
                weaponSlot.baseEquip = weaponSlot.currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseWeapon;
                invenSpriteList._weaponList[0].sprite = weaponSlot.currentEquip.texture[0];

                playerSpriteList._weaponList[0].sprite = weaponSlot.currentEquip.texture[0];
                break;
            case EquipType.Cloak:
                cloakSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.playerCloak);
                cloakSlot.baseEquip = cloakSlot.currentEquip == GameMgr.Instance.playerMgr.playerinventory.baseCloak;
                invenSpriteList._backList[0].sprite = cloakSlot.currentEquip.texture[0];
                playerSpriteList._backList[0].sprite = cloakSlot.currentEquip.texture[0];

                break;
        }

    }

    private void Awake()
    {
        ascendingToggle.onValueChanged.AddListener(AscendingValueChange);

        foreach (Toggle toggle in filteringToggles)
        {
            toggle.onValueChanged.AddListener(OnFilteringToggleValueChanged);
        }
        foreach (Toggle toggle in inventoryModeToggles)
        {
            toggle.onValueChanged.AddListener(OnInventoryModeToggleValueChanged);
        }
        UpdateToggleColors();

        baseEquipments = new Dictionary<EquipType, Equip>
        {
            { EquipType.Hair, GameMgr.Instance.playerMgr.playerinventory.baseHair },
            { EquipType.Face, GameMgr.Instance.playerMgr.playerinventory.baseFace },
            { EquipType.Cloth, GameMgr.Instance.playerMgr.playerinventory.baseCloth },
            { EquipType.Pants, GameMgr.Instance.playerMgr.playerinventory.basePant },
            { EquipType.Weapon, GameMgr.Instance.playerMgr.playerinventory.baseWeapon },
            { EquipType.Cloak, GameMgr.Instance.playerMgr.playerinventory.baseCloak }
        };
        decomposButton.onClick.AddListener(OnDecomposMode);
        cancleDecomposButton.onClick.AddListener(OffDecomposMode);
        confirmDecomposButton.onClick.AddListener(OpenDecomposPanel);
        autoDecomposButton.onClick.AddListener(OpenAutoDecomposPanel);
        equipUpgradeButton.onClick.AddListener(OpenEquipUpgradePanel);
        //데이터 테이블 호출
        //키 호출

        AllSlotUpdate();
        SlotLevelUpdate();
    }
    private void OnFilteringToggleValueChanged(bool isOn)
    {
        UpdateToggleColors();
        for (int i = 0; i < filteringToggles.Length; i++)
        {
            if (filteringToggles[i].isOn)
            {
                currentToggleNumber = i;
                break;
            }
        }
        FilteringItemSlots();
    }

    private void AscendingValueChange(bool isOn)
    {
        if(ascendingToggle.isOn)
        {
            ascendingToggleText.text = "오름차순";
        }
        else
        {
            ascendingToggleText.text = "내림차순";
        }
        SortItemSlots();
    }

    private void OnInventoryModeToggleValueChanged(bool isOn)
    {
        bool mode = true;
        UpdateToggleColors();
        for (int i = 0; i < inventoryModeToggles.Length; i++)
        {
            if (inventoryModeToggles[i].isOn)
            {
                inventoryModeToggleNumber = i;
                if (i == 0)
                {
                    mode = true;
                }
                else
                {
                    mode = false;
                }
                break;
            }
        }
        InventoryModeChange(mode);
    }

    private void InventoryModeChange(bool mode)
    {
        equipButtons.gameObject.SetActive(mode);
        equipSlotPanel.gameObject.SetActive(mode);
        equipTogglePanel.gameObject.SetActive(mode);
        itemButtons.gameObject.SetActive(!mode);
        itemSlotPanel.gameObject.SetActive(!mode);
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

    public void UnEquipAllSlot()
    {
        hairSlot.RemoveEquip();
        faceSlot.RemoveEquip();
        clothSlot.RemoveEquip();
        pantSlot.RemoveEquip();
        weaponSlot.RemoveEquip();
        cloakSlot.RemoveEquip();
    }

    public EquipItemSlot InstantiateSlot(Equip equip)
    {
        var newSlot = Instantiate(prefabEquipSlot, equipInventoryPanel.transform);
        newSlot.SetData(equip);
        equipItemSlots.Add(newSlot);

        EquipSlotCountUpdate();

        return newSlot;
    }

    public void NormalItemUpdate()
    {
        foreach (var slot in normalItemSlots)
        {
            slot.itemCountUpdate();
        }
    }

    public void InstantiateSlot(NormalItem item, int value)
    {
        foreach (var slot in normalItemSlots)
        {
            if (slot.currentItem.itemNumber == item.itemNumber)
            {
                slot.currentItem.itemValue += value;
                slot.itemCountUpdate();
                NormalSlotCountUpdate();
                return;
            }
        }

        GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList.Add(item);
        var newSlot = Instantiate(prefabNormalSlot, normalInventoryPanel.transform);
        newSlot.SetData(item);
        normalItemSlots.Add(newSlot);

        NormalSlotCountUpdate();
    }

    public void ChangeEquip(EquipItemSlot newSlot)
    {
        newSlot.SetData(GameMgr.Instance.playerMgr.playerinventory.EquipItem(newSlot.currentEquip));
        GameMgr.Instance.uiMgr.uiInventory.UiSlotUpdate(newSlot.currentEquip.equipType);
        if (baseEquipments.TryGetValue(newSlot.currentEquip.equipType, out var baseEquip) &&
             newSlot.currentEquip == baseEquip)
        {
            equipItemSlots.Remove(newSlot);
            Destroy(newSlot.gameObject);
        }

        EquipSlotCountUpdate();
    }

    public void EquipSlotCountUpdate()
    {
        equipItemSlotCountText.text = GameMgr.Instance.playerMgr.playerinventory.playerEquipItemList.Count.ToString() + " / " + GameMgr.Instance.playerMgr.playerinventory.maxSlots.ToString();
    }

    public void NormalSlotCountUpdate()
    {
        normalItemSlotCountText.text = GameMgr.Instance.playerMgr.playerinventory.playerNormalItemList.Count.ToString() + " / " + GameMgr.Instance.playerMgr.playerinventory.maxSlots.ToString();
    }

    public void OnDecomposMode()
    {
        decomposMode = true;
        sortPanel.gameObject.SetActive(false);
        decomposPanel.gameObject.SetActive(true);
        DecomposButtonUpdate();
    }

    public bool DecomposSelect(EquipItemSlot slot)
    {
        if (!selectedSlot.Contains(slot))
        {
            selectedSlot.Add(slot);
            DecomposButtonUpdate();
            return true;
        }
        selectedSlot.Remove(slot);
        DecomposButtonUpdate();
        return false;
    }

    public void OffDecomposMode()
    {
        foreach (var item in selectedSlot)
        {
            item.OnSelected(false);
        }
        selectedSlot.Clear();

        decomposMode = false;
        sortPanel.gameObject.SetActive(true);
        decomposPanel.gameObject.SetActive(false);

    }

    public void OpenDecomposPanel()
    {
        GameMgr.Instance.uiMgr.uiWindow.decomposPanel.gameObject.SetActive(true);
        GameMgr.Instance.uiMgr.uiWindow.decomposPanel.SetDecompos(selectedSlot);

    }

    public void Decompos(int reinforcevalue)
    {
        foreach (var item in selectedSlot)
        {
            GameMgr.Instance.playerMgr.playerinventory.RemoveEquipItem(item.currentEquip);
            equipItemSlots.Remove(item);
            Destroy(item.gameObject);
        }

        NormalItem newitem = new NormalItem(220006, reinforcevalue);
        InstantiateSlot(newitem, reinforcevalue);

        EquipSlotCountUpdate();
        OffDecomposMode();
    }

    public void OpenAutoDecomposPanel()
    {
        GameMgr.Instance.uiMgr.uiWindow.autoDecomposSelectPanel.gameObject.SetActive(true);
        GameMgr.Instance.uiMgr.uiWindow.autoDecomposSelectPanel.AutoComposInit();
    }

    public void AutoDecomPos(List<EquipItemSlot> slots)
    {
        foreach (var item in selectedSlot)
        {
            item.OnSelected(false);
        }
        selectedSlot.Clear();

        foreach (var item in slots)
        {
            item.OnSelected(true);
            selectedSlot.Add(item);
        }
        DecomposButtonUpdate();

    }
    public List<EquipItemSlot> GetSelectedItemCount(EquipType equipType, RarerityType rarerity)
    {
        List<EquipItemSlot> slots = new List<EquipItemSlot>();
        foreach (var item in equipItemSlots)
        {
            if (item.currentEquip.equipType == equipType)
            {
                if (item.currentEquip.rarerityType == rarerity)
                {
                    slots.Add(item);
                }
            }
        }
        return slots;
    }

    public void DecomposButtonUpdate()
    {
        if (selectedSlot.Count == 0)
        {
            confirmDecomposButton.interactable = false;
            confirmDecomposButton.GetComponentInChildren<TextMeshProUGUI>().text = "선택 완료";
        }
        else
        {
            confirmDecomposButton.interactable = true;
            confirmDecomposButton.GetComponentInChildren<TextMeshProUGUI>().text = "선택 완료\n(" + selectedSlot.Count + ")";
        }
    }

    public void SlotLevelUpdate()
    {
        hairSlot.SlotLevelUpdate();
        faceSlot.SlotLevelUpdate();
        clothSlot.SlotLevelUpdate();
        pantSlot.SlotLevelUpdate();
        weaponSlot.SlotLevelUpdate();
        cloakSlot.SlotLevelUpdate();
    }
    public void OpenEquipUpgradePanel()
    {
        GameMgr.Instance.uiMgr.uiWindow.equipUpgradePanel.gameObject.SetActive(true);
    }

    public void AutoEquip()
    {
        UnEquipAllSlot();
        int hairCP = 0;
        EquipItemSlot hair = null;
        int faceCP = 0;
        EquipItemSlot face = null;
        int clothCP = 0;
        EquipItemSlot cloth = null;
        int pantCP = 0;
        EquipItemSlot pant = null;
        int weaponCP = 0;
        EquipItemSlot weapon = null;
        int cloakCP = 0;
        EquipItemSlot cloak = null;

        foreach (var equip in equipItemSlots)
        {
            switch(equip.currentEquip.equipType)
            {
                case EquipType.Hair:
                    if (equip.currentEquip.CP > hairCP)
                        { 
                           hair = equip;
                           hairCP = equip.currentEquip.CP;
                        }
                    break;
                case EquipType.Face:
                    if (equip.currentEquip.CP > faceCP)
                    {
                        face = equip;
                        faceCP = equip.currentEquip.CP;
                    }
                    break;
                case EquipType.Cloth:
                    if (equip.currentEquip.CP > clothCP)
                    {
                        cloth = equip;
                        clothCP = equip.currentEquip.CP;
                    }
                    break;
                case EquipType.Pants:
                    if (equip.currentEquip.CP > pantCP)
                    {
                        pant = equip;
                        pantCP = equip.currentEquip.CP;
                    }
                    break;
                case EquipType.Weapon:
                    if (equip.currentEquip.CP > weaponCP)
                    {
                        weapon = equip;
                        weaponCP = equip.currentEquip.CP;
                    }
                    break;
                case EquipType.Cloak:
                    if (equip.currentEquip.CP > cloakCP)
                    {
                        cloak = equip;
                        cloakCP = equip.currentEquip.CP;
                    }
                    break;
            }
        }
        if (hair != null)
        {
            ChangeEquip(hair);
        }
        if (face != null)
        {
            ChangeEquip(face);
        }
        if (cloth != null)
        {
            ChangeEquip(cloth);
        }
        if (pant != null)
        {
            ChangeEquip(pant);
        }
        if (weapon != null)
        {
            ChangeEquip(weapon);
        }
        if (cloak != null)
        {
            ChangeEquip(cloak);
        }
    }
}
