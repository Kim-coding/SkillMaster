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
    public Button autoSpawnButton;

    public TextMeshProUGUI autoMergeButtonText;

    public GameObject merge;

    private bool isAutoMerging = false;
    private float timer = 0f;
    public float autoMergeDuration;
    public float baseAutoMergeDuration;

    public GameObject[] autoLockImages;

    private void Start()
    {
        sortButton.onClick.AddListener(SortingSkills);
        autoMergeButton.onClick.AddListener(ToggleAutoMerge);
        autoMergeButtonText = autoMergeButton.GetComponentInChildren<TextMeshProUGUI>();

        if(GameMgr.Instance.sceneMgr.mainScene != null && GameMgr.Instance.uiMgr.uiGuideQuest.currentQuest.QuestID < 60066)
        {
            autoMergeButton.interactable = false;
            autoSpawnButton.interactable = false;
            foreach (var lockimage in autoLockImages)
            {
                lockimage.SetActive(true);
            }

        }
    }

    public void UnLockAutoButton()
    {
        if (GameMgr.Instance.uiMgr.uiGuideQuest.currentQuest.QuestID == 60066)
        {
            autoMergeButton.interactable = true;
            autoSpawnButton.interactable = true;
            foreach (var lockimage in autoLockImages)
            {
                lockimage.SetActive(false);
            }

        }
    }

    public void SetBase(float duration)
    {
        baseAutoMergeDuration = duration;
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
        if(isAutoMerging)
        {
            isAutoMerging = !isAutoMerging;
            autoMergeButtonText.text = "수동 조합";
        }
        else
        {
            isAutoMerging = !isAutoMerging;
            autoMergeButtonText.text = "자동 조합";
        }
    }

    public void SetAutoMergeDuration(float duration)
    {
        autoMergeDuration = baseAutoMergeDuration - duration;
    }

    private void Update()
    {
        if(isAutoMerging)
        {
            timer += Time.deltaTime;
            if(timer > autoMergeDuration)
            {
                AutoMerge();
                timer = 0f;
            }
        }
    }

    private void AutoMerge()
    {
        List<SkillBallController> skillBalls = GameMgr.Instance.playerMgr.skillBallControllers;
        
        skillBalls.Sort((a, b) => a.tier.CompareTo(b.tier));

        for ( int i = 0; i < skillBalls.Count - 1; i++ )
        {
            if (skillBalls[i].tier == skillBalls[i + 1].tier && !skillBalls[i].isMove && !skillBalls[i + 1].isMove && skillBalls[i].tier != GameMgr.Instance.playerMgr.playerInfo.maxSkillLevel)
            {
                MoveAndMerge(skillBalls[i], skillBalls[i + 1]);
                skillBalls[i].isMove = true;
                skillBalls[i + 1].isMove = true;
                break;
            }
        }
    }

    private void MoveAndMerge(SkillBallController skillBall1, SkillBallController skillBall2)
    {
        SkillBallMovement movement1 = skillBall1.GetComponent<SkillBallMovement>();
        SkillBallMovement movement2 = skillBall2.GetComponent<SkillBallMovement>();

        if ( movement1 == null || movement2 == null )
        {
            return;
        }

        var rectTransform1 = skillBall1.GetComponent<RectTransform>();
        var rectTransform2 = skillBall2.GetComponent<RectTransform>();

        var startPosition1 = rectTransform1.anchoredPosition;
        var startPosition2 = rectTransform2.anchoredPosition; 
        var endPosition = (startPosition1 + startPosition2)/2;

        float moveDuration = 1f;

        movement1.Move(startPosition1,endPosition,moveDuration);
        movement2.Move(startPosition2,endPosition,moveDuration);
    }
}
