using UnityEngine;

public interface IAttackable
{
    public void OnAttack(GameObject attacker, Attack attack);
}
