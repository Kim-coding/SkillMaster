using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiMerge : MonoBehaviour
{
    public SkillSpawner skillSpawner;
    public TextMeshProUGUI skillReserveCount;
    public TextMeshProUGUI skillSpawnCount;

    public Button spawnButton;
    public Button sortButton;

    private void Start()
    {
        sortButton.onClick.AddListener(SortingSkills);
    }

    public void SkillCountUpdate()
    {
        skillReserveCount.text = "보유 (" + GameMgr.Instance.playerMgr.skillBallControllers.Count + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxReserveSkillCount + ")";
        skillSpawnCount.text = "소환\n (" + GameMgr.Instance.playerMgr.playerEnhance.currentSpawnSkillCount + "/" + GameMgr.Instance.playerMgr.playerEnhance.maxSpawnSkillCount + ")";
    }

    public void SpawnButtonUpdate(bool a)
    {
        spawnButton.interactable = a;
    }

    private void SortingSkills()
    {
        var sortSkillBall = gameObject.GetComponent<SortSkillBall>();
        if(sortSkillBall != null )
        {
            sortSkillBall.Sort();
        }
    }

}
