using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public List<GameObject> skills;  // 프리팹 (이펙트)
    public List<SkillBallController> castingList;
    public FireMagic fireMagic;
    private PlayerAI playerAI;

    private void Awake()
    {
        fireMagic = new FireMagic();
        playerAI = GetComponent<PlayerAI>();
    }

    public void UseSkill(GameObject skill, int skillType, GameObject launchPoint, GameObject target, float range, float width, string skillDamage, int skillPropertyID, string skillEffect)
    {
        fireMagic.SetDamage(skillDamage);
        var attack = fireMagic.CreateAttack(playerAI.characterStat);
        CreateSkill(skill, skillType, launchPoint, target, range, width, attack, skillPropertyID, skillEffect);
    }
    public GameObject CreateSkill(GameObject skillPrefab, int type, GameObject launchPoint, GameObject target, float range, float width, Attack attack, int skillPropertyID, string skillEffect)
    {
        GameObject skillObject = Instantiate(skillPrefab);
        Renderer renderer = skillObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        ISkillComponent skillComponent = null;
        switch (type)
        {
            case 1:
                skillComponent = skillObject.AddComponent<LinearRangeAttackSkill>();
                break;
            case 2:
                skillComponent = skillObject.AddComponent<LinearProjectileSkill>();
                break;
            case 3:
                skillComponent = skillObject.AddComponent<AreaDotSkill>();
                break;
            case 4:
                skillComponent = skillObject.AddComponent<AreaSingleHitSkill>();
                break;
            case 5:
                skillComponent = skillObject.AddComponent<ScelectAreaLinearAttack>();
                break;
            case 6:
                skillComponent = skillObject.AddComponent<ScelectAreaProjectileSkill>();
                break;
            case 7:
                skillComponent = skillObject.AddComponent<OrbitingProjectileSkill>();
                break;
            case 8:
                skillComponent = skillObject.AddComponent<ChainAttackSkill>();
                break;
            case 9:
                skillComponent = skillObject.AddComponent<DonutDotSkill>();
                break;
            case 10:
                skillComponent = skillObject.AddComponent<GrowingShockwaveSkill>();
                break;
            case 11:
                skillComponent = skillObject.AddComponent<LeapAttackSkill>();
                break;
                // 기타 스킬 타입 생성
        }
        if (skillComponent != null)
        {
            InitializeSkill(skillComponent, skillObject, launchPoint, target, range, width, attack,  skillPropertyID, skillEffect);
        }

        return skillObject;
    }

    private void InitializeSkill(ISkillComponent skillComponent, GameObject skillObject, GameObject launchPoint, GameObject target, float range, float width, Attack attack, int skillPropertyID, string skillEffect)
    {
        skillComponent.ApplyShape(skillObject, launchPoint.transform.position, target, range, width, skillPropertyID, skillEffect);
        skillComponent.ApplyDamageType(launchPoint, attack, DamageType.OneShot, SkillShapeType.Linear);
    }
    public void SetList()
    {
        foreach(var skillObject in GameMgr.Instance.playerMgr.skillBallControllers) 
        {
            castingList.Add(skillObject);
        }
    }
}
