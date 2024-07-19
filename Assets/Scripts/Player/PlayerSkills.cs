using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public List<GameObject> skills;  // 프리팹 (이펙트)
    public List<SkillType> skillTypeList;   // 내가 가지고 있는 스킬 종류 
    public FireMagic fireMagic;
    private PlayerAI playerAI;

    private void Awake()
    {
        fireMagic = new FireMagic();
        playerAI = GetComponent<PlayerAI>();
    }

    public void UseSkill(GameObject skill, SkillType skillType, GameObject launchPoint, GameObject target, float range, float width)
    {
        var attack = fireMagic.CreateAttack(playerAI.characterStat);
        CreateSkill(skill, skillType, launchPoint, target, range, width, attack);


    }
    public GameObject CreateSkill(GameObject skillPrefab, SkillType type, GameObject launchPoint, GameObject target, float range, float width, Attack attack)
    {
        GameObject skillObject = Instantiate(skillPrefab);
        ISkillComponent skillComponent = null;
        switch (type)
        {
            case SkillType.LinearRangeAttack:
                skillComponent = skillObject.AddComponent<LinearRangeAttackSkill>();
                break;
            case SkillType.ScelectAreaLinear:
                skillComponent = skillObject.AddComponent<ScelectAreaLinearAttack>();
                break;
            case SkillType.AreaSingleHit:
                skillComponent = skillObject.AddComponent<AreaSingleHitSkill>();
                break;
            case SkillType.DonutDot:
                skillComponent = skillObject.AddComponent<DonutDotSkill>();
                break;
            case SkillType.GrowingShockwave:
                skillComponent = skillObject.AddComponent<GrowingShockwaveSkill>();
                break;
            case SkillType.LinearProjectile:
                skillComponent = skillObject.AddComponent<LinearProjectileSkill>();
                break;
            case SkillType.ChainAttack:
                skillComponent = skillObject.AddComponent<ChainAttackSkill>();
                break;
            case SkillType.LeapAttack:
                skillComponent = skillObject.AddComponent<LeapAttackSkill>();
                break;
            case SkillType.AreaDot:
                skillComponent = skillObject.AddComponent<AreaDotSkill>();
                break;
            case SkillType.ScelectAreaProjectile:
                skillComponent = skillObject.AddComponent<ScelectAreaProjectileSkill>();
                break;
                // 기타 스킬 타입 생성
        }
        if(skillComponent != null)
        {
            InitializeSkill(skillComponent, skillObject, launchPoint, target, range, width, attack);
        }

        return skillObject;
    }

    private void InitializeSkill(ISkillComponent skillComponent, GameObject skillObject, GameObject launchPoint, GameObject target, float range, float width, Attack attack)
    {
        skillComponent.ApplyShape(skillObject, launchPoint.transform.position, target, range, width);
        skillComponent.ApplyDamageType(launchPoint, attack, DamageType.OneShot, SkillShapeType.Linear);
    }
}
