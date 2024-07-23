using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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
    private SkillSpawner skillSpawner;
    [HideInInspector]
    public GameObject mergeWindow;

    private int tier;
    
    private float effectX;
    private float effectY;
    public TextMeshProUGUI tierText;
    public Transform pos;

    private RectTransform areaRect;   
    private bool isButtonPressed;

    public int skill_ID;
    public float attackSpeed;
    public BigInteger skillDamage;
    public int skillType;
    public float damageColdown;
    public float skillArange;
    public float atkArangeX;
    public float atkArangeY;
    public int skillPropertyID;
    public string SkillEffect;
    public string Skillicon;

    public Image skillIconImage;

    private void Start()
    {
        areaRect = GetComponent<RectTransform>();
        skillSpawner = GameMgr.Instance.uiMgr.uiMerge.skillSpawner;
        mergeWindow = skillSpawner.mergeWindow;
        skillDamage = new BigInteger();
    }

    public void Set(int skill_ID)
    {
        this.skill_ID = skill_ID;
        var skillTable = DataTableMgr.Get<SkillTable>(DataTableIds.skill);

        var skillData = skillTable.GetID(skill_ID);
        if (skillData != null)
        {
            tier = skillData.SkillLv;
            skillPropertyID = skillData.SkillPropertyID;
            attackSpeed = skillData.AttackSpeed;
            skillType = skillData.Type;
            skillDamage = new BigInteger(skillData.Skill_damage); 
            damageColdown = skillData.DamageColdown;
            skillArange = skillData.SkillArange;
            atkArangeX = skillData.AtkArangeX;
            atkArangeY = skillData.AtkArangeY;
            SkillEffect = skillData.SkillEffect;
            Skillicon = skillData.Skillicon;

            tierText.text = tier.ToString();
        }

        Sprite iconSprite = Resources.Load<Sprite>($"SkillIcon/{Skillicon}");
        if (iconSprite != null)
        {
            skillIconImage.sprite = iconSprite;
        }
        else
        {
            Debug.LogError($"Skill icon not found in Resources/SkillIcon/{Skillicon}");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isButtonPressed)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 clampedPosition = ClampPositionInParent(mousePos);
            areaRect.position = clampedPosition;
        }
    }

    private Vector3 ClampPositionInParent(Vector3 position)
    {

        float clampedX = Mathf.Clamp(position.x, skillSpawner.localPointX.x, skillSpawner.localPointY.x);
        float clampedY = Mathf.Clamp(position.y, skillSpawner.localPointX.y, skillSpawner.localPointY.y);

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
                Vector3 mergePosition = (gameObject.transform.position + other.gameObject.transform.position) / 2;
                skillSpawner.MergeSkill(skill_ID + 1, mergePosition, tier + 1);
                //합쳐지는 이펙트
                GameMgr.Instance.playerMgr.skillBallControllers.Remove(gameObject.GetComponent<SkillBallController>());
                GameMgr.Instance.playerMgr.skillBallControllers.Remove(other.gameObject.GetComponent<SkillBallController>());
                GameMgr.Instance.uiMgr.uiMerge.SkillCountUpdate();
                Destroy(gameObject);
                Destroy(other.gameObject);
                break;
            }
        }
    }
}

