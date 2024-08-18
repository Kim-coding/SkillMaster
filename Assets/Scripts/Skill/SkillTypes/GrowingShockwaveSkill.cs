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

    float growingSpeed;   // 성장 속도

    private float currentRadius;
    private float endRadius;

    private GameObject skillEffectObject;
    private string skillEffect;

    public void Initialize()
    {
        skillObject = null;
        //초기화 시 필요한 나머지 작업
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float speed, float range, float width, int skillPropertyID, string skillEffect)
    {
        this.skillObject = skillObject;
        this.skillEffect = skillEffect;
        growingSpeed = speed;
        Sprite circleSprite = Resources.Load<Sprite>("OuterCircleSprite");
        if (circleSprite != null)
        {
            skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
        }
        if (skillPropertyID > 0)
        {
            var skillDownTable = DataTableMgr.Get<SkillDownTable>(DataTableIds.skillDown);
            var skillDownData = skillDownTable.GetID(skillPropertyID);
            if (skillDownData != null)
            {
                currentRadius = skillDownData.AtminRadius;
                endRadius = skillDownData.AtmaxRadius;
            }
        }
        skillObject.transform.position = launchPoint;
        skillObject.transform.localScale = new Vector2(currentRadius * 2, currentRadius * 2);
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {   
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;

        GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
        if (skillEffectPrefab != null)
        {
            skillEffectObject = Instantiate(skillEffectPrefab, attacker.transform.position, Quaternion.identity);
            skillEffectObject.transform.SetParent(skillObject.transform);
            skillEffectObject.transform.localScale = new Vector2(0.5f, 0.5f);
        }
    }

    private void UpdateMonsterList()
    {
        GameObject[] allMonsters = GameMgr.Instance.GetMonsters();
        foreach (var monster in allMonsters)
        {
            if (monster != null)
            {
                float distance = Vector2.Distance(skillObject.transform.position, monster.transform.position);
                if (distance < endRadius && !attackedMonsters.Contains(monster))
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
        if(currentRadius > endRadius)
        {
            attackedMonsters.Clear();
            Destroy(gameObject);
        }
        else
        {
            currentRadius += growingSpeed * Time.deltaTime;

            skillObject.transform.localScale = new Vector2(currentRadius * 2, currentRadius * 2);

            UpdateMonsterList();
        }
    }
}
