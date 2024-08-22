using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBook : MonoBehaviour
{
    public SkillBook skillBookPrefab;
    public GameObject skillBookPanel;

    public EquipBookSet equipBookSetPrefab;
    public GameObject equipBoolHairPanel;
    public GameObject equipBoolFacePanel;
    public GameObject equipBoolClothPanel;
    public GameObject equipBoolPantsPanel;
    public GameObject equipBoolWeaponPanel;
    public GameObject equipBoolCloakPanel;

    public Dictionary<int, SkillBook> skillBookDic = new Dictionary<int, SkillBook>();
    //public Dictionary<int, EquipBookElement> equipBookDic = new Dictionary<int, EquipBookElement>();


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
            switch (item.equipment_part)
            {
                case 1:
                    var hairSet = Instantiate(equipBookSetPrefab, equipBoolHairPanel.transform);
                    hairSet.Init(item.equipbook_id);
                    break;
                case 2:
                    var faseSet = Instantiate(equipBookSetPrefab, equipBoolFacePanel.transform);
                    faseSet.Init(item.equipbook_id);
                    break;
                case 3:
                    var clothSet = Instantiate(equipBookSetPrefab, equipBoolClothPanel.transform);
                    clothSet.Init(item.equipbook_id);
                    break;
                case 4:
                    var pantSet = Instantiate(equipBookSetPrefab, equipBoolPantsPanel.transform);
                    pantSet.Init(item.equipbook_id);
                    break;
                case 5:
                    var weaponSet = Instantiate(equipBookSetPrefab, equipBoolWeaponPanel.transform);
                    weaponSet.Init(item.equipbook_id);
                    break;
                case 6:
                    var cloakSet = Instantiate(equipBookSetPrefab, equipBoolCloakPanel.transform);
                    cloakSet.Init(item.equipbook_id);
                    break;
            }

        }


    }
}
