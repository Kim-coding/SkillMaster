using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    //��� ��ų�� �������� �� ����
    void Initialize(GameObject attacker, Attack attack);
    void Activate();
    void Deactivate();
}
