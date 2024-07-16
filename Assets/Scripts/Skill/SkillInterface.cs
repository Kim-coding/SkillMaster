using UnityEngine;

public interface ISkillShape //��ų�� ���¿� ��ġ�� ����
{
    void ApplyShape(GameObject skillObject, Vector3 launchPoint, Vector3 target, float range, float width);
}

public interface IDamageType //��ų�� ������ �Ӽ��� ����
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