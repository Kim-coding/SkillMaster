using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface AttackDefinition
{
    //매직 타입도 받아서 처리 해야될것같음

    public Attack CreateAttack(Status stat);

}
