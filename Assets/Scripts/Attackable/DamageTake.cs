using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTake : MonoBehaviour, IAttackable
{

    private MonsterAI monster;

    private void Awake()
    {
        monster = GetComponent<MonsterAI>();
    }

    public void OnAttack(GameObject attacker, GameObject defender, Attack attack)
    {
        monster.health.Minus(attack.Damage); // ������ ���� ���ؼ� ������
        Debug.Log(monster.health.ToString());
        if (monster.health.factor == 1 && monster.health.numberList[0] <= 0)
        {
            monster.health.Clear();
            monster.Die();
        }
    }
}
