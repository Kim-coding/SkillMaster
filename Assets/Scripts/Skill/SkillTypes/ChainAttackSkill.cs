using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAttackSkill : MonoBehaviour, ISkillComponent, ISkill, ISpecialEffect
{
    public string skillID = "AreaSingleHitSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private int attackNumber = 2;  //TO-DO : 공격 횟수, 테이블 받아오기 //Initialize에 매개변수로 받아와야 함

    private int maxChains = 5;     //TO-DO : 피격 몬스터 수, 테이블 받아오기

    private HashSet<GameObject> hitMonsters = new HashSet<GameObject>(); //피격 몬스터 수 체크

    private Coroutine chainCoroutine;
    private GameObject currentTarget;

    private List<GameObject> lineRenderers = new List<GameObject>();

    void Start()
    {
        hitMonsters.Clear();
        ClearLineRenderers();

        if (GameMgr.Instance.sceneMgr.mainScene.IsBossBattle())
        {
            DrawLine(attacker, currentTarget);
            ApplyDamage(currentTarget);
        }
        else if (currentTarget != null)
        {
            DrawLine(attacker, currentTarget);
            StartChainAttack(currentTarget);
        }
    }

    public void Initialize()
    {
        //duration
        //attackNumber
        //maxChains
        hitMonsters.Clear();
        ClearLineRenderers();
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width)
    {
        currentTarget = target;
        this.skillObject = skillObject;
        skillObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;
    }

    public void ApplySpecialEffect(SpecialType specialType, int count)
    {
        if (specialType == SpecialType.Chaining)
        {
            maxChains = count;
        }
    }

    private void StartChainAttack(GameObject target)
    {
        if (chainCoroutine != null)
        {
            StopCoroutine(chainCoroutine);
        }
        chainCoroutine = StartCoroutine(ChainAttack(target));
    }

    private IEnumerator ChainAttack(GameObject initialTarget)
    {
        Queue<GameObject> targets = new Queue<GameObject>();
        targets.Enqueue(initialTarget);

        while (targets.Count > 0 && hitMonsters.Count < maxChains)
        {
            GameObject target = targets.Dequeue();

            if (target == null || !target.activeInHierarchy || hitMonsters.Contains(target))
            {
                continue;
            }

            hitMonsters.Add(target);
            ApplyDamage(target);

            yield return new WaitForSeconds(0.1f);
            ClearLineRenderers();
            List<GameObject> newTargets = new List<GameObject> { };
            if (hitMonsters.Count == 1)
            {
                newTargets = GetNewTarget(target, attackNumber);
            }
            else if(hitMonsters.Count < maxChains)
            {
                newTargets = GetNewTarget(target, 1);
            }

            foreach (var newTarget in newTargets)
            {
                if (newTarget != null && newTarget.activeInHierarchy)
                {
                    DrawLine(target, newTarget);
                    targets.Enqueue(newTarget);
                }
            }

            if (hitMonsters.Count >= maxChains)
            {
                break;
            }
        }

        ChainCoroutineStop();
    }

    private void ApplyDamage(GameObject target)
    {
        var attackables = target.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, target, attack);
        }
    }

    private List<GameObject> GetNewTarget(GameObject currentTarget, int count)
    {
        if (currentTarget == null)
        {
            return new List<GameObject>();
        }

        List<GameObject> newTargets = new List<GameObject>();
        var allMonsters = GameMgr.Instance.GetMonsters();

        List<GameObject> sortedMonsters = new List<GameObject>();
        foreach (var monster in allMonsters)
        {
            if (monster != null && !hitMonsters.Contains(monster))
            {
                sortedMonsters.Add(monster);
            }
        }

        sortedMonsters.Sort((a, b) =>
        {
            if (a == null || b == null)
            {
                return 0;
            }
            float distanceA = Vector2.Distance(currentTarget.transform.position, a.transform.position);
            float distanceB = Vector2.Distance(currentTarget.transform.position, b.transform.position);
            return distanceA.CompareTo(distanceB);
        });

        for (int i = 0; i < count && i < sortedMonsters.Count; i++)
        {
            if (sortedMonsters[i] != null && sortedMonsters[i].activeInHierarchy)
            {
                newTargets.Add(sortedMonsters[i]);
            }
        }
        return newTargets;
    }

    private void DrawLine(GameObject from, GameObject to)
    {
        if (from == null || to == null)
        {
            Debug.LogWarning("DrawLine: One of the targets is null");
            return;
        }

        GameObject lineObj = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, from.transform.position);
        lineRenderer.SetPosition(1, to.transform.position);
        lineObj.transform.SetParent(transform);
        lineRenderers.Add(lineObj);
    }

    private void ClearLineRenderers()
    {
        foreach (var lineRenderer in lineRenderers)
        {
            if (lineRenderer != null)
            {
                Destroy(lineRenderer);
            }
        }
        lineRenderers.Clear();
    }

    private void ChainCoroutineStop()
    {
        ClearLineRenderers();

        if (chainCoroutine != null)
        {
            StopCoroutine(chainCoroutine);
        }

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        ChainCoroutineStop();
    }
    private void OnDestroy()
    {
        ChainCoroutineStop();
    }
}
