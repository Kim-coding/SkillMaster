using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public enum ClearState
{
    NotAcquired = 0,
    Acquired,
    RewardAcquired,
}

public class SkillBookSaveData
{
    public int skillID { get; set; }
    public ClearState state { get; set; }

}

public class SkillBook : MonoBehaviour
{
    public SkillBookSaveData saveData = new SkillBookSaveData();

    public Image icon;
    public Image reward;
    public Image blind;
    public TextMeshProUGUI lvText;
    public TextMeshProUGUI rewardText;

    private int rewardValue = 0;


    public void Init()
    {
        var skillData = DataTableMgr.Get<SkillTable>(DataTableIds.skill).GetID(saveData.skillID);
        if(skillData == null )
        {
            return;
        }
        LoadSkillIcon(skillData.Skillicon);
        lvText.text = skillData.SkillLv + " ·¹º§";

        rewardValue = DataTableMgr.Get<SkillBookTable>(DataTableIds.skillBook).GetID(saveData.skillID).dia_rewardvalue;
        reward.GetComponent<Button>().onClick.AddListener(GetReward);
        icon.GetComponent<Button>().onClick.AddListener(() => { GameMgr.Instance.uiMgr.uiWindow.skillBookPanel.SetSkillBookPanel(saveData.skillID); });
        blind.GetComponent<Button>().onClick.AddListener(() => { GameMgr.Instance.uiMgr.uiWindow.skillBookPanel.SetSkillBookPanel(saveData.skillID); });
    }

    public void AcquiredCheck()
    {
        if (saveData.state == ClearState.NotAcquired)
        {
            reward.gameObject.SetActive(false);
            blind.gameObject.SetActive(true);
        }
        if (saveData.state == ClearState.Acquired)
        {
            reward.gameObject.SetActive(true);
            rewardText.text = rewardValue.ToString();
            blind.gameObject.SetActive(false);
        }
        if (saveData.state == ClearState.RewardAcquired)
        {
            reward.gameObject.SetActive(false);
            blind.gameObject.SetActive(false);
        }
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

    private void GetReward()
    {
        GameMgr.Instance.playerMgr.currency.AddDia(new BigInteger(rewardValue));
        saveData.state = ClearState.RewardAcquired;
        AcquiredCheck();
        //GameMgr.Instance.playerMgr.playerInfo.skillBookDatas[saveData.skillID].state = saveData.state;

        GameMgr.Instance.uiMgr.uiBook.OnRewardCollected();
    }
}
