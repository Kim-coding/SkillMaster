using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    public int damage = 10;
    public GameObject attacker;
    public Attack attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("АјАн");
            MonsterAI monster = collision.gameObject.GetComponent<MonsterAI>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
                monster.GetComponent<IAttackable>().OnAttack(attacker.gameObject, attack);
            }
            Destroy(gameObject);
        }
    }
}