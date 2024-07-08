using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDefinition
{

    //���� Ÿ�Ե� �޾Ƽ� ó�� �ؾߵɰͰ���



    public Attack CreateAttack(PlayerStat PState)
    {
        BigInteger damage = new BigInteger();
        damage.Init(PState.AttackPower);
        // �ּҵ����� ~ �ִ뵥���� ����
       float damageRange = Random.Range(0.7f, 1.2f);
        damage.Multiple(damageRange);
        bool isCritical = Random.value < PState.criticalPercent;
         if (isCritical)
        {
            damage.Multiple(PState.criticalMultiple);
        }

        return new Attack(damage , isCritical);
    }


    public Attack CreateAttack(MonsterStat MStage)
    {
        BigInteger damage = new BigInteger();
        damage.Init(MStage.damage);
        // �ּҵ����� ~ �ִ뵥���� ����
        float damageRange = Random.Range(0.7f, 1.2f);
        damage.Multiple(damageRange);
        bool isCritical = false;
        return new Attack(damage, isCritical);
    }

}
