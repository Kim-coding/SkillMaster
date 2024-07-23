using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class FireMagic : AttackDefinition
{
    private string skillDamage;
    public void SetDamage(string skillDamage)
    {
        this.skillDamage = skillDamage;
    }

    public Attack CreateAttack(Status stat)
    {
        CharacterStat PState = stat.GetComponent<CharacterStat>();
        if(PState == null)
        {
            return new Attack(new BigInteger(0), false);
        }
        BigInteger damage = new BigInteger(PState.attackPower);
        damage += new BigInteger(skillDamage);
        // �ּҵ����� ~ �ִ뵥���� ����

        float damageRange = Random.Range(0.7f, 1.2f);
        damage *= damageRange;
        //Debug.Log(damage.ToString());
        if (damage.factor == 1 && damage.numberList[damage.factor-1] == 0)
        {
            damage.numberList[damage.factor - 1] = 1;
        } // ������ 0�� �ȵǰ� �ּ� ����

        bool isCritical = Random.value < PState.playerCriticalPercent;
        if (isCritical)
        {
            damage *= PState.playerCriticalMultiple;
        }

        return new Attack(damage, isCritical);
    }
}
