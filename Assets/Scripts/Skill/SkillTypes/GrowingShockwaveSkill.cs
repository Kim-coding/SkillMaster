using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingShockwaveSkill : MonoBehaviour, ISkillComponent, ISkill  //충격파 ( 도넛, 1회성, 성장형 )
{

    public string skillID = "GrowingShockwaveSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private List<GameObject> attackedMonsters = new List<GameObject>();

    float duration = 2f;
    float timer = 0f;

    float growingSpeed = 1f;   // 성장 속도

    float initialInnerRadius = 0.5f;  // 시작 내원 반지름
    float initialOuterRadius = 1.0f;  // 시작 외원 반지름

    private float currentInnerRadius;
    private float currentOuterRadius;

    public void Initialize()
    {
        timer = 0f;
        //skillID = 0;
        skillObject = null;
        currentInnerRadius = initialInnerRadius;
        currentOuterRadius = initialOuterRadius;
        //초기화 시 필요한 나머지 작업
    }

    private void Start()
    {
        currentInnerRadius = initialInnerRadius;
        currentOuterRadius = initialOuterRadius;
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width)
    {
        this.skillObject = skillObject;
        Sprite circleSprite = Resources.Load<Sprite>("OuterCircleSprite");
        if (circleSprite != null)
        {
            skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
        }

        skillObject.transform.position = launchPoint;
        skillObject.transform.localScale = new Vector2(currentOuterRadius*2, currentOuterRadius * 2);
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;
    }

    private void UpdateMonsterList()
    {
        GameObject[] allMonsters = GameMgr.Instance.GetMonsters();
        foreach (var monster in allMonsters)
        {
            if (monster != null)
            {
                float distance = Vector2.Distance(skillObject.transform.position, monster.transform.position);
                if (distance < currentOuterRadius && distance > currentInnerRadius && !attackedMonsters.Contains(monster))
                {
                    attackedMonsters.Add(monster);
                    var attackables = monster.GetComponents<IAttackable>();
                    foreach(var attackable in attackables)
                    {
                        attackable.OnAttack(attacker, monster, attack);
                    }
                }
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
            timer = 0;
            attackedMonsters.Clear();
            Destroy(gameObject);
        }
        else
        {
            currentInnerRadius += growingSpeed * Time.deltaTime;
            currentOuterRadius += growingSpeed * Time.deltaTime;

            skillObject.transform.localScale = new Vector2(currentOuterRadius * 2, currentOuterRadius * 2);

            UpdateMonsterList();
        }
    }
}
