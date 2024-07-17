using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutDotSkill : MonoBehaviour, ISkillShape, IDamageType, ISkillComponent, ISkill
{
    //int skillID;
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private List<MonsterAI> monsters = new List<MonsterAI>(); //일정 간격으로 monsters 갱신.

    float duration = 1f;
    float timer = 0f;

    public float innerRadius = 1.2f;
    public float outerRadius = 1.5f;

    public void Initialize()
    {
        timer = 0f;
        //skillID = 0;
        skillObject = null;
        //초기화 시 필요한 나머지 작업
    }

    private void Start()
    {
        StartCoroutine(ApplyDotDamage());
    }
    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, Vector3 target, float range, float width)
    {
        this.skillObject = skillObject;
        Sprite innerCircleSprite = Resources.Load<Sprite>("Circle");
        if (innerCircleSprite != null)
        {
            this.skillObject.GetComponent<SpriteRenderer>().sprite = innerCircleSprite;
        }

        this.skillObject.transform.position = launchPoint;
        this.skillObject.transform.localScale = new Vector2(innerRadius, innerRadius);
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
            Debug.Log(monsters.Count);
            foreach (MonsterAI monster in monsters)
            {
                if (monster != null)
                {
                    DotDamage dotDamage = monster.gameObject.AddComponent<DotDamage>();
                    dotDamage.attacker = attacker;
                    dotDamage.attack = attack;
                    monster.ApplyDotDamage(dotDamage);
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void UpdateMonsterList()
    {
        monsters.Clear();

        GameObject[] allMonsters = GameMgr.Instance.GetMonsters();
        foreach (var m in allMonsters)
        {
            var monster = m.GetComponent<MonsterAI>();
            float distance = Vector2.Distance(attacker.transform.position, monster.transform.position);
            if (distance < outerRadius && distance > innerRadius)
            {
                monsters.Add(monster);
                Debug.Log(distance);
            }
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = 0f;
            Destroy(gameObject);
        }
    }
}
