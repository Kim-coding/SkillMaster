using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAttackSkill : MonoBehaviour, ISkillComponent, ISkill, ISpecialEffect
{
    public string skillID = "ChainAttackSkill";
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private float timer = 0f;
    private float duration =  1f;

    private int attackNumber = 1;

    private int maxChains = 3;

    private HashSet<GameObject> hitMonsters = new HashSet<GameObject>(); //피격 몬스터 수 체크

    private Coroutine chainCoroutine;
    private GameObject currentTarget;

    private List<GameObject> lineRenderers = new List<GameObject>();
    private List<GameObject> chainEffects = new List<GameObject>();
    private GameObject skillEffectObject;
    private string skillEffect;

    void Start()
    {
        hitMonsters.Clear();
        ClearChainEffects();

        if (GameMgr.Instance.sceneMgr.mainScene.IsBossBattle())
        {
            StartChainEffect(attacker, currentTarget);
            ApplyDamage(currentTarget);
        }
        else if (currentTarget != null)
        {
            StartChainEffect(attacker, currentTarget);
            StartChainAttack(currentTarget);
        }
    }

    public void Initialize()
    {
        //duration
        //attackNumber
        //maxChains
        hitMonsters.Clear();
        ClearChainEffects();
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width, int skillPropertyID, string skillEffect)
    {
        currentTarget = target;
        this.skillEffect = skillEffect;
        Renderer renderer = skillObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
        this.skillObject = skillObject;
        skillObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        if (skillPropertyID > 0)
        {
            var skillDownTable = DataTableMgr.Get<SkillDownTable>(DataTableIds.skillDown);
            var skillDownData = skillDownTable.GetID(skillPropertyID);
            if (skillDownData != null)
            {
                attackNumber = skillDownData.Attacknumber;
                maxChains = skillDownData.HitMonsterValue;
            }
        }
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
            ClearChainEffects();
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
                    StartChainEffect(target, newTarget);
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

    private void StartChainEffect(GameObject from, GameObject to)
    {
        GameObject skillEffectPrefab = Resources.Load<GameObject>($"SkillEffects/{skillEffect}");
        if (skillEffectPrefab != null)
        {
            GameObject chainEffect = Instantiate(skillEffectPrefab, from.transform.position, Quaternion.identity);
            chainEffect.AddComponent<MoveEffect>().Initialize(from.transform.position, to.transform.position, 0.2f);
            chainEffects.Add(chainEffect);
        }
    }

    private void ClearChainEffects()
    {
        foreach (var effect in chainEffects)
        {
            if (effect != null)
            {
                Destroy(effect);
            }
        }
        chainEffects.Clear();
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
        ClearChainEffects();

        if (chainCoroutine != null)
        {
            StopCoroutine(chainCoroutine);
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if( timer > duration )
        {
            Destroy(gameObject);
        }
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
