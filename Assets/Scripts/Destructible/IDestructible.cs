using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible
{
    public void OnDestruction(GameObject attacker);
}
