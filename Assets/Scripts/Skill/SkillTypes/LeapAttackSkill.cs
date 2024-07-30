using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
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
    private float attackRadius;

    private int attackNumber = 1;  // 공격 횟수 TO-DO 테이블로 받아와야 하는 정보
    private float skillColdiwn = 0f;
    private GameObject skillEffectObject;
    private string skillEffect;

    public void Initialize()
    {

    }
    private void Start()
    {
        StartCoroutine(Leap());
    }
    public void ApplyShape(GameObject skillObject, Vector3 launchPosition, GameObject target, float range, float width, int skillPropertyID, string skillEffect)
    {
        this.skillObject = skillObject;
        this.skillEffect = skillEffect;
        Sprite circleSprite = Resources.Load<Sprite>("Circle");
        if (circleSprite != null)
        {
            this.skillObject.GetComponent<SpriteRenderer>().sprite = circleSprite;
            this.skillObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
        this.skillObject.AddComponent<CircleCollider2D>().isTrigger = true;

        var skillDownTable = DataTableMgr.Get<SkillDownTable>(DataTableIds.skillDown);
        if (skillPropertyID > 0)
        {
            var skillDownData = skillDownTable.GetID(skillPropertyID);
            if (skillDownData != null)
            {
                attackNumber = skillDownData.Attacknumber;
                skillColdiwn = skillDownData.SkillColdown;
            }
        }

        this.skillObject.transform.position = target.transform.position;
        this.skillObject.transform.localScale = new Vector2(range * 2, width * 2);

        attackRadius = range;
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
        for (int i = 0; i < attackNumber; i++)
        {
            Vector3 peakPosition = initialPosition + Vector3.up * leapHeight;
            Vector3 targetPeakPosition = targetPosition + Vector3.up * leapHeight;
            float elapsedTime = 0f;
            while (elapsedTime < leapDuration / 2)
            {
                attacker.transform.position = Vector3.Lerp(initialPosition, peakPosition, (elapsedTime / (leapDuration / 2)));
                elapsedTime += attacker.GetComponent<PlayerAI>().characterStat.attackSpeed * Time.deltaTime;
                yield return null;
            }

            Vector3 targetAbovePosition = new Vector3(targetPosition.x, targetPeakPosition.y, targetPosition.z);
            attacker.transform.position = targetAbovePosition;
            yield return null;

            Renderer renderer = skillObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }

            GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
            if (skillEffectPrefab != null)
            {
                skillEffectObject = Instantiate(skillEffectPrefab, attacker.transform.position, Quaternion.identity);

                skillEffectObject.transform.SetParent(skillObject.transform);

            }
            elapsedTime = 0f;
            targetPosition.y -= 0.5f;
            while (elapsedTime < leapDuration / 2)
            {
                attacker.transform.position = Vector3.Lerp(targetAbovePosition, targetPosition, (elapsedTime / (leapDuration / 5)));
                elapsedTime += attacker.GetComponent<PlayerAI>().characterStat.attackSpeed * Time.deltaTime;
                yield return null;
            }
            GameMgr.Instance.playerMgr.characters[0].GetComponent<PlayerAI>().onSkill = false;
            ApplyAttack();
            yield return new WaitForSeconds(skillColdiwn);
            initialPosition = attacker.transform.position;
            SetRandomTarget();
        }
        Stop();
    }

    private void ApplyAttack()
    {
        var allMonsters = GameMgr.Instance.GetMonsters();
        foreach (var monster in allMonsters)
        {
            var attackables = monster.GetComponents<IAttackable>();
            var distanc = Vector2.Distance(skillObject.transform.position, monster.transform.position);
            if (attackRadius >= distanc)
            {
                foreach (var attackable in attackables)
                {
                    attackable.OnAttack(attacker, monster, attack);
                }
            }
        }
        var playerAi = GameMgr.Instance.playerMgr.characters[0].GetComponent<PlayerAI>();
        playerAi.onSkill = false;
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
        StartCoroutine(Leap());
        Destroy(gameObject);
    }

    //void Update()
    //{
    //    Vector3 peakPosition = initialPosition + Vector3.up * leapHeight;
    //    Vector3 targetPeakPosition = targetPosition + Vector3.up * leapHeight;
    //    Vector3 targetAbovePosition = new Vector3();
    //    Debug.Log(GameMgr.Instance.playerMgr.characters[0].GetComponent<PlayerAI>().onSkill);
    //    switch (currentState)
    //    {
    //        case LeapState.Start:
    //            {

    //                attacker.transform.position = Vector3.Lerp(initialPosition, peakPosition, (timer / (leapDuration / 2)));
    //                timer += attacker.GetComponent<PlayerAI>().characterStat.attackSpeed * Time.deltaTime;
    //                if (timer > (leapDuration / 2))
    //                {
    //                    currentState = LeapState.floating;
    //                    timer = 0;
    //                }
    //                break;
    //            }
    //        case LeapState.floating:
    //            targetAbovePosition = new Vector3(targetPosition.x, targetPeakPosition.y, targetPosition.z);
    //            attacker.transform.position = targetAbovePosition;

    //            Renderer renderer = skillObject.GetComponent<Renderer>();
    //            if (renderer != null)
    //            {
    //                renderer.enabled = true;
    //            }
    //            GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
    //            if (skillEffectPrefab != null)
    //            {
    //                skillEffectObject = Instantiate(skillEffectPrefab, attacker.transform.position, Quaternion.identity);

    //                skillEffectObject.transform.SetParent(skillObject.transform);

    //            }
    //            currentState = LeapState.press;
    //            break;
    //        case LeapState.press:
    //            attacker.transform.position = Vector3.Lerp(targetAbovePosition, targetPosition, (timer / (leapDuration / 5)));
    //            timer += attacker.GetComponent<PlayerAI>().characterStat.attackSpeed * Time.deltaTime;
    //            if (timer > (leapDuration / 2))
    //            {
    //                currentState = LeapState.end;
    //                timer = 0;
    //            }
    //            break;
    //        case LeapState.end:
    //            GameMgr.Instance.playerMgr.characters[0].GetComponent<PlayerAI>().onSkill = false;
    //            Debug.Log(false);
    //            ApplyAttack();
    //            initialPosition = attacker.transform.position;
    //            SetRandomTarget();
    //            if(attackNumber > 1)
    //            {
    //                attackNumber--;
    //                currentState = LeapState.Start;
    //            }
    //            else
    //            {
    //                Stop();
    //            }
    //            break;
    //    }
    //}
}
