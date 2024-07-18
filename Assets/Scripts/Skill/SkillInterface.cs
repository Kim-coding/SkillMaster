using UnityEngine;

public interface ISkillShape //스킬의 형태와 위치를 설정
{
    void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width);
}

public interface IDamageType //스킬의 데미지 속성을 설정
{
    void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType);
}

public interface ISpecialEffect
{
    void ApplySpecialEffect(SpecialType specialType, int count);
}

public interface ISkillComponent
{
    void ApplyShape(GameObject skillObject, Vector3 launchPosition, GameObject targetPosition, float range, float width);
    void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType);
}

public interface ISkill
{
    void Initialize();
}