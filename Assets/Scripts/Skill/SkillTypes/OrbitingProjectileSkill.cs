using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingProjectileSkill : MonoBehaviour, ISkillComponent, ISkill
{
    public string skillID = "OrbitingProjectileSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private GameObject target;

    private float radius;           // 발사체가 회전하는 원의 반지름
    private float moveSpeed = 360f;       // 발사체 회전 속도
    private int Projectileangle;    // 발사체 회전 각도
    private int attackNumber;        // 발사체 공격 횟수
    private int ProjectileValue;     // 발사체 개수
    private float ProjectileSizeX;   // 발사체 크기 X
    private float ProjectileSizeY;   // 발사체 크기 Y
    private string skillEffect;
    private GameObject skillEffectObject;

    private List<GameObject> projectiles = new List<GameObject>();

    public void Initialize()
    {
        // 필요 시 초기화 작업 수행
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width, int skillPropertyID, string skillEffect)
    {
        this.skillObject = skillObject;
        this.skillEffect = skillEffect;
        radius = range; 
        this.target = target;
        this.skillObject.transform.position = launchPoint;
        skillObject.transform.position = launchPoint;
        
        if(skillPropertyID > 0)
        {
            var skillDownTable = DataTableMgr.Get<SkillDownTable>(DataTableIds.skillDown);
            var skillDownData = skillDownTable.GetID(skillPropertyID);
            if (skillDownData != null)
            {
                attackNumber = skillDownData.Attacknumber;
                ProjectileValue = skillDownData.ProjectileValue;
                Projectileangle = skillDownData.Projectileangle;
                ProjectileSizeX = skillDownData.ProjectileSizeX;
                ProjectileSizeY = skillDownData.ProjectileSizeY;
            }
        }

    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;

        StartCoroutine(CreateOrbitProjectiles());
    }

    private IEnumerator CreateOrbitProjectiles()
    {
        for (int j = 0; j < ProjectileValue; j++)
        {
            GameObject projectile = Instantiate(skillObject, target.transform.position, Quaternion.identity);
            projectile.transform.localScale = new Vector2(ProjectileSizeX, ProjectileSizeY);
            Sprite CircleSprite = Resources.Load<Sprite>("Circle");
            if (CircleSprite != null)
            {
                projectile.GetComponent<SpriteRenderer>().sprite = CircleSprite;
                projectile.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
            Renderer renderer = projectile.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
            Destroy(projectile.GetComponent<OrbitingProjectileSkill>());

            ProjectileBehavior projectileBehavior = projectile.AddComponent<ProjectileBehavior>();
            projectileBehavior.attacker = attacker;
            projectileBehavior.attack = attack;

            projectile.AddComponent<CircleCollider2D>().isTrigger = true;

            projectiles.Add(projectile);

            Vector3 directionToTarget = (projectile.transform.position - attacker.transform.position).normalized;
            float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            if (targetAngle < 0)
                targetAngle += 360f;

            GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
            if (skillEffectPrefab != null)
            {
                skillEffectObject = Instantiate(skillEffectPrefab, attacker.transform.position, Quaternion.identity);
                if (skillEffectPrefab.GetComponent<Animator>() != null)
                {
                    skillEffectObject.transform.rotation = Quaternion.Euler(0, 0, -targetAngle);
                    skillEffectObject.transform.SetParent(skillObject.transform);
                }
                else
                {
                    var mainModule = skillEffectObject.GetComponent<ParticleSystem>().main;
                    mainModule.startRotation = Mathf.Deg2Rad * targetAngle;

                    skillEffectObject.transform.SetParent(attacker.transform);

                    float effectLifetime = (Projectileangle / 720f);
                    Destroy(skillEffectObject, effectLifetime);
                }
            }

            StartCoroutine(MoveOrbitProjectile(projectile, attacker.transform.position, targetAngle)); 
            yield return new WaitForSeconds(0.4f);
        }
    }

    private IEnumerator MoveOrbitProjectile(GameObject projectile, Vector3 center, float targetAngle)
    {
        float angle = targetAngle;
        float endAngle = targetAngle - Projectileangle;

        while (angle > endAngle)
        {
            angle -= moveSpeed * Time.deltaTime;
            float radian = angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)) * radius;
            projectile.transform.position = center + offset;
            yield return null;
        }
        projectiles.Remove(projectile);

        Destroy(projectile);
        if (projectiles.Count == 0)
        {
            Destroy(skillObject);
            Destroy(gameObject);
            Destroy(skillEffectObject);
        }
    }
}
