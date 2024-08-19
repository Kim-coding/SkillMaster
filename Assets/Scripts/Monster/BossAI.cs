using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

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

    public Image bossHp;
    private Transform hpCanvas;

    [HideInInspector]
    public bool onDeath = false;

    private Animator animator;
    public Animator Animator { get => animator; }

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        bossStat = GetComponent<BossStat>();
        //bossStat.Init();
        bossAttack = new BossAttack();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (players.Length != 0)
        {
            FindTarget();
        }
    }

    public void UpdateHpBar(float f)
    {
        if(bossHp != null)
        {
            bossHp.fillAmount = f;
        }
        if(GameMgr.Instance.uiMgr.monsterSlider != null)
        {
            GameMgr.Instance.uiMgr.monsterSlider.maxValue = 1f;
            GameMgr.Instance.uiMgr.monsterSlider.value = f;
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

        if (target != null)
        {
            if(bossHp != null)
            {
                hpCanvas = bossHp.transform.parent;
            }
            if ((target.transform.position - transform.position).x >= 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                if(hpCanvas != null)
                    hpCanvas.transform.localScale = new Vector3(Mathf.Abs(hpCanvas.transform.localScale.x), hpCanvas.transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
                if (hpCanvas != null)
                    hpCanvas.transform.localScale = new Vector3(Mathf.Abs(hpCanvas.transform.localScale.x) * -1, hpCanvas.transform.localScale.y);
            }
        }


        if (target != null && target.activeInHierarchy)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= bossStat.attackRange)
                {
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    if (attackTimer >= bossStat.attackSpeed)
                    {
                        animator.SetTrigger("Attack");
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
