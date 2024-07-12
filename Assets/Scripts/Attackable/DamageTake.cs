using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterDamageTake : MonoBehaviour, IAttackable
{

    private MonsterAI monster;

    private void Awake()
    {
        monster = GetComponent<MonsterAI>();
    }

    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        monster.monsterStat.health -= attack.Damage; // 데미지 감소 곱해서 빼야함
        //Debug.Log(monster.health.ToString());
        if (monster.monsterStat.health.factor == 1 && monster.monsterStat.health.numberList[0] <= 0 && !monster.onDeath)
        {
            monster.monsterStat.health.Clear();
            monster.onDeath = true;
            var destructibles = GetComponents<IDestructible>();
            foreach (var destructible in destructibles)
            {
                destructible.OnDestruction(attacker);
            }
        }
    }
}
