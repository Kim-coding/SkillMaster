using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNewTarget : MonoBehaviour, IDestructible
{
    public void OnDestruction(GameObject attacker)
    {
        attacker.GetComponent<PlayerAI>().SetTarget(attacker.GetComponent<PlayerAI>().FindClosestMonster());
    }
}
