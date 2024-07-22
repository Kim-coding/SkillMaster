using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamage : MonoBehaviour
{
    public int DotID { get; set; }
    public float Duration = 1f;
    public float TickInterval = 0.5f;

    public GameObject attacker {  get; set; }
    public Attack attack { get; set; }
    public List<GameObject> monsters = new List<GameObject>();

    public void SetMonsters(List<GameObject> monsters)
    {
        this.monsters = monsters;
    }
    public IEnumerator Apply(GameObject target)
    {
        float elapsedTime = 0f;

        while (elapsedTime < Duration)
        {
            if (!monsters.Contains(target) || target == null)
            {
                yield break;
            }
            var attackables = target.GetComponents<IAttackable>();
            foreach(var attackable in attackables)
            {
                attackable.OnAttack(attacker, target, attack);
            }
            yield return new WaitForSeconds(TickInterval);
            elapsedTime += TickInterval;
        }
    }
}
