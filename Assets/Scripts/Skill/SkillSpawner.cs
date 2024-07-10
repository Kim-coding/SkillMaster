using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillSpawner : MonoBehaviour
{
    public GameObject mergeWindow;
    public GameObject prefabSkillBall;
    public Transform parentTransform;

    public Vector3 minX;
    public Vector3 maxY;

    private void Start()
    {
        parentTransform = mergeWindow.transform.GetComponent<RectTransform>();

    }
    public void SpawnSkill()
    {

        var newSkill = Instantiate(prefabSkillBall, parentTransform);

        var rt = newSkill.GetComponent<RectTransform>();
        rt.anchoredPosition = RandomVector() ;

        var newSkillControler = newSkill.GetComponent<SkillBallController>();
        newSkillControler.casing(this);
        newSkillControler.Set(1);
        GameMgr.Instance.playerMgr.skillBallControllers.Add(newSkillControler);
    }

    public void MergeSkill(int t, Vector3 pos)
    {
        var newSkill = Instantiate(prefabSkillBall, pos, Quaternion.identity, parentTransform);
        var newSkillControler = newSkill.GetComponent<SkillBallController>();
        newSkillControler.casing(this);
        newSkillControler.Set(t);
        GameMgr.Instance.playerMgr.skillBallControllers.Add(newSkillControler);
    }

    private Vector3 RandomVector()
    {

        var rect = prefabSkillBall.GetComponent<RectTransform>();
        RectTransform parentRectTransform = mergeWindow.transform.parent.GetComponent<RectTransform>();
        float uiWidth = rect.rect.width;
        float uiHeight = rect.rect.height;

        Vector2 parentSize = new Vector2(parentRectTransform.rect.width, parentRectTransform.rect.height);

        Vector2 bottomLeftDesiredPosition = new Vector2(uiWidth * 0.5f, uiHeight * 0.5f);
        Vector2 topRightDesiredPosition = new Vector2(-uiWidth * 0.5f, -uiHeight * 0.5f);

        Vector2 parentBottomLeft = new Vector2(-parentSize.x * 0.5f, -parentSize.y * 0.5f);
        Vector2 parentTopRight = new Vector2(parentSize.x * 0.5f, parentSize.y * 0.5f);

        minX = new Vector3(parentBottomLeft.x + bottomLeftDesiredPosition.x, parentBottomLeft.y + bottomLeftDesiredPosition.y);
        maxY = new Vector3(parentTopRight.x + topRightDesiredPosition.x, parentTopRight.y + topRightDesiredPosition.y);

        float randomX = Random.Range(parentBottomLeft.x + bottomLeftDesiredPosition.x, parentTopRight.x + topRightDesiredPosition.x);
        float randomY = Random.Range(parentBottomLeft.y + bottomLeftDesiredPosition.y, parentTopRight.y + topRightDesiredPosition.y);

        return new Vector3(randomX, randomY, 0);
    }
}
