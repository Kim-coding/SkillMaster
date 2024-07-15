
public enum MagicType
{
    None = -1,
    Fire = 0, // �� ����

}

public enum AttackType
{
    None = -1,
    Bullet,
    Boom,
}


public enum RangeType
{
    None = -1,
    Single,  //���� ���� Ÿ��
    Multi,  //���� ���� Ÿ��
}

public enum CurrencyType
{
    None = -1,
    Gold,
    Diamond,
    //Ruby
}

public enum EnhanceType
{
    None = -1,
    AttackPower,
    Defence,
    MaxHealth,
    Recovery,
    CriticalPercent,
    CriticalMultiple,
    AttackSpeed,
    Speed,
    AttackRange,
}


public static class DataTableIds
{
    public static readonly string stage = "StageTable";

    public static readonly string monster = "MonsterTable";

    public static readonly string boss = "BossTable";

    public static readonly string merge = "MergeTable";

    public static readonly string skill = "SkillTable";
}
