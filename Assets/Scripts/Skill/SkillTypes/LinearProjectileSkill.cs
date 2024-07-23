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
    private float projectileangle;
    float duration = 1.0f;
    float timer = 0f;

    public float speed = 5f; // 투사체 속도

    private Vector3 direction;
    public void Initialize()
    {
        timer = 0f;
        skillObject = null;
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width, int skillPropertyID)
    {
        this.skillObject = skillObject;
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
        }

        skillObject.AddComponent<CircleCollider2D>();
        skillObject.GetComponent<CircleCollider2D>().isTrigger = true;

        skillObject.transform.position = launchPoint;
        skillObject.transform.localScale = new Vector2(width, width);

        direction = (target.transform.position - launchPoint).normalized;
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            var monsterComponent = collision.GetComponent<IAttackable>();
            monsterComponent.OnAttack(attacker.gameObject, collision.gameObject, attack);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = 0f;
            Destroy(gameObject);
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}