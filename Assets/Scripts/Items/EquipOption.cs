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
    }

    public bool AddOption(OptionType option, float value)
    {
        if (optionCount[(int)option] >= 2)
        {
            return false;
        }

        currentOptions.Add((option, value));
        optionCount[(int)option]++;
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
