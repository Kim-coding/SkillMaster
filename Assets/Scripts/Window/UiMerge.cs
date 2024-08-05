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
    public Button autoMergeButton;

    public GameObject merge;

    private bool isAutoMerging = false;
    private float timer = 0f;
    private float duration = 1f;

    private void Start()
    {
        sortButton.onClick.AddListener(SortingSkills);
        autoMergeButton.onClick.AddListener(ToggleAutoMerge);
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
        SortSkillBall sortSkillBall = merge.gameObject.GetComponent<SortSkillBall>();
        if(sortSkillBall != null )
        {
            sortSkillBall.Sort();
        }
    }

    private void ToggleAutoMerge()
    {
        isAutoMerging = !isAutoMerging;
    }

    private void Update()
    {
        if(isAutoMerging)
        {
            timer += Time.deltaTime;
            if(timer > duration)
            {
                AutoMerge();
            }
        }
    }

    private void AutoMerge()
    {
        List<SkillBallController> skillBalls = GameMgr.Instance.playerMgr.skillBallControllers;
        
        skillBalls.Sort((a, b) => a.tier.CompareTo(b.tier));
    }
}
