using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class FireMagic : AttackDefinition
{

    public Attack CreateAttack(Status stat)
    {
        CharacterStat PState = stat.GetComponent<CharacterStat>();
        if(PState == null)
        {
            return new Attack(new BigInteger(0), false);
        }
        BigInteger damage = new BigInteger(PState.attackPower);
        // 최소데미지 ~ 최대데미지 판정
        //Debug.Log(damage.ToString());
        float damageRange = Random.Range(0.7f, 1.2f);
        damage *= damageRange;
        bool isCritical = Random.value < PState.playerCriticalPercent;
        if (isCritical)
        {
            damage *= PState.playerCriticalMultiple;
        }

        return new Attack(damage, isCritical);
    }
}
