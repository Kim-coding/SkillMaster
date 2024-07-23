using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float duration;
    private float timer;
    private GameObject attacker;
    private Attack attack;

    public void Initialize(Vector3 direction, float speed, float duration, GameObject attacker, Attack attack)
    {
        this.direction = direction;
        this.speed = speed;
        this.duration = duration;
        this.attacker = attacker;
        this.attack = attack;
        timer = 0f;

        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            var monsterComponent = collision.GetComponent<IAttackable>();
            monsterComponent.OnAttack(attacker.gameObject, collision.gameObject, attack);
        }
    }
}
