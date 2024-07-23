using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapAttackSkill : MonoBehaviour, ISkillComponent, ISkill
{
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float leapHeight = 1f;
    private float leapDuration = 0.5f;
    private float attackRadius = 2f;

    private float timer = 0f;
    private float duration = 1f;

    private int attackNumber = 1;  // 공격 횟수 TO-DO 테이블로 받아와야 하는 정보
    private Coroutine attackCoroutine;

    private void Start()
    {
        attackCoroutine = StartCoroutine(Leap());
    }

    public void Initialize()
    {
        
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPosition, GameObject target, float range, float width, int skillPropertyID)
    {
        this.skillObject = skillObject;
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            this.skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
        }
        this.skillObject.AddComponent<CircleCollider2D>().isTrigger = true;

        this.skillObject.transform.position = target.transform.position;
        this.skillObject.transform.localScale = new Vector2(attackRadius *2 , attackRadius* 2);

        initialPosition = launchPosition;
        targetPosition = target.transform.position;
        
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;
    }

    private IEnumerator Leap()
    {
        for(int i = 0; i < attackNumber; i++)
        {
            Vector3 peakPosition = initialPosition + Vector3.up * leapHeight;
            Vector3 targetPeakPosition = targetPosition + Vector3.up * leapHeight;
            float elapsedTime = 0f;
            while (elapsedTime < leapDuration / 2)
            {
                attacker.transform.position = Vector3.Lerp(initialPosition, peakPosition, (elapsedTime / (leapDuration / 2)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Vector3 targetAbovePosition = new Vector3(targetPosition.x, targetPeakPosition.y, targetPosition.z);
            attacker.transform.position = targetAbovePosition;
            yield return null;

            elapsedTime = 0f;
            while (elapsedTime < leapDuration / 2)
            {
                attacker.transform.position = Vector3.Lerp(targetAbovePosition, targetPosition, (elapsedTime / (leapDuration / 5)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            ApplyAttack();

            yield return new WaitForSeconds(0.3f);
            initialPosition = attacker.transform.position;
            SetRandomTarget();
        }
        Stop();
    }

    private void ApplyAttack()
    {
        var allMonsters = GameMgr.Instance.GetMonsters();
        foreach(var monster in allMonsters)
        {
            var attackables = monster.GetComponents<IAttackable>();
            var distanc = Vector2.Distance(skillObject.transform.position, monster.transform.position);
            if(attackRadius >= distanc)
            {
                foreach(var attackable in attackables)
                {
                    attackable.OnAttack(attacker, monster, attack);
                }
            }
        }
    }
    private void SetRandomTarget()
    {
        var allMonsters = GameMgr.Instance.GetMonsters();
        if (allMonsters.Length > 0)
        {
            int randomIndex = Random.Range(0, allMonsters.Length);
            attacker.GetComponent<PlayerAI>().currentTarget = allMonsters[randomIndex].transform;
            targetPosition = allMonsters[randomIndex].transform.position;
            skillObject.transform.position = targetPosition;
        }
    }

    private void Stop()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
             //Stop();
        }
    }
}
