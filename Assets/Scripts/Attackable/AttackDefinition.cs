using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDefinition
{

    //매직 타입도 받아서 처리 해야될것같음



    public Attack CreateAttack(PlayerStat PState, /*몬스터 스탯*/ Status MStage)
    {
        BigInteger damage = new BigInteger();
        damage.Init(PState.AttackPower);
        bool critical = false;
        // 최소데미지 ~ 최대데미지 판정 !! 필수
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
