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
    private float spawnduration = 1f;

    private PlayerMgr playerMgr;
    private GameObject virtualObject;
    private void Start()
    {
        playerMgr = GameMgr.Instance.playerMgr;
        parentTransform = mergeWindow.transform.GetComponent<RectTransform>();
        maxReserveSkillCount = playerMgr.playerEnhance.maxReserveSkillCount;
        Canvas.ForceUpdateCanvases();
        Setting();
        if(playerMgr.skillBallControllers.Count == 0)
        {
            SpawnSkill();
            Destroy(virtualObject);
        }
        autoSkillSpownButton.onClick.AddListener(AutoSkillSpawn);
        autoSkillSpownButtonText = autoSkillSpownButton.GetComponentInChildren<TextMeshProUGUI>();
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

    public void AutoSkillSpawn()
    {
        if(autoSpawn)
        {
            autoSpawn = false;
            autoSkillSpownButtonText.text = "荐悼 积己";
        }
        else
        {
            autoSpawn = true;
            autoSkillSpownButtonText.text = "磊悼 积己";
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


    public void SpawnSkill()
    {
        if (playerMgr.skillBallControllers.Count == maxReserveSkillCount || playerMgr.playerEnhance.currentSpawnSkillCount <= 0)
        {
            return;
        }
        if (playerMgr.skillBallControllers.Count != 0)
        { 
            GameMgr.Instance.soundMgr.PlaySFX("Button"); 
        }
        var newSkill = Instantiate(prefabSkillBall, parentTransform);

        var rt = newSkill.GetComponent<RectTransform>();
        rt.anchoredPosition = RandomVector();

        var newSkillControler = newSkill.GetComponent<SkillBallController>();
        newSkillControler.Set(40001);
        playerMgr.skillBallControllers.Add(newSkillControler);
        playerMgr.playerEnhance.currentSpawnSkillCount--;
        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();
        if (playerMgr.skillBallControllers.Count == maxReserveSkillCount || playerMgr.playerEnhance.currentSpawnSkillCount <= 0)
        { GameMgr.Instance.uiMgr.uiMerge.SpawnButtonUpdate(false); }
    }

    public void MergeSkill(int skill_ID, Vector3 pos, int t)
    {
        var newSkill = Instantiate(prefabSkillBall, pos, Quaternion.identity, parentTransform);
        var newSkillControler = newSkill.GetComponent<SkillBallController>();
        newSkillControler.Set(skill_ID);
        playerMgr.skillBallControllers.Add(newSkillControler);

        GameMgr.Instance.uiMgr.uiMerge.SpawnButtonUpdate(true);

        EventMgr.TriggerEvent(QuestType.MergeSkillCount);
        playerMgr.playerInfo.MaxSkillLevelUpdate(t);
    }

    private Vector3 RandomVector()
    {
        float randomX = Random.Range(bottomLeftOffset.x + bottomLeftDesiredPosition.x, topRightOffset.x + topRightDesiredPosition.x);
        float randomY = Random.Range(bottomLeftOffset.y + bottomLeftDesiredPosition.y, topRightOffset.y + topRightDesiredPosition.y);
        return new Vector3(randomX, randomY, 0);
    }
}
