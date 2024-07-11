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
    //SkillProjectile를 SkillBase를 상속 받게 하면 되지 않을까? 라는 생각 중
    //SkillBase, Skill 스크립트 추가
    //SkillBase는 추상 클래스로 만들었음.
    //SkillBase를 상속받아 구현을 하면 PlayerSkills 에서 뻑날예정 수정이 필요 함.
}
