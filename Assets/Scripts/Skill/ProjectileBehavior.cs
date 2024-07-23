using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject attacker;
    public Attack attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            var attackables = collision.GetComponents<IAttackable>();
            foreach (var attackable in attackables)
            {
                attackable.OnAttack(attacker.gameObject, collision.gameObject, attack);
            }
        }
    }
}
