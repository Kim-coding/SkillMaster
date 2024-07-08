using UnityEngine;

public interface IAttackable
{
    public void OnAttack(GameObject attacker,GameObject defender ,Attack attack);
}
