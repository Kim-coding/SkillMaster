using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectileSkill : MonoBehaviour, ISkillShape, IDamageType //���� ����ü ����
{
    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        throw new System.NotImplementedException();
    }

    public void ApplyShape(GameObject skillObject, Vector3 launchPoint, Vector3 target, float range, float width)
    {
        throw new System.NotImplementedException();
    }
}
