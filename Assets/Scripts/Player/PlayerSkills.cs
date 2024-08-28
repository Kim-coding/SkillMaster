using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public BaseSkill baseSkill;
    public Transform skillParent;
    public List<SkillBallController> castingList;
    public FireMagic fireMagic;
    private PlayerAI playerAI;
    private DamageType damageType;
    private SkillShapeType skillShapeType;
    private int skillLv;
    private bool soundOn;
    private void Awake()
    {
        fireMagic = new FireMagic();
        playerAI = GetComponent<PlayerAI>();
    }

    public void UseSkill(int skillType, GameObject launchPoint, GameObject target, float speed, float range, float width, string skillDamage, int skillPropertyID, string skillEffect, int skillLv, bool soundOn = false)
    {
        this.soundOn = soundOn;
        this.skillLv = skillLv;
        fireMagic.SetDamage(skillDamage);
        var attack = fireMagic.CreateAttack(playerAI.characterStat);
        CreateSkill(skillType, launchPoint, target, speed, range, width, attack, skillPropertyID, skillEffect);
    }

    public GameObject CreateSkill(int type, GameObject launchPoint, GameObject target, float speed, float range, float width, Attack attack, int skillPropertyID, string skillEffect)
    {
        BaseSkill skillObject = Instantiate(baseSkill);
        Renderer renderer = skillObject.gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        ISkillComponent skillComponent = skillObject.GetComponent<ISkillComponent>();

        if(skillComponent != null)
        {
            return skillObject.gameObject;
        }
        switch (type)
        {
            case 1:
                skillComponent = skillObject.gameObject.GetOrAddComponent<LinearRangeAttackSkill>();
                skillShapeType = SkillShapeType.Linear;
                damageType = DamageType.OneShot;
                break;
            case 2:
                skillComponent = skillObject.gameObject.GetOrAddComponent<LinearProjectileSkill>();
                skillShapeType = SkillShapeType.Linear;
                damageType = DamageType.Penetrate;
                break;
            case 3:
                skillComponent = skillObject.gameObject.GetOrAddComponent<AreaDotSkill>();
                skillShapeType = SkillShapeType.Circular;
                damageType = DamageType.Dot;
                break;
            case 4:
                skillComponent = skillObject.gameObject.GetOrAddComponent<AreaSingleHitSkill>();
                skillShapeType = SkillShapeType.Circular;
                damageType = DamageType.OneShot;
                break;
            case 5:
                skillComponent = skillObject.gameObject.GetOrAddComponent<ScelectAreaLinearAttack>();
                skillShapeType = SkillShapeType.Linear;
                damageType = DamageType.OneShot;
                break;
            case 6:
                skillComponent = skillObject.gameObject.GetOrAddComponent<ScelectAreaProjectileSkill>();
                skillShapeType = SkillShapeType.Circular;
                damageType = DamageType.OneShot;
                break;
            case 7:
                skillComponent = skillObject.gameObject.GetOrAddComponent<OrbitingProjectileSkill>();
                skillShapeType = SkillShapeType.Circular;
                damageType = DamageType.Penetrate;
                break;
            case 8:
                skillComponent = skillObject.gameObject.GetOrAddComponent<ChainAttackSkill>();
                skillShapeType = SkillShapeType.Linear;
                break;
            case 9:
                skillComponent = skillObject.gameObject.GetOrAddComponent<DonutDotSkill>();
                skillShapeType = SkillShapeType.Circular;
                damageType = DamageType.Dot;
                break;
            case 10:
                skillComponent = skillObject.gameObject.GetOrAddComponent<GrowingShockwaveSkill>();
                skillShapeType = SkillShapeType.Circular;
                damageType = DamageType.OneShot;
                break;
            //case 11:
            //    skillComponent = skillObject.AddComponent<LeapAttackSkill>();
            //    skillShapeType = SkillShapeType.Circular;
            //    break;
                // 기타 스킬 타입 생성
        }
        if (skillComponent != null)
        {
            InitializeSkill(skillComponent, skillObject.gameObject, launchPoint, target, speed, range, width, attack, skillPropertyID, skillEffect);
        }

        return skillObject.gameObject;
    }

    private void InitializeSkill(ISkillComponent skillComponent, GameObject skillObject, GameObject launchPoint, GameObject target, float speed, float range, float width, Attack attack, int skillPropertyID, string skillEffect)
    {
        skillComponent.ApplyShape(skillObject, launchPoint.transform.position, target, speed, range, width, skillPropertyID, skillEffect);
        skillComponent.ApplyDamageType(launchPoint, attack, damageType, skillShapeType);
        if (soundOn)
        {
            GameMgr.Instance.soundMgr.PlaySFX($"Skill{skillLv}");
        }
    }
    public void SetList()
    {
        castingList = GameMgr.Instance.playerMgr.skillBallControllers
            .OrderByDescending(skill => skill.tier)
            .ToList();
    }

    private void ResetSkill(BaseSkill skillObject)
    {
        skillObject.gameObject.GetComponent<BaseSkill>().Reset();
    }
}
