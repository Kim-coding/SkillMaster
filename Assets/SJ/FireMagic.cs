using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class FireMagic : AttackDefinition
{

    int magicDamage = 1; // 테이블에서 받아오기

    public void abb() //경고듣기싫어서임시로만듬
    {
        magicDamage += 1;
    }
}
