using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDefinition
{

    //���� Ÿ�Ե� �޾Ƽ� ó�� �ؾߵɰͰ���



    public Attack CreateAttack(PlayerStat PState, /*���� ����*/ Status MStage)
    {
        BigInteger damage = new BigInteger();
        damage.Init(PState.AttackPower);
        bool critical = false;
        // �ּҵ����� ~ �ִ뵥���� ���� !! �ʼ�
       //damage += Random.Range(miniDamage, maxDamage);
       // bool isCritical = Random.value < criticalChance;

      //  if (isCritical)
        {
       //     damage *= criticalMultiplier;
        }

      //  if (dStage != null)
        {
       //     damage -= dStage.armor;
        }
        return new Attack(damage , critical);
    }
}
