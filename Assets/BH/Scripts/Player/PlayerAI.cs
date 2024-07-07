using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;

public class PlayerAI : MonoBehaviour
{
    public PlayerMgr playerMgr;
    public PlayerSkills playerSkills;

    public float attackRange = 1.5f;
    public float speed = 1f;

    public List<Node> path;
    public int currentPathIndex = 0;
    public Transform currentTarget;

    private AStarPathfinding pathfinding;
    private StateMachine stateMachine;
    public StateMachine PlayerStateMachine => stateMachine;
    private void Awake()
    {
        pathfinding = GetComponentInParent<AStarPathfinding>();
        stateMachine = new StateMachine(this);
        stateMachine.Initialize(new IdleState(this));
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

    public Transform FindClosestMonster()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestMonster = null;
        GameObject[] monsters = playerMgr.RequestMonsters();

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

    public void Attack(Skill skill)
    {
        if (currentTarget != null && playerSkills.CanUseSkill(skill))
        {
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            playerSkills.UseSkill(skill, transform.position, direction);
        }
    }
}