using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapAttackSkill : MonoBehaviour, ISkillShape, IDamageType, ISkillComponent, ISkill // 도약 공격 ( 원형, 범위, 1회성, 이동)
{
    //int skillID;
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }
    public void ApplyShape(GameObject skillObject, Vector3 launchPosition, GameObject targetPosition, float range, float width)
    {
        throw new System.NotImplementedException();
    }

    public void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        
    }
}
