using System.Collections.Generic;


public enum OptionType
{
    attackPower = 0,
    criticalPercent,
    criticalMultiple,
    attackSpeed,
    deffence,
    maxHealth,
    speed,
    goldIncrease,
    recovery,
    attackRange,
    cooldown,
    count,
}


public class EquipOption
{
    public List<(OptionType, float)> currentOptions = new List<(OptionType, float)>();
    public int[] optionCount = new int[(int)OptionType.count];

    public float attackPower;
    public float criticalPercent;
    public float criticalMultiple;
    public float attackSpeed;
    public float deffence;
    public float maxHealth;
    public float speed;
    public float goldIncrease;
    public float recovery;
    public float attackRange;
    public float cooldown;

    public void Init((OptionType, float) a, (OptionType, float) b, (OptionType, float) c, (OptionType, float) d)
    {
        AddOption(a.Item1, a.Item2);
        AddOption(b.Item1, b.Item2);
        AddOption(c.Item1, c.Item2);
        AddOption(d.Item1, d.Item2);
    }

    public void OptionClear()
    {
        currentOptions.Clear();
        for(int i = 0; i < optionCount.Length; i++)
        {
            optionCount[i] = 0;
        }
        attackPower = 0;
        criticalPercent = 0;
        criticalMultiple = 0;
        attackSpeed = 0;
        deffence = 0;
        maxHealth = 0;
        speed = 0;
        goldIncrease = 0;
        recovery = 0;
        attackRange = 0;
        cooldown = 0;
    }

    public void AddOption(OptionType option, float value)
    {
        if (optionCount[(int)option] > 2)
        {
            return;
        }

        currentOptions.Add((option, value));
        optionCount[(int)option]++;
        switch (option)
        {
            case OptionType.attackPower:
                attackPower = value;
                break;
            case OptionType.criticalPercent:
                criticalPercent = value;
                break;
            case OptionType.criticalMultiple:
                criticalMultiple = value;
                break;
            case OptionType.attackSpeed:
                attackSpeed = value;
                break;
            case OptionType.deffence:
                deffence = value;
                break;
            case OptionType.maxHealth:
                maxHealth = value;
                break;
            case OptionType.speed:
                speed = value;
                break;
            case OptionType.goldIncrease:
                goldIncrease = value;
                break;
            case OptionType.recovery:
                recovery = value;
                break;
            case OptionType.attackRange:
                attackRange = value;
                break;
            case OptionType.cooldown:
                cooldown = value;
                break;
        }
    }

    public void RemoveOption(OptionType option)
    {
        if (optionCount[(int)option] <= 0)
        {
            return;
        }

        for (int i = 0; i < currentOptions.Count; i++)
        {
            if (currentOptions[i].Item1 == option)
            {
                currentOptions.RemoveAt(i);
            }
        }
    }

    public void ChangeOption(OptionType preOption, OptionType newOption, float value)
    {
        for (int i = 0; i < currentOptions.Count; i++)
        {
            if (currentOptions[i].Item1 == preOption)
            {
                currentOptions[i] = (newOption, value);
            }
        }
    }

    public void ChangeOption(int index, OptionType newOption, float value)
    {
        if (index >= currentOptions.Count) { return; }

        currentOptions[index] = (newOption, value);
    }


}
