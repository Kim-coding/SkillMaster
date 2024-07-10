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

            // ���콺 ��ġ�� �θ��� ���� ��ǥ�� ��ȯ
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(mergeWindow.GetComponent<RectTransform>(), mousePos, Camera.main, out localPoint);

            // ���� ��ǥ�� Ŭ����
            Vector3 clampedPosition = ClampPositionInParent(localPoint);

            // areaRect�� ��ġ ����
            areaRect.localPosition = clampedPosition;
        }
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mergeWindow.GetComponent<RectTransform>(), mousePos, Camera.main, out localPoint);

        Debug.Log(mousePos.x + "/" + localPoint.x);

    }



    private Vector3 ClampPositionInParent(Vector3 position)
    {
        float clampedX = Mathf.Clamp(position.x, skillSpawner.minX.x, skillSpawner.maxY.x);

        Debug.Log(position.x);
        Debug.Log(skillSpawner.minX.x);

        float clampedY = Mathf.Clamp(position.y, skillSpawner.minX.y, skillSpawner.maxY.y);

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
                //�������� ����Ʈ
                GameMgr.Instance.playerMgr.skillBallControllers.Remove(gameObject.GetComponent<SkillBallController>());
                GameMgr.Instance.playerMgr.skillBallControllers.Remove(other.gameObject.GetComponent<SkillBallController>());
                Destroy(gameObject);
                Destroy(other.gameObject);
                break;
            }
        }
    }
}

