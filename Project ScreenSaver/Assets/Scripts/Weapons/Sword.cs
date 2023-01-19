namespace DefaultNamespace.Weapons
{
    public class Sword : IDamageDealing, IEquipableWeapon
    {
        public float damage { get; }
        public float speed { get; }
        public float range { get; }
        
        public void Attack(Unit attacker, Unit target)
        {
            
        }
    }
}