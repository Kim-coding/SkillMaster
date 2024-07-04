using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public Transform monster;
    private AStarPathfinding pathfinding;
    private List<Node> path;
    private int currentPathIndex = 0;
    public float range = 5f;

    private void Start()
    {
        pathfinding = GetComponentInParent<AStarPathfinding>();
    }

    private void Update()
    {
        if (path == null || currentPathIndex >= path.Count)
        {
            pathfinding.FindPath(transform.position, monster.position);
            path = pathfinding.GetPath();
            currentPathIndex = 0;
        }

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
}
