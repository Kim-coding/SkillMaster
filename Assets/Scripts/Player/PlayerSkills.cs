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

    public void UseSkill(GameObject skill, SkillType skillType, GameObject launchPoint, Transform target, float range, float width)
    {
        var attack = fireMagic.CreateAttack(playerAI.characterStat);
        CreateSkill(skill, skillType, launchPoint, target, range, width, attack);


    }
    public GameObject CreateSkill(GameObject skillPrefab, SkillType type, GameObject launchPoint, Transform target, float range, float width, Attack attack)
    {
        GameObject skillObject = Instantiate(skillPrefab);
        ISkill skillComponent = null;
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
                // 기타 스킬 타입 생성
        }

        if(skillComponent != null)
        {
            InitializeSkill(skillComponent, skillObject, launchPoint, target, range, width, attack);
        }

        return skillObject;
    }

    private void InitializeSkill(ISkill skillComponent, GameObject skillObject, GameObject launchPoint, Transform target, float range, float width, Attack attack)
    {
        skillComponent.ApplyShape(skillObject, launchPoint.transform.position, target.position, range, width);
        skillComponent.ApplyDamageType(launchPoint, attack, DamageType.OneShot, SkillShapeType.Linear);
    }
}
