using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    //모든 스킬이 공통으로 들어갈 사항
    void Initialize(GameObject attacker, Attack attack);
    void Activate();
    void Deactivate();
}
