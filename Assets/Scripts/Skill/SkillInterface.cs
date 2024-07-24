using UnityEngine;

public interface ISpecialEffect
{
    void ApplySpecialEffect(SpecialType specialType, int count);
}

public interface ISkillComponent
{
    //테이블에서 받아야 하는 정보 : 스킬 이팩트, 스킬 시전자, 타겟, 공격범위X, 공격범위Y, 스킬 인식 거리, 스킬 타입
    void ApplyShape(GameObject skillObject, Vector3 launchPoint, GameObject target, float range, float width, int skillPropertyID, string skillEffect); //스킬의 형태와 위치를 설정
    void ApplyDamageType(GameObject attacker, Attack attack, DamageType damageType, SkillShapeType shapeType); //스킬의 데미지 속성을 설정
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