using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDefinition
{

    //매직 타입도 받아서 처리 해야될것같음


    public Attack CreateAttack(CharacterStat PState)
    {
        BigInteger damage = new BigInteger(PState.attackPower);
        // 최소데미지 ~ 최대데미지 판정
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
        // 최소데미지 ~ 최대데미지 판정
        float damageRange = Random.Range(0.7f, 1.2f);
        damage *= damageRange;
        bool isCritical = false;
        return new Attack(damage, isCritical);
    }

}
