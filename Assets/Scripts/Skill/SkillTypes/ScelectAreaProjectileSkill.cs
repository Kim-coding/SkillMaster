using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    private float projectileSizeX = 1f;
    private float projectileSizeY = 1f;
    private int projectileValue;
    private int attackNumber = 1;
    private float attackArangeX;
    private float attackArangeY;

    private float stayTimer;
    private float stayDuration = 0.2f;

    private GameObject skillEffectObject;
    private string skillEffect;

    public void Initialize()
    {
        
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float speed, float range, float width, int skillPropertyID, string skillEffect)
    {
        this.skillObject = skillObject;
        this.skillEffect = skillEffect;
        attackArangeX = width;
        attackArangeY = range;
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            this.skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
            this.skillObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
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
                projectileValue = skillDownData.ProjectileValue;
                projectileSizeX = skillDownData.ProjectileSizeX;
                projectileSizeY = skillDownData.ProjectileSizeY;
                attackNumber = skillDownData.Attacknumber;
            }
            if(projectileValue != -1)
            {
                isMoving = true;
            }
            this.skillObject.transform.localScale = new Vector2(projectileSizeX, projectileSizeY);
            this.skillObject.transform.position = new Vector3(targetPoint.x - attackArangeX, targetPoint.y + attackArangeX, targetPoint.z);
        }
        else
        {
            this.skillObject.transform.localScale = new Vector2(attackArangeX * 2, attackArangeY * 2);
            this.skillObject.transform.position = targetPoint;

        }

        GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
        if (skillEffectPrefab != null)
        {
            skillEffectObject = Instantiate(skillEffectPrefab, skillObject.transform.position, Quaternion.identity);

            skillEffectObject.transform.SetParent(skillObject.transform);
            var mainModule = skillEffectObject.GetComponent<ParticleSystem>().main;
            mainModule.startSize = attackArangeX * 2;
            skillEffectObject.transform.position = targetPoint;

        }
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
        else
        {
            stayTimer += Time.deltaTime;
            if (stayTimer >= stayDuration)
            {
                ApplyAttack();
                if (attackNumber > 1)
                {
                    GameObject nextTarget = FindClosestTarget(skillObject.transform.position);
                    if (nextTarget != null)
                    {
                        targetPoint = nextTarget.transform.position;
                        isMoving = true;
                        stayTimer = 0;
                        attackNumber--;
                    }
                    else
                    {
                        GameMgr.Instance.playerMgr.characters[0].GetComponent<PlayerSkills>().skillPool.Return(gameObject.GetComponent<BaseSkill>());
                    }
                }
                else
                {
                    GameMgr.Instance.playerMgr.characters[0].GetComponent<PlayerSkills>().skillPool.Return(gameObject.GetComponent<BaseSkill>());
                }
            }
        }
    }

    private GameObject FindClosestTarget(Vector3 currentPosition)
    {
        var allMonsters = GameMgr.Instance.GetMonsters();
        GameObject closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (var monster in allMonsters)
        {
            float distance = Vector2.Distance(currentPosition, monster.transform.position);
            if (distance < closestDistance && monster != null && monster.activeInHierarchy)
            {
                closestDistance = distance;
                closestTarget = monster;
            }
        }

        return closestTarget;
    }

    private void MoveToTarget()
    {
        float speed = moveSpeed * Time.deltaTime;
        skillObject.transform.position = Vector3.MoveTowards(skillObject.transform.position, targetPoint, speed);
        skillEffectObject.transform.position = Vector3.MoveTowards(skillObject.transform.position, targetPoint, speed);

        if (Vector3.Distance(skillObject.transform.position, targetPoint) <= 0.1f)
        {
            isMoving = false;
            skillObject.transform.localScale = new Vector2(attackArangeX * 2, attackArangeY * 2);
            skillEffectObject.transform.localScale = new Vector2(attackArangeX * 2, attackArangeY * 2);
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
        GameMgr.Instance.playerMgr.characters[0].GetComponent<PlayerSkills>().skillPool.Return(gameObject.GetComponent<BaseSkill>());
    }
}
