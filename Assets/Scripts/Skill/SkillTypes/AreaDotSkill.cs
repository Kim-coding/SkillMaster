using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDotSkill : MonoBehaviour, ISkillShape, IDamageType //원형 범위 도트 공격
{
    public void ApplyAttackType(SkillShapeType shapeType)
    {
        throw new System.NotImplementedException();
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width)
    {
        throw new System.NotImplementedException();
    }

    void IDamageType.ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        throw new System.NotImplementedException();
    }
}