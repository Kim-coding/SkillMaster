using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public CharacterStat characterStat;
    public PlayerMgr playerMgr;
    public PlayerSkills playerSkills;
    public PlayerBaseStat playerBaseStat;

    private float attackRange;
    private float speed;
    private float attackSpeed;

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
        speed = playerBaseStat.speed;
        attackRange = playerBaseStat.attackRange;
        attackSpeed = playerBaseStat.attackSpeed;
    }

    private void Update()
    {
        stateMachine.Update();
    }
    public void SetTarget(Transform target)
    {
        currentTarget = target;
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
            if (monster.activeInHierarchy)
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
        if (currentTarget != null)
        {
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            playerSkills.UseSkill(skill, transform.position, direction, gameObject);
        }
    }
}
