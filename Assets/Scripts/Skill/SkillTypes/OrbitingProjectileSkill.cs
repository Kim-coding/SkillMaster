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

    private float radius;           // �߻�ü�� ȸ���ϴ� ���� ������
    private float moveSpeed = 100f;       // �߻�ü ȸ�� �ӵ�
    private int Projectileangle;    // �߻�ü ȸ�� ����
    private int attackNumber;        // �߻�ü ���� Ƚ��
    private int ProjectileValue;     // �߻�ü ����
    private float ProjectileSizeX;   // �߻�ü ũ�� X
    private float ProjectileSizeY;   // �߻�ü ũ�� Y

    private List<GameObject> projectiles = new List<GameObject>();

    public void Initialize()
    {
        // �ʿ� �� �ʱ�ȭ �۾� ����
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width, int skillPropertyID)
    {
        this.skillObject = skillObject;
        radius = range; 
        
        this.skillObject.transform.position = launchPoint;
        skillObject.transform.position = launchPoint;
        
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
            GameObject projectile = Instantiate(skillObject, attacker.transform.position, Quaternion.identity);
            projectile.transform.localScale = new Vector2(ProjectileSizeX, ProjectileSizeY);
            Sprite CircleSprite = Resources.Load<Sprite>("Circle");
            if (CircleSprite != null)
            {
                projectile.GetComponent<SpriteRenderer>().sprite = CircleSprite;
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

            projectile.transform.Translate(skillObject.transform.up * radius, Space.World);
            projectile.AddComponent<CircleCollider2D>().isTrigger = true;

            projectiles.Add(projectile);
            StartCoroutine(MoveOrbitProjectile(projectile));
            yield return new WaitForSeconds(0.4f);
        }
    }

    private IEnumerator MoveOrbitProjectile(GameObject projectile)
    {
        float angle = 0f;
        while (angle < Projectileangle)
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
