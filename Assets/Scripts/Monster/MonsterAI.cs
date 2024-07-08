using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class MonsterAI : MonoBehaviour
{
    private GameObject[] players;
    public GameObject target;

    private float timer = 0;
    public float targetUpdataTime = 0.5f;
    public float attackRange;

    public BigInteger health;

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        health = new BigInteger();
        health.Init(100);
    }

    private void Start()
    {
        if(players.Length != 0)
        {
            FindTarget();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= targetUpdataTime)
        {
            FindTarget();
            timer = 0;
        }

        if (target != null && target.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                return;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                Move();
            }
        }
        else
        {
            target = null;
        }
    }

    private void FindTarget()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestPlayer = null;

        foreach (var player in players)
        {
            if (player.activeInHierarchy)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestPlayer = player;
                }
            }
        }

        target = closestPlayer;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime);
    }

    public void Die()
    {
        Debug.Log("몬스터 사망");
        gameObject.SetActive(false); // 몬스터 비활성화 또는 파괴
    }
}
