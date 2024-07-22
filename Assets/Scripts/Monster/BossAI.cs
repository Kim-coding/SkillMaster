using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class BossAI : MonoBehaviour, IAnimation
{
    private int bossID;
    private GameObject[] players;
    public GameObject target;

    private float timer = 0;
    private float attackTimer = 0;
    public float targetUpdataTime = 0.5f;

    public BossStat bossStat;
    public BossAttack bossAttack;
    [HideInInspector]
    public bool onDeath = false;

    private Animator animator;
    public Animator Animator { get => animator; }

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        bossStat = GetComponent<BossStat>();
        bossStat.Init();
        bossAttack = new BossAttack();
        // animator = GetComponent<Animator>();
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
                if (Vector3.Distance(transform.position, target.transform.position) <= bossStat.attackRange)
                {
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    if (attackTimer >= bossStat.attackSpeed)
                    {
                        var attackables = target.GetComponents<IAttackable>();
                        foreach (var attackable in attackables)
                        {
                            attackable.OnAttack(gameObject, target, bossAttack.CreateAttack(bossStat));
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
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bossStat.speed * Time.deltaTime);
    }

}
