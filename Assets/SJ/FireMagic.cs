using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class FireMagic : AttackDefinition
{

    int magicDamage = 1; // ���̺��� �޾ƿ���

    public void abb() //�����Ⱦ�ӽ÷θ���
    {
        magicDamage += 1;
    }
}
