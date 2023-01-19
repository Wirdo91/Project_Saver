namespace DefaultNamespace.Weapons
{
    public interface IEquipableWeapon
    {
        float speed { get; }
        float range { get; }

        void Attack(Unit attacker, Unit target);
    }
}