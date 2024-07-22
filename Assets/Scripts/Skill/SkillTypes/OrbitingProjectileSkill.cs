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
    public GameObject projectilePrefab;

    private float radius = 2f;           // 발사체가 회전하는 원의 반지름
    private float moveSpeed = 100f;       // 발사체 회전 속도
    private float attackAngle = 360f;    // 발사체 회전 각도
    private int attackNumber = 3;        // 공격 횟수 : 발사체 수
    private List<GameObject> projectiles = new List<GameObject>();

    public void Initialize()
    {
        // 필요 시 초기화 작업 수행
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width)
    {
        this.skillObject = Instantiate(skillObject);
        this.skillObject.transform.localScale = new Vector2(width, width);
        Sprite CircleSprite = Resources.Load<Sprite>("Circle");
        if (CircleSprite != null)
        {
            this.skillObject.GetComponent<SpriteRenderer>().sprite = CircleSprite;
        }
        
        this.skillObject.transform.position = launchPoint;
        skillObject.transform.position = launchPoint;
        skillObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        projectilePrefab = this.skillObject;
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
        for (int i = 0; i < attackNumber; i++)
        {
            yield return new WaitForSeconds(0.5f);

            GameObject projectile = Instantiate(projectilePrefab, attacker.transform.position, Quaternion.identity);
            Destroy(projectile.GetComponent<OrbitingProjectileSkill>());
            
            ProjectileBehavior projectileBehavior = projectile.AddComponent<ProjectileBehavior>();
            projectileBehavior.attacker = attacker;
            projectileBehavior.attack = attack;

            projectile.transform.SetParent(null);
            projectile.transform.SetParent(gameObject.transform);
            projectile.transform.Translate(this.skillObject.transform.up * radius, Space.World);
            projectile.AddComponent<CircleCollider2D>().isTrigger = true;

            projectiles.Add(projectile);
            StartCoroutine(MoveOrbitProjectile(projectile));
        }
    }

    private IEnumerator MoveOrbitProjectile(GameObject projectile)
    {
        float angle = 0f;
        while (angle < attackAngle)
        {
            angle += moveSpeed * Time.deltaTime;
            Vector3 offset = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * radius;
            projectile.transform.position = attacker.transform.position + offset;
            yield return null;
        }
        projectiles.Remove(projectile);
        Destroy(projectile);
        if (projectiles.Count == 0)
        {
            Destroy(skillObject);
            Destroy(gameObject);
        }
    }
}
