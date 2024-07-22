using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScelectAreaProjectileSkill : MonoBehaviour, ISkillComponent, ISkill  //메테오 유사 스킬
{
    public string skillID = "ScelectAreaProjectileSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private Vector3 targetPoint;
    private float moveSpeed = 10f;
    private float attackRadius = 2f;
    private bool isMoving = false;

    public void Initialize()
    {
        
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width)
    {
        this.skillObject = skillObject;
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            this.skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
        }
        targetPoint = target.transform.position;

        this.skillObject.transform.localScale = new Vector2(range, range);
        this.skillObject.transform.position = new Vector3(targetPoint.x - range, targetPoint.y + range, targetPoint.z);

        isMoving = true;
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        float speed = moveSpeed * Time.deltaTime;
        skillObject.transform.position = Vector3.MoveTowards(skillObject.transform.position, targetPoint, speed);

        if (Vector3.Distance(skillObject.transform.position, targetPoint) <= 0.1f)
        {
            isMoving = false;
            ApplyAttack();
        }
    }

    private void ApplyAttack()
    {
        var allMonsters = GameMgr.Instance.GetMonsters();
        foreach (var monster in allMonsters)
        {
            var attackables = monster.GetComponents<IAttackable>();
            var distanc = Vector2.Distance(skillObject.transform.position, monster.transform.position);
            if (attackRadius >= distanc)
            {
                foreach (var attackable in attackables)
                {
                    attackable.OnAttack(attacker, monster, attack);
                }
            }
        }
        Destroy(gameObject);
    }
}
