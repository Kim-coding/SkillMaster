using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScelectAreaLinearAttack : MonoBehaviour,ISkillShape,IDamageType, ISkillComponent, ISkill
{
    public string skillID = "ScelectAreaLinearAttack";
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

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, Vector3 target, float range, float width) //스킬의 형태와 위치를 설정
    {
        this.skillObject = skillObject;
        skillObject.transform.localScale = new Vector3(range, width, 1);

        skillObject.AddComponent<BoxCollider2D>();
        skillObject.GetComponent<BoxCollider2D>().isTrigger = true;

        var rot = (target - launchPoint).normalized;

        float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg + 90;
        skillObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        skillObject.transform.position = target;
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType) //스킬의 데미지 속성을 설정
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
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
