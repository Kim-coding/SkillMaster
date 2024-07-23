using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScelectAreaProjectileSkill : MonoBehaviour, ISkillComponent, ISkill  //메테오 유사 스킬 (테이블 연결 미완)
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
    private float ProjectileSizeX = 1f;
    private float ProjectileSizeY = 1f;
    private int ProjectileValue;

    private float attackArangeX;
    private float attackArangeY;

    private bool isMeteor = false;
    public void Initialize()
    {
        
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width, int skillPropertyID)
    {
        this.skillObject = skillObject;
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            this.skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
        }
        targetPoint = target.transform.position;

        Renderer renderer = this.skillObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }

        if (skillPropertyID > 0)
        {
            var skillDownTable = DataTableMgr.Get<SkillDownTable>(DataTableIds.skillDown);
            var skillDownData = skillDownTable.GetID(skillPropertyID);
            if (skillDownData != null)
            {
                ProjectileValue = skillDownData.ProjectileValue;
                ProjectileSizeX = skillDownData.ProjectileSizeX;
                ProjectileSizeY = skillDownData.ProjectileSizeY;
            }
            isMeteor = true;
            this.skillObject.transform.localScale = new Vector2(ProjectileSizeX, ProjectileSizeY);
        }
        else
        {
            this.skillObject.transform.localScale = new Vector2(range, width);
        }

        attackArangeX = range;
        attackArangeY = width;

        this.skillObject.transform.position = new Vector3(targetPoint.x - attackArangeX, targetPoint.y + attackArangeX, targetPoint.z);

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
        if (isMoving && isMeteor)
        {
            MoveToTarget();
        }
        else
        {
            ApplyAttack();
        }
    }

    private void MoveToTarget()
    {
        float speed = moveSpeed * Time.deltaTime;
        skillObject.transform.position = Vector3.MoveTowards(skillObject.transform.position, targetPoint, speed);

        if (Vector3.Distance(skillObject.transform.position, targetPoint) <= 0.1f)
        {
            isMoving = false;
            skillObject.transform.localScale = new Vector2(attackArangeX, attackArangeY);
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
