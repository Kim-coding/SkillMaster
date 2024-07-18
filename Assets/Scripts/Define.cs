
public enum MagicType
{
    None = -1,
    Fire = 0, // 불 마법

}

public enum AttackType
{
    None = -1,
    Bullet,
    Boom,
}
public enum SkillShapeType
{
    Circular,
    Linear,
}


public enum SkillType
{
    LinearRangeAttack,
    LinearProjectile,
    LeapAttack,
    OrbitingProjectile,
    ScelectAreaLinear,
    ScelectAreaProjectile,
    GrowingShockwave,
    DonutDot,
    ChainAttack,
    AreaSingleHit,
    AreaDot,
}

public enum DamageType
{
    Dot,
    Penetrate,
    OneShot
}

public enum SpecialType
{
    Chaining, 
    Growth, 
    Movement, 
    Follow,
}

public enum RangeType
{
    None = -1,
    Single,  //단일 공격 타입
    Multi,  //광역 공격 타입
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
    Gold,
}

public enum QuestType
{
    None = -1,
    Stage = 1,
    MonsterKill,
    MaxSkillLevel,
    MergeSkillCount,
    AttackEnhance,
    MaxHealthEnhance,
    DefenceEnhance,
    CriticalPercentEnhance,
    CriticalMultipleEnhance,
    RecoveryEnhance,
    GoldEnhance,
}


public static class DataTableIds
{
    public static readonly string stage = "StageTable";

    public static readonly string monster = "MonsterTable";

    public static readonly string boss = "BossTable";

    public static readonly string merge = "MergeTable";

    public static readonly string skill = "SkillTable";

    public static readonly string quest = "QuestTable";
}
