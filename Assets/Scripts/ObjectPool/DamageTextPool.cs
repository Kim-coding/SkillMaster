using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : ObjectPool<DamageText>
{
    public DamageTextPool(DamageText prefab, Transform parentTransform, int initialCapacity = 10, int maxCapacity = 100) : 
        base(prefab, parentTransform, initialCapacity, maxCapacity)
    {
    }

    protected override void OnGet(DamageText obj)
    {
        base.OnGet(obj);
    }

    protected override void OnReturn(DamageText obj)
    {
        base.OnReturn(obj);
    }
}
