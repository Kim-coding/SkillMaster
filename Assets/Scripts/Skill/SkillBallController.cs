using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public static class RectTransformExtensions
{
    public static bool Overlaps(this RectTransform rectTransform, RectTransform other)
    {
        Rect rect1 = GetWorldRect(rectTransform);
        Rect rect2 = GetWorldRect(other);
        return rect1.Overlaps(rect2);
    }

    private static Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];
        return new Rect(bottomLeft, topRight - bottomLeft);
    }
}


public class SkillBallController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public SkillSpawner skillSpawner;
    public GameObject mergeWindow;

    private int tier;
    private BigInteger attackPower;
    private float attackSpeed;
    private float effectX;
    private float effectY;
    public TextMeshProUGUI tierText;

    private RectTransform areaRect;   
    private bool isButtonPressed;

    private void Start()
    {
        areaRect = GetComponent<RectTransform>();
    }

    public void casing(SkillSpawner s)
    {
        skillSpawner = s;
        mergeWindow = s.mergeWindow;
    }

    public void Set(int t)
    {
        tier = t;
        tierText.text = t.ToString();
        //attackPower
        //attackSpeed
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isButtonPressed)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;

            Vector3 clampedPosition = ClampPositionInParent(mousePos);

            // areaRect의 위치 변경
            areaRect.position = clampedPosition;
        }
    }

    private Vector3 ClampPositionInParent(Vector3 position)
    {
        GameObject virtualObject = new GameObject("virtualObject");
        RectTransform virtualObjectRect = virtualObject.AddComponent<RectTransform>();
        virtualObjectRect.transform.SetParent(mergeWindow.GetComponent<RectTransform>(), false);

        virtualObjectRect.anchoredPosition = skillSpawner.minX;
        Vector3 localPointX = new Vector3(virtualObjectRect.position.x, virtualObjectRect.position.y, 0);

        virtualObjectRect.anchoredPosition = skillSpawner.maxY;
        Vector3 localPointY = new Vector3(virtualObjectRect.position.x, virtualObjectRect.position.y, 0);

        float clampedX = Mathf.Clamp(position.x, localPointX.x, localPointY.x);
        float clampedY = Mathf.Clamp(position.y, localPointX.y, localPointY.y);

        Debug.Log(position.x + "/" + localPointX.x + "/" + localPointY.x);

        return new Vector3(clampedX, clampedY, position.z);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MergeCheck();
        isButtonPressed = false;
    }


    private void MergeCheck()
    {
        if (!isButtonPressed) { return; }
        foreach (var other in GameMgr.Instance.playerMgr.skillBallControllers)
        {
            if(other.gameObject == gameObject || other == null || other.tier != tier)
            { continue; }
            if (gameObject.GetComponent<RectTransform>().Overlaps(other.GetComponent<RectTransform>()))
            {
                skillSpawner.MergeSkill(tier + 1, (gameObject.transform.position + other.gameObject.transform.position)/2);
                //합쳐지는 이펙트
                GameMgr.Instance.playerMgr.skillBallControllers.Remove(gameObject.GetComponent<SkillBallController>());
                GameMgr.Instance.playerMgr.skillBallControllers.Remove(other.gameObject.GetComponent<SkillBallController>());
                Destroy(gameObject);
                Destroy(other.gameObject);
                break;
            }
        }
    }
}

