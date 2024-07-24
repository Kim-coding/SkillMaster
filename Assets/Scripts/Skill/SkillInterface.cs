using UnityEngine;

public interface ISpecialEffect
{
    void ApplySpecialEffect(SpecialType specialType, int count);
}

public interface ISkillComponent
{
    //���̺��� �޾ƾ� �ϴ� ���� : ��ų ����Ʈ, ��ų ������, Ÿ��, ���ݹ���X, ���ݹ���Y, ��ų �ν� �Ÿ�, ��ų Ÿ��
    void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width, int skillPropertyID, string skillEffect); //��ų�� ���¿� ��ġ�� ����
    void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType); //��ų�� ������ �Ӽ��� ����
}

public interface ISkill
{
    void Initialize();
}

public interface IDotMonsters
{
    bool IsMonsterInList(GameObject monster);
    void UpdateMonsterList();
}