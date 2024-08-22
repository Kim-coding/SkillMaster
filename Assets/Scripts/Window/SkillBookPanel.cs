using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SkillBookPanel : MonoBehaviour
{
    public Image icon;
    public Image screenShot;

    public Button exit;
    public TextMeshProUGUI skillLv;
    public TextMeshProUGUI skillInfo;


    public void SetSkillBookPanel(int SkillLv)
    {
        gameObject.SetActive(true);
        var skillData = DataTableMgr.Get<SkillTable>(DataTableIds.skill).GetID(SkillLv);
        LoadSkillIcon(skillData.Skillicon);
        skillLv.text = skillData.SkillLv + " ·¹º§";
        var skillBookData = DataTableMgr.Get<SkillBookTable>(DataTableIds.skillBook).GetID(SkillLv);
        skillInfo.text = DataTableMgr.Get<StringTable>(DataTableIds.String).GetID(skillBookData.text_id);
        screenShot.sprite = Resources.Load<Sprite>($"skillscreenshot/{skillBookData.image}");
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }


    private void LoadSkillIcon(string iconName)
    {
        string address = $"SkillIcon/{iconName}";
        Addressables.LoadAssetAsync<Sprite>(address).Completed += OnLoadIcon;
    }

    void OnLoadIcon(AsyncOperationHandle<Sprite> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            icon.sprite = obj.Result;
        }
        else
        {
            Debug.LogError($"Failed to load icon: {obj.OperationException}");
        }
    }

}
