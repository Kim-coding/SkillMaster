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

    public TextMeshProUGUI autoMergeButtonText;

    public GameObject merge;

    private bool isAutoMerging = false;
    private float timer = 0f;
    private float duration = 3f;

    private void Start()
    {
        sortButton.onClick.AddListener(SortingSkills);
        autoMergeButton.onClick.AddListener(ToggleAutoMerge);
        autoMergeButtonText = autoMergeButton.GetComponentInChildren<TextMeshProUGUI>();
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

    private void Update()
    {
        if(isAutoMerging)
        {
            timer += Time.deltaTime;
            if(timer > duration)
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
            if (skillBalls[i].tier == skillBalls[i + 1].tier && !skillBalls[i].isMove && !skillBalls[i + 1].isMove)
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

        var startPosition1 = skillBall1.transform.position;
        var startPosition2 = skillBall2.transform.position;
        var endPosition = (startPosition1 + startPosition2)/2;

        float moveDuration = 1f;

        movement1.Move(startPosition1,endPosition,moveDuration);
        movement2.Move(startPosition2,endPosition,moveDuration);
    }
}
