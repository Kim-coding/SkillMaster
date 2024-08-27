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

    float duration = 0.9f;
    float timer = 0f;
    float angle;
    private GameObject skillEffectObject;
    private string skillEffect;
    float atkArangeX;
    float atkArangeY;
    public void Initialize()
    {
        timer = 0f;
        //skillID = 0;
        skillObject = null;
        //초기화 시 필요한 나머지 작업
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float speed, float range, float width, int skillPropertyID, string skillEffect) //스킬의 형태와 위치를 설정
    {
        this.skillObject = skillObject;
        this.skillEffect = skillEffect;
        atkArangeX = range;
        atkArangeY = width;
        Renderer renderer = this.skillObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
        skillObject.transform.localScale = new Vector2(range, width);

        skillObject.AddComponent<BoxCollider2D>().isTrigger = true;
        this.skillObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        var rot = (target.transform.position - launchPoint).normalized;

        angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg + 90;
        skillObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        skillObject.transform.position = target.transform.position;
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType) //스킬의 데미지 속성을 설정
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;

        GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
        if (skillEffectPrefab != null)
        {
            skillEffectObject = Instantiate(skillEffectPrefab, skillObject.transform.position, Quaternion.identity);
            skillEffectObject.transform.SetParent(skillObject.transform);
            if (skillEffectPrefab.GetComponent<Animator>() != null)
            {
                skillEffectObject.transform.localScale = new Vector2(1, 1);
            }
            else
            {
                var mainModule = skillEffectObject.GetComponent<ParticleSystem>().main;
                mainModule.startSize = atkArangeX;
            }
        }
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
            GameMgr.Instance.playerMgr.characters[0].GetComponent<PlayerSkills>().skillPool.Return(gameObject.GetComponent<BaseSkill>());
        }
    }
}
