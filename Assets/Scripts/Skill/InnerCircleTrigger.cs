using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerCircleTrigger : MonoBehaviour
{
    private DonutDotSkill parentSkill;

    public void Initialize(DonutDotSkill skill)
    {
        parentSkill = skill;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("»£√‚");
            parentSkill.OnInnerTriggerEnter(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            parentSkill.OnInnerTriggerExit(other);
        }
    }
}
