using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPool : ObjectPool<SkillBase>
{
    public SkillPool(SkillBase prefab, Transform parentTransform, int initialCapacity = 10, int maxCapacity = 100)
            : base(prefab, parentTransform, initialCapacity, maxCapacity)
    {
    }

    protected override void OnGet(SkillBase obj)
    {
        base.OnGet(obj);
    }

    protected override void OnReturn(SkillBase obj)
    {
        base.OnReturn(obj);
    }
}
