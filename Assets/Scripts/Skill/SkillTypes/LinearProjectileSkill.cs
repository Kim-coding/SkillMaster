using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectileSkill : MonoBehaviour, ISkillComponent, ISkill //직선 투사체 공격
{
    public string skillID = "LinearProjectileSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;
    
    private int attackNumber;
    private int projectileangle;
    private int ProjectileValue;
    private float ProjectileSizeX;
    private float ProjectileSizeY;

    Vector3 launchPoint;
    GameObject target;

    float duration = 1.0f;
    float timer = 0f;

    public float speed; // 투사체 속도
    private GameObject skillEffectObject;
    private string skillEffect;
    public void Initialize()
    {
        timer = 0f;
        skillObject = null;
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width, int skillPropertyID, string skillEffect)
    {
        this.skillObject = skillObject;
        this.skillEffect = skillEffect;
        if(skillPropertyID > 0)
        {
            var skillDownTable = DataTableMgr.Get<SkillDownTable>(DataTableIds.skillDown);
            var skillDownData = skillDownTable.GetID(skillPropertyID);
            if (skillDownData != null)
            {
                attackNumber = skillDownData.Attacknumber;
                ProjectileValue = skillDownData.ProjectileValue;
                projectileangle = skillDownData.Projectileangle;
                ProjectileSizeX = skillDownData.ProjectileSizeX;
                ProjectileSizeY = skillDownData.ProjectileSizeY;
                speed = skillDownData.ProjectileSpeed;
            }
        }
        
        skillObject.transform.position = launchPoint;
        this.launchPoint = launchPoint;
        this.target = target;
    }

    private void CreateProjectiles(Vector3 launchPoint, GameObject target)
    {
        for (int i = 0; i < ProjectileValue; i++)
        {
            for (int j = 0; j < attackNumber; j++)
            {
                GameObject projectile = Instantiate(skillObject);
                projectile.transform.position = launchPoint;
                projectile.transform.localScale = new Vector2(ProjectileSizeX, ProjectileSizeY);
                Sprite circleSprite = Resources.Load<Sprite>("Circle");
                if (circleSprite != null)
                {
                    projectile.GetComponent<SpriteRenderer>().sprite = circleSprite;
                    projectile.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
                }
                Destroy(projectile.GetComponent<LinearProjectileSkill>());
                projectile.AddComponent<CircleCollider2D>().isTrigger = true;

                ProjectileBehavior projectileBehavior = projectile.AddComponent<ProjectileBehavior>();
                projectileBehavior.attacker = attacker;
                projectileBehavior.attack = attack;

                Vector3 direction;
                if (projectileangle == -1)
                {
                    direction = (target.transform.position - launchPoint).normalized;
                }
                else
                {
                    float angle = projectileangle / 2 + (projectileangle / (ProjectileValue - 1)) * i;
                    direction = Quaternion.Euler(0, 0, angle) * (target.transform.position - launchPoint).normalized;
                }
                projectile.AddComponent<ProjectileMovement>().Initialize(direction, speed, duration, attacker, attack);

                GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
                if (skillEffectPrefab != null)
                {
                    skillEffectObject = Instantiate(skillEffectPrefab, attacker.transform.position, Quaternion.identity);
                    var mainModule = skillEffectObject.GetComponent<ParticleSystem>().main;
                    float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    mainModule.startRotation = Mathf.Deg2Rad * rotationAngle;
                    skillEffectObject.transform.SetParent(projectile.transform);

                }

            }
        }
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;

        CreateProjectiles(launchPoint, target);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = 0f;
            Destroy(gameObject);
        }
    }
}