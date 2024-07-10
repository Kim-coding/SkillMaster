using UnityEngine;

public abstract class SkillBase : MonoBehaviour, ISkill
{
    protected GameObject attacker;
    protected Attack attack;

    public virtual void Initialize(GameObject attacker, Attack attack)
    {
        this.attacker = attacker;
        this.attack = attack;
    }

    public abstract void Activate();
    public abstract void Deactivate();
}