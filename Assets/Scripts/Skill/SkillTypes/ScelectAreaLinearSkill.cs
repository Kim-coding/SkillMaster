using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScelectAreaLinearAttack : MonoBehaviour, ISkillComponent, ISkill
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
        //�ʱ�ȭ �� �ʿ��� ������ �۾�
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width, int skillPropertyID) //��ų�� ���¿� ��ġ�� ����
    {
        this.skillObject = skillObject;
        Renderer renderer = this.skillObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
        skillObject.transform.localScale = new Vector2(range, width);

        skillObject.AddComponent<BoxCollider2D>();
        skillObject.GetComponent<BoxCollider2D>().isTrigger = true;

        var rot = (target.transform.position - launchPoint).normalized;

        float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg + 90;
        skillObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        skillObject.transform.position = target.transform.position;
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType) //��ų�� ������ �Ӽ��� ����
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
