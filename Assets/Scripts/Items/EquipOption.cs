using System.Collections.Generic;


public enum OptionType
{
    attackPower = 0,
    criticalPercent,
    criticalMultiple,
    attackSpeed,
    deffence,
    maxHealth,
    speed_1,
    speed_2,
    goldIncrease,
    recovery,
    attackRange,
    cooldown,
    count,
}


public class EquipOption
{
    public List<int> currentOptions = new List<int>();
    public int[] optionCount = new int[(int)OptionType.count];

    public float attackPower;
    public float criticalPercent;
    public float criticalMultiple;
    public float attackSpeed;
    public float deffence;
    public float maxHealth;
    public float speed_1;
    public float speed_2;
    public float goldIncrease;
    public float recovery;
    public float attackRange;
    public float cooldown;
}
