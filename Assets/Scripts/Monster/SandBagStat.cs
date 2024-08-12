using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBagStat : MonoBehaviour, IDamageable
{
    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }
    public float Defence { get; set; }

    private void Start()
    {
        Health = new BigInteger(0);
        Ondeath = false;
        Defence = 0f;
    }

}
