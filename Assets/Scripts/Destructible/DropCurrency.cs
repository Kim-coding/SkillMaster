using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCurrency : MonoBehaviour, IDestructible
{
    private BigInteger gold = new BigInteger();


    public void OnDestruction(GameObject attacker)
    {
        string g = "4534544";
        gold.Init(g);
        GameMgr.Instance.playerMgr.currency.AddGold(gold);
    }

}
