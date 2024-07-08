using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    public GameObject attacker;
    public Attack attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            //Debug.Log("АјАн");
            MonsterAI monster = collision.gameObject.GetComponent<MonsterAI>();
            if (monster != null)
            {
                var monsterComponents = collision.GetComponents<IAttackable>();
                foreach (var monsterComponent in monsterComponents)
                {
                    monsterComponent.OnAttack(attacker.gameObject, collision.gameObject, attack);
                }
            }
            Destroy(gameObject);
        }
    }
}