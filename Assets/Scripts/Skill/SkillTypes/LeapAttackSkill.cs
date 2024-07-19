using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapAttackSkill : MonoBehaviour, ISkillShape, IDamageType, ISkillComponent, ISkill
{
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float leapHeight = 5f;
    private float leapDuration = 0.5f;
    private float attackRadius = 3f;

    private bool isLeaping = false;

    public void Initialize()
    {
        
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPosition, GameObject target, float range, float width)
    {
        this.skillObject = skillObject;
        skillObject.AddComponent<CircleCollider2D>();
        initialPosition = launchPosition;
        targetPosition = target.transform.position;
        StartCoroutine(Leap());
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;
    }

    private IEnumerator Leap()
    {
        isLeaping = true;

        // Step 1: Move upwards
        Vector3 peakPosition = initialPosition + Vector3.up * leapHeight;
        float elapsedTime = 0f;
        while (elapsedTime < leapDuration / 2)
        {
            attacker.transform.position = Vector3.Lerp(initialPosition, peakPosition, (elapsedTime / (leapDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Step 2: Move to target's above position
        Vector3 targetAbovePosition = new Vector3(targetPosition.x, peakPosition.y, targetPosition.z);
        elapsedTime = 0f;
        while (elapsedTime < leapDuration / 2)
        {
            attacker.transform.position = Vector3.Lerp(peakPosition, targetAbovePosition, (elapsedTime / (leapDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Step 3: Move downwards to target position
        elapsedTime = 0f;
        while (elapsedTime < leapDuration / 2)
        {
            attacker.transform.position = Vector3.Lerp(targetAbovePosition, targetPosition, (elapsedTime / (leapDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ApplyAreaAttack(targetPosition);

        isLeaping = false;
    }

    private void ApplyAreaAttack(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            var attackable = hitCollider.GetComponent<IAttackable>();
            if (attackable != null)
            {
                attackable.OnAttack(attacker, hitCollider.gameObject, attack);
            }
        }
    }

    void Update()
    {
        // Optionally, add update logic if needed
    }
}
