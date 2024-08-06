using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class AutoDecomposPanel : MonoBehaviour
{
    public Toggle[] ItemTypeToggls;
    public Toggle[] ItemRarityToggls;

    private bool allTypeOff = true;
    private bool allrarityOff = true;

    public Button SelectButton;
    public Button CancleButton;

    private int selectedEquipCount;
    List<EquipItemSlot> autoSelectSlots = new List<EquipItemSlot>();

    public void AutoComposInit()
    {
        for (int i = 0; i < ItemTypeToggls.Length; i++)
        {
            ItemTypeToggls[i].isOn = false;
        }

        for (int i = 0; i < ItemRarityToggls.Length; i++)
        {
            ItemRarityToggls[i].isOn = false;
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        allTypeOff = true;
        allrarityOff = true;
        for (int i = 0; i < ItemTypeToggls.Length; i++)
        {
            if (ItemTypeToggls[i].isOn)
            {
                allTypeOff = false;
            }
        }

        for (int i = 0; i < ItemRarityToggls.Length; i++)
        {
            if (ItemRarityToggls[i].isOn)
            {
                allrarityOff = false;
            }
        }

        CheckToggleValue();
    }

    private void CheckToggleValue()
    {
        autoSelectSlots.Clear();

        if (allTypeOff && allrarityOff)
        {
            SelectButtonUpdate();
            return;
        }

        if (allTypeOff && !allrarityOff)
        {
            for (int i = 0; i < ItemRarityToggls.Length; i++)
            {
                if (ItemRarityToggls[i].isOn)
                {
                    for (int j = 0; j < ItemTypeToggls.Length; j++)
                    {
                        List<EquipItemSlot> slots = GameMgr.Instance.uiMgr.uiInventory.GetSelectedItemCount((EquipType)(j + 1), (RarerityType)(i + 1));
                        foreach(EquipItemSlot slot in slots)
                        {
                            autoSelectSlots.Add(slot);
                        }
                    }
                }
            }
            SelectButtonUpdate();
            return;
        }


        if (!allTypeOff && allrarityOff)
        {
            for (int i = 0; i < ItemTypeToggls.Length; i++)
            {
                if (ItemTypeToggls[i].isOn)
                {
                    for (int j = 0; j < ItemRarityToggls.Length; j++)
                    {
                        List<EquipItemSlot> slots = GameMgr.Instance.uiMgr.uiInventory.GetSelectedItemCount((EquipType)(i + 1), (RarerityType)(j + 1));
                        foreach (EquipItemSlot slot in slots)
                        {
                            autoSelectSlots.Add(slot);
                        }
                    }
                }
            }
            SelectButtonUpdate();
            return;
        }

        for (int i = 0; i < ItemTypeToggls.Length; i++)
        {
            if (ItemTypeToggls[i].isOn)
            {
                for (int j = 0; j < ItemRarityToggls.Length; j++)
                {
                    if (ItemRarityToggls[j].isOn)
                    {
                        List<EquipItemSlot> slots = GameMgr.Instance.uiMgr.uiInventory.GetSelectedItemCount((EquipType)(i + 1), (RarerityType)(j + 1));
                        foreach (EquipItemSlot slot in slots)
                        {
                            autoSelectSlots.Add(slot);
                        }
                    }
                }
            }
        }
        SelectButtonUpdate();
    }



    private void Awake()
    {
        foreach (Toggle toggle in ItemTypeToggls)
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
        foreach (Toggle toggle in ItemRarityToggls)
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        SelectButton.onClick.AddListener(SelectConfirm);
        CancleButton.onClick.AddListener(ClosePanel);
    }

    public void SelectButtonUpdate()
    {
        selectedEquipCount = autoSelectSlots.Count;
        var buttonText = SelectButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = $"º±≈√({selectedEquipCount})";
    }

    public void SelectConfirm()
    {
        GameMgr.Instance.uiMgr.uiInventory.AutoDecomPos(autoSelectSlots);
        ClosePanel();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

}
