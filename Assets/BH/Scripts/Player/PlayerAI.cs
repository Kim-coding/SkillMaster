using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAI : MonoBehaviour
{
    public float range = 5f;
    private float pathUpdateInterval = 0.2f;
    public float attackRange = 1.5f;
    private float pathUpdateTimer = 0f;
    private AStarPathfinding pathfinding;
    private List<Node> path;
    private int currentPathIndex = 0;
    private Transform currentTarget;

    private void Start()
    {
        pathfinding = GetComponentInParent<AStarPathfinding>();
    }

    private void Update()
    {
        pathUpdateTimer += Time.deltaTime;

        if (pathUpdateTimer >= pathUpdateInterval)
        {
            pathUpdateTimer = 0f;
            UpdatePath();
        }

        MoveAlongPath();
    }

    private void UpdatePath()
    {
        currentTarget = FindClosestMonster();

        if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
            {
                Attack(currentTarget);
            }
            else
            {
                pathfinding.FindPath(transform.position, currentTarget.position);
                path = pathfinding.GetPath();
                currentPathIndex = 0;
            }
        }
    }

    private Transform FindClosestMonster()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestMonster = null;
        GameObject[] monsters = GameMgr.Instance.sceneMgr.mainScene.GetMonsters();

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

    private void MoveAlongPath()
    {
        if (path != null && currentPathIndex < path.Count && currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
                return;

            Vector3 targetPosition = path[currentPathIndex].worldPosition;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
    }

    private void Attack(Transform target)
    {
        Debug.Log("플레이어 : 공격상태 " + target.name);
        //Battle상태로 전환
    }
}