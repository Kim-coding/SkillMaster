using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillSpawner : MonoBehaviour
{
    public GameObject mergeWindow;
    public GameObject prefabSkillBall;

    [HideInInspector]
    public int maxReserveSkillCount;

    [HideInInspector]
    public int maxSpawnSkillCount;
    [HideInInspector]
    public int currentSpawnSkillCount;

    [HideInInspector]
    public RectTransform parentTransform;
    [HideInInspector]
    public RectTransform rect;

    [HideInInspector]
    public Vector3 bottomLeft;
    [HideInInspector]
    public Vector3 topRight;

    [HideInInspector]
    public Vector3 localPointX;
    [HideInInspector]
    public Vector3 localPointY;

    Vector2 parentSize;

    Vector2 bottomLeftDesiredPosition;
    Vector2 topRightDesiredPosition;
    Vector2 bottomLeftOffset;
    Vector2 topRightOffset;

    public Button autoSkillSpownButton;
    public TextMeshProUGUI autoSkillSpownButtonText;
    private bool autoSpawn = false;
    private float spawnTiemr = 0f;
    public float spawnduration;
    public float baseSpawnduration = 2f;

    private PlayerMgr playerMgr;
    private GameObject virtualObject;
    public TMP_InputField skillLevelInputField; // InputField 연결
    public int skillLV = 40001;

    private void Start()
    {
        playerMgr = GameMgr.Instance.playerMgr;
        parentTransform = mergeWindow.transform.GetComponent<RectTransform>();
        maxReserveSkillCount = playerMgr.playerEnhance.maxReserveSkillCount;
        Canvas.ForceUpdateCanvases();
        Setting();

        if (SaveLoadSystem.CurrSaveData.savePlay != null)
        {
            GameMgr.Instance.uiMgr.uiMerge.skillSpawner.LoadSkill(SaveLoadSystem.CurrSaveData.savePlay.saveSkillBallControllers);
        }
        else
        {
            SpawnSkill(1);
        }

        Destroy(virtualObject);
        autoSkillSpownButton.onClick.AddListener(AutoSkillSpawn);
        autoSkillSpownButtonText = autoSkillSpownButton.GetComponentInChildren<TextMeshProUGUI>();
        if(skillLevelInputField != null)
        {
            skillLevelInputField.onEndEdit.AddListener(OnSkillLevelInputFieldEndEdit);
        }

        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();
        //AutoSkillSpawn();
    }

    private void Update()
    {
        if(autoSpawn)
        {
            spawnTiemr += Time.deltaTime;
            if(spawnTiemr > spawnduration)
            {
                spawnTiemr = 0f;
                SpawnSkill();
            }
        }
    }

    public void SetSpawnDuration(float duration)
    {
        spawnduration = baseSpawnduration - duration;
    }

    public void SetBase(float duration)
    {
        baseSpawnduration = duration;
    }

    public void AutoSkillSpawn()
    {
        if(autoSpawn)
        {
            autoSpawn = false;
            autoSkillSpownButtonText.text = "자동 생성 \n <color=#ff0000>OFF</color>";
        }
        else
        {
            autoSpawn = true;
            autoSkillSpownButtonText.text = "자동 생성 \n <color=#0000ff>ON</color>";
        }
    }

    private void Setting()
    {
        rect = prefabSkillBall.GetComponent<RectTransform>();
        float uiWidth = rect.rect.width;
        float uiHeight = rect.rect.height;

        parentSize = new Vector2(parentTransform.rect.width, parentTransform.rect.height);

        bottomLeftDesiredPosition = new Vector2(uiWidth * 0.5f, uiHeight * 0.5f);
        topRightDesiredPosition = new Vector2(-uiWidth * 0.5f, -uiHeight * 0.5f);
        bottomLeftOffset = new Vector2(-parentSize.x * 0.5f, -parentSize.y * 0.5f);
        topRightOffset = new Vector2(parentSize.x * 0.5f, parentSize.y * 0.5f);

        bottomLeft = new Vector3(bottomLeftOffset.x + bottomLeftDesiredPosition.x, bottomLeftOffset.y + bottomLeftDesiredPosition.y);
        topRight = new Vector3(topRightOffset.x + topRightDesiredPosition.x, topRightOffset.y + topRightDesiredPosition.y);


        virtualObject = new GameObject("virtualObject");
        RectTransform virtualObjectRect = virtualObject.AddComponent<RectTransform>();
        virtualObjectRect.transform.SetParent(mergeWindow.GetComponent<RectTransform>(), false);

        virtualObjectRect.anchoredPosition = bottomLeft;
        localPointX = new Vector3(virtualObjectRect.position.x, virtualObjectRect.position.y, 0);
        virtualObjectRect.anchoredPosition = topRight;
        localPointY = new Vector3(virtualObjectRect.position.x, virtualObjectRect.position.y, 0);
    }

    public void LoadSkill(List<SkillBallController> list)
    { 

        foreach (SkillBallController skillBallController in list)
        {
            var newSkill = Instantiate(prefabSkillBall, parentTransform);
            var rt = newSkill.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(skillBallController.anchoredPos.x, skillBallController.anchoredPos.y , 0);
            var newSkillControler = newSkill.GetComponent<SkillBallController>();
            newSkillControler.Set(skillBallController.skill_ID);
            playerMgr.skillBallControllers.Add(newSkillControler);
        }

        playerMgr.playerSkills.SetList(); // 캐스팅 리스트 업데이트

    }



    public void SpawnSkill(int skillId = -1)
    {
        if (playerMgr.skillBallControllers.Count == maxReserveSkillCount || playerMgr.playerEnhance.currentSpawnSkillCount <= 0)
        {
            return;
        }

        if(GameMgr.Instance.uiMgr.uiTutorial != null && GameMgr.Instance.uiMgr.uiTutorial.gameObject.activeSelf)
        {
            skillId = 1; // 튜토리얼 중에는 1번 스킬만 들어가야 한다.
        }

        if (playerMgr.skillBallControllers.Count != 0)
        { 
            GameMgr.Instance.soundMgr.PlaySFX("Button"); 
        }
        var newSkill = Instantiate(prefabSkillBall, parentTransform);

        var rt = newSkill.GetComponent<RectTransform>();
        rt.anchoredPosition = RandomVector();

        var newSkillControler = newSkill.GetComponent<SkillBallController>();
        if (skillId == -1)
        {
            newSkillControler.Set(SelectedSkill());
        }
        else
        {
            if(!newSkillControler.Set(skillId + 40000))
            {
                Destroy(newSkill.gameObject);
                return;
            }
        }
        playerMgr.skillBallControllers.Add(newSkillControler);
        playerMgr.playerInfo.MaxSkillLevelUpdate(newSkillControler.tier);
        playerMgr.playerEnhance.currentSpawnSkillCount--;
        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();
        playerMgr.playerSkills.SetList(); // 캐스팅 리스트 업데이트
        if (playerMgr.skillBallControllers.Count == maxReserveSkillCount || playerMgr.playerEnhance.currentSpawnSkillCount <= 0)
        { GameMgr.Instance.uiMgr.uiMerge.SpawnButtonUpdate(false); }
    }

    public void MergeSkill(int skill_ID, Vector3 pos, int t)
    {
        var newSkill = Instantiate(prefabSkillBall, pos, Quaternion.identity, parentTransform);
        var newSkillControler = newSkill.GetComponent<SkillBallController>();
        newSkillControler.Set(skill_ID);
        playerMgr.skillBallControllers.Add(newSkillControler);

        playerMgr.playerSkills.SetList(); // 캐스팅 리스트 업데이트
        GameMgr.Instance.uiMgr.uiMerge.SpawnButtonUpdate(true);
        if(GameMgr.Instance.uiMgr.uiGuideQuest != null)
        {
            EventMgr.TriggerEvent(QuestType.MergeSkillCount);
        }
        playerMgr.playerInfo.MaxSkillLevelUpdate(t);
        GameMgr.Instance.uiMgr.uiBook.OnRewardCollected();
    }

    private Vector3 RandomVector()
    {
        float randomX = Random.Range(bottomLeftOffset.x + bottomLeftDesiredPosition.x, topRightOffset.x + topRightDesiredPosition.x);
        float randomY = Random.Range(bottomLeftOffset.y + bottomLeftDesiredPosition.y, topRightOffset.y + topRightDesiredPosition.y);
        return new Vector3(randomX, randomY, 0);
    }
    private void OnSkillLevelInputFieldEndEdit(string input)
    {
        if (int.TryParse(input, out int level))
        {
            skillLV = 40000 + level;
            Debug.Log("Skill Level set to: " + skillLV);
        }
        else
        {
            Debug.LogError("Invalid input for skill level.");
        }
    }

    public void maxReserveSkillCountUpdate()
    {
        maxReserveSkillCount = playerMgr.playerEnhance.maxReserveSkillCount;
    }

    private int SelectedSkill()
    {
        var skillSummonData = DataTableMgr.Get<SkillSummonTable>(DataTableIds.skillSummon).GetID(GameMgr.Instance.playerMgr.playerEnhance.cbnUpgradeLv);

        int skill1Lv = AdjustSkillLevel(skillSummonData.skill1Lv);
        int skill2Lv = AdjustSkillLevel(skillSummonData.skill2Lv);
        int skill3Lv = AdjustSkillLevel(skillSummonData.skill3Lv);
        int skill4Lv = AdjustSkillLevel(skillSummonData.skill4Lv);
        float skill1per = skillSummonData.skill1per;
        float skill2per = skillSummonData.skill2per;
        float skill3per = skillSummonData.skill3per;
        float skill4per = skillSummonData.skill4per;

        float totalProbability = skill1per + skill2per + skill3per + skill4per;
        float randomValue = Random.Range(0, totalProbability);

        if (randomValue < skill1per)
        {
            return skill1Lv;
        }
        else if (randomValue < skill1per + skill2per)
        {
            return skill2Lv;
        }
        else if (randomValue < skill1per + skill2per + skill3per)
        {
            return skill3Lv;
        }
        else
        {
            return skill4Lv;
        }
    }
    private int AdjustSkillLevel(int skillLevel)
    {
        return skillLevel + 40000;
    }
}
