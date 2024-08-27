using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPool : ObjectPool<BaseSkill>
{
    public SkillPool(BaseSkill skillPrefab, Transform parentTransform, int initialCapacity = 10, int maxCapacity = 50)
        : base(skillPrefab, parentTransform, initialCapacity, maxCapacity) 
    { 

    }

    protected override void OnGet(BaseSkill baseSkill)
    {
        baseSkill.gameObject.SetActive(true);
    }

    protected override void OnReturn(BaseSkill baseSkill)
    {
        baseSkill.gameObject.SetActive(false);
        baseSkill.ResetComponents();
    }
}
