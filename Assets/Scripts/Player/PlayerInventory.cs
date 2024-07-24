using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private readonly List<Equip> playerEquips = new List<Equip>();

    public int maxSlots = 150;

    public void AddItem(Equip equip)
    {
        playerEquips.Add(equip);
    }
    public void RemoveItem(Equip equip)
    {
        playerEquips.Remove(equip);
    }

    public List<Equip> GetPlayerEquips()
    {
        return playerEquips;
    }
}
