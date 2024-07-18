using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DotDamage : MonoBehaviour
{
    public int DotID { get; set; }
    public float Duration = 1f;
    public float TickInterval = 0.2f;

    public GameObject attacker {  get; set; }
    public Attack attack { get; set; }

    public IEnumerator Apply(GameObject target)
    {
        float elapsedTime = 0f;
        var monster = target.GetComponent<MonsterAI>();

        while (elapsedTime < Duration)
        {
            var attackables = target.GetComponents<IAttackable>();
            foreach(var attackable in attackables)
            {
                attackable.OnAttack(attacker, target, attack);
            }
            yield return new WaitForSeconds(TickInterval);
            elapsedTime += TickInterval;
        }
        Destroy(this);
    }
}
