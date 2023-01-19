namespace DefaultNamespace.Weapons
{
    public class Bow : IEquipableWeapon
    {
        public float speed { get; }
        public float range { get; }
        
        public void Attack(Unit attacker, Unit target)
        {
        }
    }
}