using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStat
{
    public string playerAttackPower;
    public int defence;
    
    public string playerHealth;
    public string playerMaxHealth;
    public string playerHealthRecovery;

    public float speed;
    public float attackRange;
    public float attackSpeed;

    public float playerCriticalPercent;
    public float playerCriticalMultiple;


    // ����??
    //��ȭ ��ġ�����͵� ���⼭ �ջ� �ؾߵɰ� ����.

    public void Init()
    {
        playerAttackPower = "30";
        playerHealth = "1000";
        playerCriticalPercent = 50f / 100f;
        playerCriticalMultiple = 2f;
}   

}
