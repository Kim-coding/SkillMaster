using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAI : MonoBehaviour
{
    public float range = 5f;
    public float pathUpdateInterval = 0.5f;
    public float attackRange = 1.5f;
    private float pathUpdateTimer = 0f;
    private AStarPathfinding pathfinding;
    private List<Node> path;
    private int currentPathIndex = 0;
    private Transform currentTarget;

    private void Start()
    {
        pathfinding = GetComponentInParent<AStarPathfinding>();
        GameMgr.Instance.sceneMgr.mainScene.spawner.OnMonsterSpawned += OnMonsterSpawned;
    }

    private void OnDestroy()
    {
        GameMgr.Instance.sceneMgr.mainScene.spawner.OnMonsterSpawned -= OnMonsterSpawned;
    }

    private void OnMonsterSpawned(GameObject monster)
    {
        if (currentTarget == null)
        {
            currentTarget = monster.transform;
            UpdatePath();
        }
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
        if (path != null && currentPathIndex < path.Count)
        {
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
    }
}




//    public Transform monster;
//    private AStarPathfinding pathfinding;
//    private List<Node> path;
//    private int currentPathIndex = 0;
//    public float attackRange = 2f;

//    public float pathUpdataIntervar = 0.5f;   // 경로 갱신
//    private float pathUpdataTimer = 0f;

//    //public float targetUpdataIntervar = 0.2f; // 타갯 갱신
//    //private float targetUpdataTimer = 0f;

//    private void Start()
//    {
//        pathfinding = GetComponentInParent<AStarPathfinding>();
//    }

//    private void Update()
//    {
//        pathUpdataTimer += Time.deltaTime;
//        if(pathUpdataTimer >= pathUpdataIntervar)
//        {
//            pathUpdataTimer = 0f;
//            UpdatePath();
//        }

//        if (monster != null && monster.gameObject.activeInHierarchy)
//        {
//            if (Vector3.Distance(transform.position, monster.transform.position) <= attackRange)
//            {
//                Debug.Log("플레이어 : 공격상태");
//                return;
//            }
//            else
//            {
//                Move();
//            }
//        }
//        else
//        {
//            monster = null;
//        }
//    }

//    private void UpdatePath()
//    {
//        pathfinding.FindPath(transform.position, monster.position);
//        path = pathfinding.GetPath();
//        currentPathIndex = 0;
//    }

//    private void Move()
//    {
//        if (path != null && currentPathIndex < path.Count)
//        {
//            Vector3 targetPosition = path[currentPathIndex].worldPosition;
//            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);

//            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
//            {
//                currentPathIndex++;
//            }
//        }
//    }
