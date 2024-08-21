using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DamageTake : MonoBehaviour, IAttackable
{

    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        var characterHealth =  gameObject.GetComponent<IDamageable>();
        var defenceValue = 1 / ( 1 + characterHealth.Defence * 0.0002f);
        attack.Damage *= defenceValue;
        characterHealth.Health -= attack.Damage;
        gameObject.GetComponent<DamageDisplay>().DisplayText(attack);
        //Debug.Log(characterHealth.Health);
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
