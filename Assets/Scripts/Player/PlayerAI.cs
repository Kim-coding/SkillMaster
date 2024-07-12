using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAI : MonoBehaviour
{
    public CharacterStat characterStat;
    public PlayerBaseStat playerBaseStat;

    public PlayerMgr playerMgr;
    public PlayerSkills playerSkills;

    private float attackRange;
    private float speed;
    public float attackSpeed;

    private List<Node> path;
    private int currentPathIndex = 0;
    public Transform currentTarget;

    private AStarPathfinding pathfinding;
    private StateMachine stateMachine;
    public StateMachine PlayerStateMachine => stateMachine;

    private void Awake()
    {
        pathfinding = GetComponentInParent<AStarPathfinding>();
        stateMachine = new StateMachine(this);
    }
    private void Start()
    {
        stateMachine.Initialize(new IdleState(this));
        StatSetting();
        playerBaseStat.onSettingChange += StatSetting;
    }

    private void StatSetting()
    {
        speed = playerBaseStat.baseSpeed;
        attackRange = playerBaseStat.baseAttackRange;
        attackSpeed = playerBaseStat.baseAttackSpeed;
    }
    public void DebugStatSetting(float dSpeed, float dAttackSpeed, float dAttackRange)
    {
        speed = dSpeed;
        attackSpeed = dAttackSpeed;
        attackRange = dAttackRange;
    }

    private void OnDestroy()
    {
        playerBaseStat.onSettingChange -= StatSetting;
    }

    private void Update()
    {
        stateMachine.Update();
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
            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
                return;

            Vector3 targetPosition = path[currentPathIndex].worldPosition;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

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
        return Vector3.Distance(transform.position, currentTarget.position) <= attackRange;
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
                ChangeState(new IdleState(this));
            }
        }
        else if (IsInAttackRange())
        {
            if (!(stateMachine.currentState is BattleState))
            {
                ChangeState(new BattleState(this));
            }
        }
        else
        {
            if (!(stateMachine.currentState is WalkState))
            {
                ChangeState(new WalkState(this));
            }
        }
    }

    public void OnAttack(GameObject skill)
    {
        if (currentTarget != null && IsInAttackRange())
        {
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            playerSkills.UseSkill(skill, transform.position, direction);
        }
        else
        {
            currentTarget = FindClosestMonster();
            CheckAndChangeState();
        }
    }
}
