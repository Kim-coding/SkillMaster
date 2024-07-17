using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public List<GameObject> skills;
    public FireMagic fireMagic;
    private PlayerAI playerAI;

    private void Awake()
    {
        fireMagic = new FireMagic();
        playerAI = GetComponent<PlayerAI>();
    }

    public void UseSkill(GameObject skill, GameObject launchPoint, Transform target, float range, float width)
    {
        var attack = fireMagic.CreateAttack(playerAI.characterStat);
        //CreateSkill(skill,SkillType.ScelectAreaLinear,launchPoint, target, range, width, attack);
        CreateSkill(skill,SkillType.ScelectAreaLinear, launchPoint, target, range, width, attack);
    }
    public GameObject CreateSkill(GameObject skillPrefab, SkillType type, GameObject launchPoint, Transform target, float range, float width, Attack attack)
    {
        GameObject skillObject = Instantiate(skillPrefab);
        switch (type)
        {
            case SkillType.LinearRangeAttack:
                var linearSkill = skillObject.AddComponent<LinearRangeAttackSkill>();
                linearSkill.ApplyShape(skillObject, launchPoint.transform.position, target.position, range, width);
                linearSkill.ApplyDamageType(launchPoint, attack, DamageType.OneShot, SkillShapeType.Linear);
                break;
            case SkillType.ScelectAreaLinear:
                var skill = skillObject.AddComponent<ScelectAreaLinearAttack>();
                skill.ApplyShape(skillObject, launchPoint.transform.position, target.position, range, width);
                skill.ApplyDamageType(launchPoint, attack, DamageType.OneShot, SkillShapeType.Linear);
                break;
                // 기타 스킬 타입 생성
        }
        return skillObject;
    }
}
