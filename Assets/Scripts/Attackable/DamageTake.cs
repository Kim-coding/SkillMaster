using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DamageTake : MonoBehaviour, IAttackable
{

    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        var characterHealth =  gameObject.GetComponent<IDamageable>();
        characterHealth.Health -= attack.Damage; // 데미지 감소 곱해서 빼야함
        //Debug.Log(monster.health.ToString());
        if (characterHealth.Health.factor == 1 && characterHealth.Health.numberList[0] <= 0 && !characterHealth.Ondeath)
        {
            characterHealth.Health.Clear();
            characterHealth.Ondeath = true;
            var destructibles = GetComponents<IDestructible>();
            foreach (var destructible in destructibles)
            {
                destructible.OnDestruction(attacker);
            }
        }
    }
}
