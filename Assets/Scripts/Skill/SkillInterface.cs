using UnityEngine;

public interface ISkillShape //��ų�� ���¿� ��ġ�� ����
{
    void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width);
}

public interface IDamageType //��ų�� ������ �Ӽ��� ����
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