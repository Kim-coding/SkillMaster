using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface AttackDefinition
{
    //���� Ÿ�Ե� �޾Ƽ� ó�� �ؾߵɰͰ���

    public Attack CreateAttack(Status stat);

}
