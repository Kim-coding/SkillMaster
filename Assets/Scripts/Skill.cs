using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill/BasicSkill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public GameObject skillPrefab;
    public int damage;
    public float cooldown;
}