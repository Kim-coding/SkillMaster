using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : ObjectPool<DisplayText>
{
    public DamageTextPool(DisplayText prefab, Transform parentTransform, int initialCapacity = 10, int maxCapacity = 100) : 
        base(prefab, parentTransform, initialCapacity, maxCapacity)
    {
    }

    protected override void OnGet(DisplayText obj)
    {
        base.OnGet(obj);
    }

    protected override void OnReturn(DisplayText obj)
    {
        base.OnReturn(obj);
    }
}
