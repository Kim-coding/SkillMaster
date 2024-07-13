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
        GameMgr.Instance.uiMgr.AllUIUpdate(gold,diamond);
    }
  
    public void AddGold(BigInteger g)
    {
        gold.Plus(g);
        GameMgr.Instance.uiMgr.GoldTextUpdate(gold);
    }
    public void RemoveGold(BigInteger g)
    {
        gold.Minus(g);
        GameMgr.Instance.uiMgr.GoldTextUpdate(gold);
    }

    public void AddDia(BigInteger d)
    {
        diamond.Plus(d);
        GameMgr.Instance.uiMgr.DiaTextUpdate(diamond);
    }

    public void RemoveDia(BigInteger d)
    {
        diamond.Minus(d);
        GameMgr.Instance.uiMgr.DiaTextUpdate(diamond);
    }

}
