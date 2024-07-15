using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    public GameObject attacker;
    public Attack attack;

    private float timer = 0f;
    private float duration = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            var monsterComponents = collision.GetComponents<IAttackable>();
            foreach (var monsterComponent in monsterComponents)
            {
                monsterComponent.OnAttack(attacker.gameObject, collision.gameObject, attack);
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }
}