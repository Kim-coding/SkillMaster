using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDefinition
{

    //���� Ÿ�Ե� �޾Ƽ� ó�� �ؾߵɰͰ���


    public Attack CreateAttack(CharacterStat PState)
    {
        BigInteger damage = new BigInteger(PState.attackPower);
        // �ּҵ����� ~ �ִ뵥���� ����
       float damageRange = Random.Range(0.7f, 1.2f);
        damage *= damageRange;
        bool isCritical = Random.value < PState.playerCriticalPercent;
         if (isCritical)
        {
            damage *= PState.playerCriticalMultiple;
        }

        return new Attack(damage , isCritical);
    }


    public Attack CreateAttack(MonsterStat MStage)
    {
        BigInteger damage = new BigInteger(MStage.attackPower);
        // �ּҵ����� ~ �ִ뵥���� ����
        float damageRange = Random.Range(0.7f, 1.2f);
        damage *= damageRange;
        bool isCritical = false;
        return new Attack(damage, isCritical);
    }

}
