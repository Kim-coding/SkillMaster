using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutDotSkill : MonoBehaviour, ISkillComponent, ISkill
{
    public string skillID = "DonutDotSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    public List<GameObject> monsters = new List<GameObject>(); //일정 간격으로 monsters 갱신.

    float duration = 1f;
    float timer = 0f;

    public float innerRadius = 0.8f;
    public float outerRadius = 1.5f;

    private Coroutine applyCoroutine;
    private Coroutine dotDamageCoroutine;

    public void Initialize()
    {
        timer = 0f;
        //skillID = 0;
        skillObject = null;
        //초기화 시 필요한 나머지 작업
    }

    private void Start()
    {
        applyCoroutine = StartCoroutine(ApplyDotDamage());
    }
    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width)
    {
        this.skillObject = skillObject;
        Sprite innerCircleSprite = Resources.Load<Sprite>("OuterCircleSprite");
        if (innerCircleSprite != null)
        {
            this.skillObject.GetComponent<SpriteRenderer>().sprite = innerCircleSprite;
        }

        this.skillObject.transform.position = launchPoint;
        this.skillObject.transform.localScale = new Vector2(outerRadius * 2, outerRadius * 2);
    }
    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;
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
                    if(gameObject.GetComponent<DotDamage>() == null)
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
                if (distance < outerRadius && distance > innerRadius)
                {
                    monsters.Add(monster);
                }
            }
        }
    }

    public void Update()
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
            if(dotDamageCoroutine != null)
            {
                StopCoroutine(dotDamageCoroutine);
                dotDamageCoroutine = null;
            }
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.position = attacker.transform.position;
        }
    }
}
