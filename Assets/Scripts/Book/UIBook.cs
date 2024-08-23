using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBook : MonoBehaviour
{
    public SkillBook skillBookPrefab;
    public GameObject skillBookPanel;

    public GameObject skillBook;
    public GameObject equipBook;

    public EquipBookSet equipBookSetPrefab;

    public GameObject[] equipBookPanels;
    public GameObject[] equipBookContents;

    public ToggleGroup bookModes;
    public Toggle[] bookToggles;

    public ToggleGroup partSelectModes;
    public Toggle[] partSelectToggles;
    private int partSelectToggleNumber;


    public Dictionary<int, SkillBook> skillBookDic = new Dictionary<int, SkillBook>();
    public Dictionary<int, EquipBookElement> equipBookDic = new Dictionary<int, EquipBookElement>();

    public Dictionary<int, EquipBookSet> setDic = new Dictionary<int, EquipBookSet>();

    private void Awake()
    {
        foreach (Toggle toggle in partSelectToggles)
        {
            toggle.onValueChanged.AddListener(OnPartsToggleValueChanged);
        }
        foreach (Toggle toggle in bookToggles)
        {
            toggle.onValueChanged.AddListener(OnBookToggleValueChanged);
        }
        UpdateToggleColors();
    }
    public void Init()
    {
        var skilldata = GameMgr.Instance.playerMgr.playerInfo.skillBookDatas;

        foreach (var item in skilldata)
        {
            var newBook = Instantiate(skillBookPrefab, skillBookPanel.transform);
            newBook.saveData = item.Value;
            newBook.Init();
            skillBookDic.Add(item.Key, newBook);
            GameMgr.Instance.playerMgr.playerInfo.AcquiredUpdate(item.Key, item.Value.state);
        }

        var setData = DataTableMgr.Get<EquipBookTable>(DataTableIds.equipBook).equipBookDatas;
        foreach (var item in setData)
        {
            var equipSet = Instantiate(equipBookSetPrefab, equipBookContents[item.equipment_part-1].transform);
            equipSet.Init(item.equipbook_id);
            setDic.Add(item.equipbook_id, equipSet);
            equipSet.getReward = GameMgr.Instance.playerMgr.playerInfo.SetDatas[item.equipbook_id];
            equipSet.EquipSetCheck();
        }

        GameMgr.Instance.playerMgr.playerInfo.SetOptionUpdate();
    }

    private void UpdateToggleColors()
    {
        foreach (var toggle in partSelectToggles)
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

        foreach (var toggle in bookToggles)
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

    private void OnPartsToggleValueChanged(bool isOn)
    {
        UpdateToggleColors();
        for (int i = 0; i < partSelectToggles.Length; i++)
        {
            if (partSelectToggles[i].isOn)
            {
                partSelectToggleNumber = i;
                break;
            }
        }

        for(int i = 0;i < partSelectToggles.Length;i++)
        {
            equipBookPanels[i].gameObject.SetActive(false);
        }
        equipBookPanels[partSelectToggleNumber].gameObject.SetActive(true);
    }

    private void OnBookToggleValueChanged(bool isOn)
    {
        UpdateToggleColors();
        skillBook.gameObject.SetActive(bookToggles[0].isOn);
        equipBook.gameObject.SetActive(bookToggles[1].isOn);
    }
}
