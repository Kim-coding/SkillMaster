using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : AttackDefinition
{
    public Attack CreateAttack(Status stat)
    {
        BossStat mState = stat.GetComponent<BossStat>();
        if (mState == null)
        {
            return new Attack(new BigInteger(0), false);
        }

        BigInteger damage = new BigInteger(mState.attackPower);
        // 최소데미지 ~ 최대데미지 판정
        float damageRange = Random.Range(0.7f, 1.2f);
        damage *= damageRange;
        bool isCritical = false;
        return new Attack(damage, isCritical);

    }
}
