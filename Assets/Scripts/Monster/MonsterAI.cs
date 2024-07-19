using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterAI : MonoBehaviour, IAnimation
{
    private GameObject[] players;
    public GameObject target;

    private float timer = 0;
    private float attackTimer = 0;
    public float targetUpdataTime = 0.5f;

    public MonsterStat monsterStat;
    public MonsterAttack monsterAttack;
    [HideInInspector]
    public bool onDeath = false;

    private Rigidbody2D rb;
    private Animator animator;
    public Animator Animator { get => animator; }

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        monsterStat = GetComponent<MonsterStat>();
        monsterStat.Init();
        monsterAttack = new MonsterAttack();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (players.Length != 0)
        {
            FindTarget();
        }
    }

    private void OnEnable()
    {
        onDeath = false;
    }

    private void Update()
    {
        if (onDeath)
        { return; }
            timer += Time.deltaTime;
            attackTimer += Time.deltaTime;
            if (timer >= targetUpdataTime)
            {
                timer = 0;
                FindTarget();

            }

            if (target != null && target.activeInHierarchy)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= monsterStat.attackRange)
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    if (attackTimer >= monsterStat.attackSpeed)
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
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    Move();
                Rotation();
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
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, monsterStat.speed * Time.deltaTime);
    }
    private void Rotation()
    {
        Vector2 rot = (target.transform.position - transform.position).normalized;
        animator.SetFloat("InputX", rot.x);
        animator.SetFloat("InputY", rot.y);
    }
}
