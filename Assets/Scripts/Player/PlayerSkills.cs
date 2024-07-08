using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public List<GameObject> skills;


    public void UseSkill(GameObject skill, Vector3 position, Vector3 direction, GameObject attacker)
    {

        GameObject skillInstance = Instantiate(skill, position, Quaternion.identity);
        Rigidbody2D rb = skillInstance.GetComponent<Rigidbody2D>();
        skillInstance.GetComponent<SkillProjectile>().attacker = gameObject;
        skillInstance.GetComponent<SkillProjectile>().attack = new FireMagic().CreateAttack(GameMgr.Instance.playerMgr.playerStat);
        if (rb != null)
        {
            rb.velocity = direction.normalized * 10f;
        }
        
    }
}
