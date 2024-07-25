using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public BigInteger Health { get; set; }
    public bool Ondeath { get; set; }

    public float Defence { get; set; }
}
