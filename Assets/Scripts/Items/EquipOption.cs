using System.Collections.Generic;


public enum OptionType
{
    attackPower = 0,
    deffence,
    maxHealth,
    criticalPercent,
    criticalMultiple,
    recovery,
    attackSpeed,
    speed,
    goldIncrease,
    attackRange,
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

    public void Init(List<(OptionType, float)> optionList)
    {
        foreach(var option in optionList)
        {
            currentOptions.Add(option);
        }
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
    }

    public bool AddOption(OptionType option, float value)
    {
        if (optionCount[(int)option] > 2)
        {
            return false;
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
        }
        return true;
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
