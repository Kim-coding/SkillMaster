using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public List<Skill> skills;
    private Dictionary<string, float> skillCooldowns;

    void Start()
    {
        skillCooldowns = new Dictionary<string, float>();
        foreach (var skill in skills)
        {
            skillCooldowns[skill.skillName] = 0f;
        }
    }

    void Update()
    {
        List<string> keys = new List<string>(skillCooldowns.Keys);
        foreach (var key in keys)
        {
            if (skillCooldowns[key] > 0)
            {
                skillCooldowns[key] -= Time.deltaTime;
            }
        }
    }

    public bool CanUseSkill(Skill skill)
    {
        return skillCooldowns[skill.skillName] <= 0;
    }
    public void UseSkill(Skill skill, Vector3 position, Vector3 direction, GameObject attacker)
    {
        if (CanUseSkill(skill))
        {
            GameObject skillInstance = Instantiate(skill.skillPrefab, position, Quaternion.identity);
            Rigidbody2D rb = skillInstance.GetComponent<Rigidbody2D>();
            skillInstance.GetComponent<SkillProjectile>().attacker = gameObject;
            skillInstance.GetComponent<SkillProjectile>().attack = new FireMagic().CreateAttack(GameMgr.Instance.playerMgr.playerStat, null);
            if (rb != null)
            {
                rb.velocity = direction.normalized * 10f;
            }
            skillCooldowns[skill.skillName] = skill.cooldown;
        }
    }
}
