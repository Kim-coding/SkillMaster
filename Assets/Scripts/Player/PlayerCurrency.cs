using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCurrency
{
    public BigInteger gold = new BigInteger();
    public BigInteger diamond = new BigInteger();

    public void Init()
    {
        gold.Init(0);
        diamond.Init(0);
        GameMgr.Instance.playerMgr.textUpdate();
    }
  
    public void AddGold(BigInteger g)
    {
        gold.Plus(g);
        GameMgr.Instance.playerMgr.textUpdate();

    }
    public void AddDia(BigInteger d)
    {
        diamond.Plus(d);
        GameMgr.Instance.playerMgr.textUpdate();
    }

}
