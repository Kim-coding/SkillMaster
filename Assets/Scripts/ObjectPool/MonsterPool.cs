using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : ObjectPool<MonsterAI>
{
    public MonsterPool(MonsterAI prefab, Transform parentTransform, int initialCapacity = 10, int maxCapacity = 100)
            : base(prefab, parentTransform, initialCapacity, maxCapacity)
    {
    }

    protected override void OnGet(MonsterAI obj)
    {
        //몬스터 풀 초기화 작업

        base.OnGet(obj);        
    }

    protected override void OnReturn(MonsterAI obj)
    {
        // 몬스터 정리 작업

        base.OnReturn(obj);

        if (obj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

}
