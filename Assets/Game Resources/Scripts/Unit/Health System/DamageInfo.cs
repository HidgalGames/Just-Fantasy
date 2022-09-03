public class DamageInfo
{
    public int DamageCount { get; private set; }
    public Unit DamageDealer { get; private set; }
    public bool IsCritical { get; private set; }

    public DamageInfo(Unit damageDealer, int damage, bool isCritical = false)
    {
        DamageDealer = damageDealer;
        DamageCount = damage;
        IsCritical = isCritical;
    }
}
