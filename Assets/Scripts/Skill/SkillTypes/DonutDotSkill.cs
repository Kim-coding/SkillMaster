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

    float duration = 2f;
    float timer = 0f;

    public float innerRadius = 0.8f;
    public int innerNumSegments = 100;

    public float outerRadius = 1.5f;
    public int outerNumSegments = 100;

    public void Initialize()
    {
        timer = 0f;
        //skillID = 0;
        skillObject = null;
        //초기화 시 필요한 나머지 작업
    }

    private void Start()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Ground");
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        DrawCircle();
        StartCoroutine(ApplyDotDamage());
    }
    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, Vector3 target, float range, float width)
    {
        this.skillObject = skillObject;
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
            skillObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }


        skillObject.transform.position = launchPoint;
        skillObject.transform.localScale = new Vector2(range, range);
    }
    void DrawCircle()
    {
        LineRenderer innerLineRenderer = GetComponent<LineRenderer>();
        innerLineRenderer.positionCount = outerNumSegments + 1;
        innerLineRenderer.useWorldSpace = false;

        float deltaTheta = (2f * Mathf.PI) / outerNumSegments;
        float theta = 0f;

        for (int i = 0; i < outerNumSegments + 1; i++)
        {
            float x = outerRadius * Mathf.Cos(theta);
            float y = outerRadius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, y, 0);
            innerLineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
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
