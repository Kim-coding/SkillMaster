public struct Attack
{
    public BigInteger Damage { get; set; }
    public bool Critical { get; set; }

    //public AttackType Type { get; set; }

    public Attack(BigInteger damage, bool critical /* AttackType type*/ )
    {
        Damage = damage;
        Critical = critical;
        //Type = type;
    }
}
