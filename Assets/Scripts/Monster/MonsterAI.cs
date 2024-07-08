using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class MonsterAI : MonoBehaviour
{
    private GameObject[] players;
    public GameObject target;

    private float timer = 0;
    private float attackTimer = 0;
    public float targetUpdataTime = 0.5f;
    public float attackRange;

    public MonsterStat monsterStat;
    public MonsterAttack monsterAttack;

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        monsterStat = GetComponent<MonsterStat>();
        monsterStat.Init();
        monsterAttack = new MonsterAttack();
    }

    private void Start()
    {
        if (players.Length != 0)
        {
            FindTarget();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        if (timer >= targetUpdataTime)
        {
            timer = 0;
            FindTarget();

        }

        if (target != null && target.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                if (attackTimer >= 1f)
                {
                    var attackables = target.GetComponents<IAttackable>();
                    foreach (var attackable in attackables)
                    {
                        attackable.OnAttack(gameObject, target, monsterAttack.CreateAttack(monsterStat));
                    }
                    attackTimer = 0;
                }
                return;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                Move();
                attackTimer = 0;
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

}
