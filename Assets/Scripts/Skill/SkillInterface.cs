using UnityEngine;

public interface ISkillShape //스킬의 형태와 위치를 설정
{
    void ApplyShape(GameObject skillObject, Vector3 launchPoint, Vector3 target, float range, float width);
}

public interface IDamageType //스킬의 데미지 속성을 설정
{
    void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType);
}

public interface ISpecialEffect
{
    void ApplyEffect(SpecialType specialType);
}

public interface ISkill
{
    void ApplyShape(GameObject skillObject, Vector3 launchPosition, Vector3 targetPosition, float range, float width);
    void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType);
}