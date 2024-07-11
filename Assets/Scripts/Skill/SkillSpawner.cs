using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillSpawner : MonoBehaviour
{
    public GameObject mergeWindow;
    public GameObject prefabSkillBall;

    [HideInInspector]
    public int maxSpawnCount;

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

    private void Start()
    {
        parentTransform = mergeWindow.transform.GetComponent<RectTransform>();
        maxSpawnCount = GameMgr.Instance.playerMgr.playerEnhance.maxSpawnCount;
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


        GameObject virtualObject = new GameObject("virtualObject");
        RectTransform virtualObjectRect = virtualObject.AddComponent<RectTransform>();
        virtualObjectRect.transform.SetParent(mergeWindow.GetComponent<RectTransform>(), false);

        virtualObjectRect.anchoredPosition = bottomLeft;
        localPointX = new Vector3(virtualObjectRect.position.x, virtualObjectRect.position.y, 0);
        virtualObjectRect.anchoredPosition = topRight;
        localPointY = new Vector3(virtualObjectRect.position.x, virtualObjectRect.position.y, 0);
    }


    public void SpawnSkill()
    {
        if(GameMgr.Instance.playerMgr.skillBallControllers.Count == maxSpawnCount)
        {
            return;
        }

        var newSkill = Instantiate(prefabSkillBall, parentTransform);

        var rt = newSkill.GetComponent<RectTransform>();
        rt.anchoredPosition = RandomVector();

        var newSkillControler = newSkill.GetComponent<SkillBallController>();
        newSkillControler.Set(1);
        GameMgr.Instance.playerMgr.skillBallControllers.Add(newSkillControler);
        GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();
    }

    public void MergeSkill(int t, Vector3 pos)
    {
        var newSkill = Instantiate(prefabSkillBall, pos, Quaternion.identity, parentTransform);
        var newSkillControler = newSkill.GetComponent<SkillBallController>();
        newSkillControler.Set(t);
        GameMgr.Instance.playerMgr.skillBallControllers.Add(newSkillControler);
    }

    private Vector3 RandomVector()
    {
        Setting();
        float randomX = Random.Range(bottomLeftOffset.x + bottomLeftDesiredPosition.x, topRightOffset.x + topRightDesiredPosition.x);
        float randomY = Random.Range(bottomLeftOffset.y + bottomLeftDesiredPosition.y, topRightOffset.y + topRightDesiredPosition.y);
        return new Vector3(randomX, randomY, 0);
    }
}
