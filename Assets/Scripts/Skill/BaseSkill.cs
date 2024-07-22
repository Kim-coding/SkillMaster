using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour, ISkill
{
    public GameObject skillObject;
    public GameObject attacker;
    public Attack attack;
    public DamageType damageType;

    protected float timer = 0f;
    protected float duration = 1.5f;

    public abstract void Initialize();

    public virtual void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width)
    {
        this.skillObject = skillObject;
        this.skillObject.transform.position = launchPoint;
    }

    public virtual void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType)
    {
        this.attacker = attacker;
        this.attack = attack;
        this.damageType = damageType;
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Destroy(skillObject);
        }
    }
}