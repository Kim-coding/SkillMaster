using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI; 

public class Equip : Item
{
    public Equip(Image icon, string path, string itemName) :
         base(icon, path, itemName) { }


    public int equipType;
    public string equipRarerity;
    public int reinforceStoneValue;
    

}
