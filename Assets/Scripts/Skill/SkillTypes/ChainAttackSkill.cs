using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChainAttackSkill : MonoBehaviour, ISkillShape, IDamageType, ISkillComponent, ISkill, ISpecialEffect
{
    public string skillID = "AreaSingleHitSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private int maxChains = 3;
    private int currentChainCount = 0;

    private HashSet<GameObject> hitMonsters = new HashSet<GameObject>();

    private Coroutine chainCoroutine;
    private GameObject currentTarget;

    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    void Start()
    {
        currentChainCount = 0;
        hitMonsters.Clear();
        ClearLineRenderers();
        DrawLine(attacker, currentTarget);
        StartChainAttack(currentTarget);
    }

    public void Initialize()
    {
        currentChainCount = 0;
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

    private IEnumerator ChainAttack(GameObject target)
    {
        if (currentChainCount >= maxChains || target == null || hitMonsters.Contains(target))
            yield break;
        
        hitMonsters.Add(target);
        ApplyDamage(target);

        yield return new WaitForSeconds(0.5f);

        List<GameObject> newTargets = GetNewTarget(target, 2);

        currentChainCount++;


        if (newTargets.Count > 0 && currentChainCount < maxChains)
        {
            foreach (var newTarget in newTargets)
            {
                if (newTarget != null)
                {
                    DrawLine(target,newTarget);
                    StartCoroutine(ChainAttack(newTarget));
                }
            }
        }
        else if (currentChainCount >= maxChains)
        {
            ChainCoroutineStop();
        }

        if (!target.activeInHierarchy)
        {
            ChainCoroutineStop();
        }

    }

    private void ApplyDamage (GameObject target)
    {
        var attackables = target.GetComponents<IAttackable>();
        foreach(var attackable in attackables)
        {
            attackable.OnAttack(attacker, target, attack);
        }
    }

    private List<GameObject> GetNewTarget(GameObject currentTarget, int count)
    {
        List<GameObject> newTargets = new List<GameObject>();
        var allMonsters = GameMgr.Instance.GetMonsters();

        List<GameObject> sortedMonsters = new List<GameObject>();
        foreach (var monster in allMonsters)
        {
            if (monster != null && !hitMonsters.Contains(monster) &&
                Vector2.Distance(currentTarget.transform.position, monster.transform.position) < 3f)
            {
                sortedMonsters.Add(monster);
            }
        }

        sortedMonsters.Sort((a, b) =>
        {
            float distanceA = Vector2.Distance(currentTarget.transform.position, a.transform.position);
            float distanceB = Vector2.Distance(currentTarget.transform.position, b.transform.position);
            return distanceA.CompareTo(distanceB);
        });

        for (int i = 0; i < count && i < sortedMonsters.Count; i++)
        {
            newTargets.Add(sortedMonsters[i]);
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

        lineRenderers.Add(lineRenderer);
    }

    private void ClearLineRenderers()
    {
        foreach (var lineRenderer in lineRenderers)
        {
            if (lineRenderer != null)
            {
                Destroy(lineRenderer.gameObject);
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
}
