using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSingleHitSkill : MonoBehaviour, ISkillComponent, ISkill
{
    public string skillID = "AreaSingleHitSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;


    float duration = 0.5f;
    float timer = 0f;

    public void Initialize()
    {
        timer = 0f;
        //skillID = 0;
        skillObject = null;
        //초기화 시 필요한 나머지 작업
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width)
    {
        this.skillObject = skillObject;
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
        }

        skillObject.transform.localScale = new Vector2(range, range);

        skillObject.AddComponent<CircleCollider2D>().isTrigger = true;
        skillObject.transform.position = launchPoint;
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
            var monsterComponents = collision.GetComponents<IAttackable>();
            foreach (var monsterComponent in monsterComponents)
            {
                monsterComponent.OnAttack(attacker.gameObject, collision.gameObject, attack);
            }
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = 0;
            Destroy(gameObject);
        }
    }
}
