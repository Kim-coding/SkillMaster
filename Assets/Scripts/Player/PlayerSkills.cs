using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public List<GameObject> skills;
    public FireMagic fireMagic;

    private void Awake()
    {
        fireMagic = new FireMagic();
    }

    public void UseSkill(GameObject skill, Vector3 position, Vector3 direction)
    {

        GameObject skillInstance = Instantiate(skill, position, Quaternion.identity);
        Rigidbody2D rb = skillInstance.GetComponent<Rigidbody2D>();
        skillInstance.GetComponent<SkillProjectile>().attacker = gameObject;
        skillInstance.GetComponent<SkillProjectile>().attack = fireMagic.CreateAttack(gameObject.GetComponent<CharacterStat>());
        if (rb != null)
        {
            rb.velocity = direction.normalized * 10f;
        }
        
    }
}
