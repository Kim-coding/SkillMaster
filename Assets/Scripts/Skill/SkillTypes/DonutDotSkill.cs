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

    private GameObject innerCircle;
    private GameObject outerCircle;

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
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
        }

        //CircleCollider2D innerCollider = gameObject.AddComponent<CircleCollider2D>();
        //innerCollider.isTrigger = true;
        //innerCollider.radius = 0.15f; // 작은 반지름 설정

        CircleCollider2D outerCollider = gameObject.AddComponent<CircleCollider2D>();
        outerCollider.isTrigger = true;
        outerCollider.radius = 0.75f; // 큰 반지름 설정
        /*outerCollider.gameObject.AddComponent<RingCollider>().Initialize(innerCollider, this);*/ // 링콜라이더 설정 
     ////////임시 설정////////////////////////////
        outerCircle = new GameObject("OuterCircle");
        SpriteRenderer outerRenderer = outerCircle.AddComponent<SpriteRenderer>();
        Sprite outerSprite = Resources.Load<Sprite>("OuterCircleSprite");
        if (outerSprite != null)
        {
            outerRenderer.sprite = outerSprite;
            outerRenderer.color = Color.red;
        }
        else
        {
            Debug.LogError("Outer circle sprite not found!");
        }

        outerCircle.transform.SetParent(transform);
        outerCircle.transform.localScale = new Vector2(outerCollider.radius * 2, outerCollider.radius * 2);

        // 내부 원 스프라이트 설정
        innerCircle = new GameObject("InnerCircle");
        innerCircle.AddComponent<InnerCircleTrigger>().Initialize(this);
        CircleCollider2D innerCollider = innerCircle.AddComponent<CircleCollider2D>();
        innerCollider.isTrigger = true;
        innerCollider.radius = 0.2f; // 작은 반지름 설정
        SpriteRenderer innerRenderer = innerCircle.AddComponent<SpriteRenderer>();
        Sprite innerSprite = Resources.Load<Sprite>("InnerCircleSprite");
        if (innerSprite != null)
        {
            innerRenderer.sprite = innerSprite;
            innerRenderer.color = Color.gray;
        }
        else
        {
            Debug.LogError("Inner circle sprite not found!");
        }
        innerCircle.transform.SetParent(transform);
        innerCircle.transform.localScale = new Vector2(innerCollider.radius * 2, innerCollider.radius * 2);

        outerRenderer.sortingOrder = 1;
        innerRenderer.sortingOrder = 2;
        outerCollider.gameObject.AddComponent<RingCollider>().Initialize(innerCollider, this);
        ///////////////////////////////////////////////////
        skillObject.transform.position = launchPoint;
        skillObject.transform.localScale = new Vector2(range, range);
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;
    }
    private int count = 0;
    private IEnumerator ApplyDotDamage()
    {
        if (monsters.Count > 0)
        {
            foreach (MonsterAI monster in monsters)
            {
                if (monster != null)
                {
                    DotDamage dotDamage = monster.gameObject.AddComponent<DotDamage>();
                    dotDamage.attacker = attacker;
                    dotDamage.attack = attack;
                    monster.ApplyDotDamage(dotDamage);
                    count++;
                }
            }
            Debug.Log(count);
        }
        yield return new WaitForSeconds(1f);

        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            MonsterAI monster = collision.gameObject.GetComponent<MonsterAI>();
            if (monster != null)
            {
                AddMonster(monster);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            MonsterAI monster = collision.gameObject.GetComponent<MonsterAI>();
            if (monster != null)
            {
                Debug.Log("Exit");
                RemoveMonster(monster);
            }
        }
    }
    public void AddMonster(MonsterAI monster)
    {
        if (!monsters.Contains(monster))
        {
            monsters.Add(monster);
        }
    }

    public void RemoveMonster(MonsterAI monster)
    {
        if (monsters.Contains(monster))
        {
            monsters.Remove(monster);
            Debug.Log("Remove");
        }
    }

    public void OnInnerTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            MonsterAI monster = collision.gameObject.GetComponent<MonsterAI>();
            if (monster != null)
            {
                RemoveMonster(monster);
            }
        }
    }
    public void OnInnerTriggerExit(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            MonsterAI monster = collision.gameObject.GetComponent<MonsterAI>();
            if (monster != null)
            {
                AddMonster(monster);
            }
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if(timer >= duration)
        {
            timer = 0f;
            Destroy(gameObject);
        }
    }
}
