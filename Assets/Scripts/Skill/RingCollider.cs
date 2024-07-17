using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollider : MonoBehaviour
{
    private CircleCollider2D innerCollider;
    private DonutDotSkill skill;

    public void Initialize(CircleCollider2D innerCollider, DonutDotSkill skill)
    {
        this.innerCollider = innerCollider;
        this.skill = skill;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (innerCollider != null && other.CompareTag("Monster"))
        {
            //skill.RemoveMonster(other.GetComponent<MonsterAI>());
            Collider2D[] results = new Collider2D[1];
            ContactFilter2D filter = new ContactFilter2D();
            filter.NoFilter();
            if (innerCollider.OverlapCollider(filter, results) == 0)
            {
                skill.AddMonster(other.GetComponent<MonsterAI>());
            }
        }
    }
    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (innerCollider != null && other.CompareTag("Monster"))
    //    {
    //        Collider2D[] results = new Collider2D[1];
    //        ContactFilter2D filter = new ContactFilter2D();
    //        filter.NoFilter();
    //        if (innerCollider.OverlapCollider(filter, results) == 0)
    //        {
    //            skill.RemoveMonster(other.GetComponent<MonsterAI>());
    //        }
    //    }
    //}
}
