using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAI : MonoBehaviour , IAnimation
{

    public CharacterStat characterStat;
    public Image hpBar;

    public GameObject animationPrefab;

    public PlayerMgr playerMgr;
    public PlayerSkills playerSkills;

    private List<Node> path;
    private int currentPathIndex = 0;
    //[HideInInspector]
    public Transform currentTarget;

    private AStarPathfinding pathfinding;
    private StateMachine stateMachine;
    [HideInInspector]
    public StateMachine PlayerStateMachine => stateMachine;

    public Animator Animator { get => animator;}

    private Animator animator;
    public bool onSkill = false;

    private void Awake()
    {
        pathfinding = GetComponentInParent<AStarPathfinding>();
        stateMachine = new StateMachine(this);
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }
    private void Start()
    {
        stateMachine.Initialize(new IdleState(this));
    }

    public void UpdateHpBar(float f)
    {
        hpBar.fillAmount = f;
    }

    private void Update()
    {
        stateMachine.Update();
        if(characterStat.Ondeath)
        {
            stateMachine.ChangState(stateMachine.idleState);
        }
        animator.SetFloat("Multiplier", characterStat.attackSpeed);

        if (currentTarget != null) {
            if ((currentTarget.transform.position - transform.position).x <= 0)
            {
                animationPrefab.transform.localScale = new Vector3(Mathf.Abs(animationPrefab.transform.localScale.x), animationPrefab.transform.localScale.y);
            }
            else
            {
                animationPrefab.transform.localScale = new Vector3(Mathf.Abs(animationPrefab.transform.localScale.x) * -1, animationPrefab.transform.localScale.y);
            }
        }
    }
    public void SetTarget(Transform target)
    {
        if (currentTarget != target)
        {
            currentTarget = target;
            UpdatePath();
        }
    }
    public void UpdatePath()
    {
        if (currentTarget != null)
        {
            pathfinding.FindPath(transform.position, currentTarget);
            path = pathfinding.GetPath();
            currentPathIndex = 0;
        }
    }
    public void MoveAlongPath()
    {
        if (path != null && currentPathIndex < path.Count && currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.position) <= characterStat.attackRange)
                return;

            Vector3 targetPosition = path[currentPathIndex].worldPosition;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * characterStat.speed);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
    }
    public Transform FindClosestMonster()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestMonster = null;
        GameObject[] monsters = playerMgr.RequestMonsters();
        if(monsters == null)
        { return null; }    
        foreach (GameObject monster in monsters)
        {
            if (monster != null && monster.activeInHierarchy)
            {
                float distanceToMonster = Vector3.Distance(transform.position, monster.transform.position);
                if (distanceToMonster < closestDistance)
                {
                    closestDistance = distanceToMonster;
                    closestMonster = monster.transform;
                }
            }
        }
        return closestMonster;
    }

    public bool IsInAttackRange()
    {
        if(currentTarget == null) return false;
        return Vector3.Distance(transform.position, currentTarget.position) <= characterStat.attackRange;
    }

    private void ChangeState(IState newState)
    {
        stateMachine.ChangState(newState);
    }

    public void CheckAndChangeState()
    {
        if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
        {
            if (!(stateMachine.currentState is IdleState))
            {
                ChangeState(stateMachine.idleState);
            }
        }
        else if (IsInAttackRange())
        {
            if (!(stateMachine.currentState is BattleState))
            {
                ChangeState(stateMachine.battleState);
            }
        }
        else
        {
            if (!(stateMachine.currentState is WalkState))
            {
                ChangeState(stateMachine.walkState);
            }
        }
    }
    public void OnAttack(GameObject skill)
    {

        if (playerSkills.castingList.Count == 0)
        {
            onSkill = false;
            return;
        }

        if (currentTarget == null || !IsInAttackRange())
        {
            currentTarget = FindClosestMonster();
        }
        
        if (currentTarget == null)
        {
            onSkill = false;
            return;

        }


        var skillType = playerSkills.castingList[0].skillType;
        string skillDamage = playerSkills.castingList[0].skillDamage;
        var skillX = playerSkills.castingList[0].atkArangeX;
        var skillY = playerSkills.castingList[0].atkArangeY;
        var skillPropertyID = playerSkills.castingList[0].skillPropertyID;
        string skillEffect = playerSkills.castingList[0].SkillEffect;

        playerSkills.UseSkill(skill, skillType, gameObject, currentTarget.gameObject, skillX, skillY, skillDamage, skillPropertyID, skillEffect);
    
    }

    public void Restart()
    {
        onSkill = false;
        characterStat.Health = new BigInteger(characterStat.maxHealth);
        characterStat.Ondeath = false;
        UpdateHpBar(1f);
        currentTarget = FindClosestMonster();
        CheckAndChangeState();
    }

}
