using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAI : MonoBehaviour
{
    //private GameObject[] monsters;
    public Transform monster;
    private AStarPathfinding pathfinding;
    private List<Node> path;
    private int currentPathIndex = 0;
    public float attackRange = 2f;

    public float pathUpdataIntervar = 0.5f;   // ��� ����
    private float pathUpdataTimer = 0f;

    //public float targetUpdataIntervar = 0.2f; // Ÿ�� ����
    //private float targetUpdataTimer = 0f;

    private void Start()
    {
        pathfinding = GetComponentInParent<AStarPathfinding>();
        //monsters = GameObject.FindGameObjectsWithTag("Monster");// �Ŵ����� ���� �޴� �������� ����.
    }

    private void Update()
    {
        pathUpdataTimer += Time.deltaTime;
        if(pathUpdataTimer >= pathUpdataIntervar)
        {
            pathUpdataTimer = 0f;
            UpdatePath();
        }

        if (monster != null && monster.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, monster.transform.position) <= attackRange)
            {
                Debug.Log("�÷��̾� : ���ݻ���");
                return;
            }
            else
            {
                Move();
            }
        }
        else
        {
            monster = null;
        }
    }

    private void UpdatePath()
    {
        pathfinding.FindPath(transform.position, monster.position);
        path = pathfinding.GetPath();
        currentPathIndex = 0;
    }

    private void Move()
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
}
