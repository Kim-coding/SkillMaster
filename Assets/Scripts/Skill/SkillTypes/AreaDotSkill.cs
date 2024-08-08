using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDotSkill : MonoBehaviour, ISkillComponent, ISkill //원형 범위 도트 공격
{
    public string skillID = "AreaDotSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private float radius;
    private float skillCooldown;

    private GameObject skillEffectObject;
    private string skillEffect;

    float duration = 1.0f;
    float timer = 0f;

    public List<GameObject> monsters = new List<GameObject>();
    private Coroutine dotDamageCoroutine;
    private Coroutine applyCoroutine;

    private void Start()
    {
        applyCoroutine = StartCoroutine(ApplyDotDamage());
    }

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float speed, float range, float width, int skillPropertyID, string skillEffect)
    {
        this.skillObject = skillObject;
        this.skillEffect = skillEffect;
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            this.skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
        }
        radius = range;
        this.skillObject.transform.localScale = new Vector2(radius * 2, radius * 2);

        this.skillObject.AddComponent<CircleCollider2D>().isTrigger = true;
        this.skillObject.transform.position = launchPoint;
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;

        GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
        if (skillEffectPrefab != null)
        {
            skillEffectObject = Instantiate(skillEffectPrefab, attacker.transform.position, Quaternion.identity);
            skillEffectObject.transform.SetParent(skillObject.transform);
            skillEffectObject.transform.localScale = new Vector2(0.5f, 0.5f);

        }
    }

    private IEnumerator ApplyDotDamage()
    {
        while (true)
        {
            UpdateMonsterList();
            foreach (var monster in monsters)
            {
                if (monster != null)
                {
                    if (gameObject.GetComponent<DotDamage>() == null)
                    {
                        var dotDamage = gameObject.AddComponent<DotDamage>();
                        dotDamage.attacker = attacker;
                        dotDamage.attack = attack;
                        dotDamage.SetMonsters(monsters);
                        dotDamageCoroutine = StartCoroutine(dotDamage.Apply(monster));
                    }
                    else
                    {
                        var dotDamage = gameObject.GetComponent<DotDamage>();
                        dotDamage.attacker = attacker;
                        dotDamage.attack = attack;
                        dotDamage.SetMonsters(monsters);
                        dotDamageCoroutine = StartCoroutine(dotDamage.Apply(monster));
                    }
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateMonsterList()
    {
        monsters.Clear();

        GameObject[] allMonsters = GameMgr.Instance.GetMonsters();
        foreach (var monster in allMonsters)
        {
            if (monster != null)
            {
                float distance = Vector2.Distance(attacker.transform.position, monster.transform.position);
                if (distance < radius)
                {
                    monsters.Add(monster);
                }
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = 0f;
            if (applyCoroutine != null)
            {
                StopCoroutine(applyCoroutine);
                applyCoroutine = null;
            }
            if (dotDamageCoroutine != null)
            {
                StopCoroutine(dotDamageCoroutine);
                dotDamageCoroutine = null;
            }
            Destroy(gameObject);
        }
    }

}