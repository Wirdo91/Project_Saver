namespace DefaultNamespace.Weapons
{
    using UnityEngine;

    public class Melee : WeaponBase, IDamageDealing
    {
        [SerializeField] private int _damage;

        public int damage => _damage;

        protected override void MakeAttack(Unit attacker, Unit target)
        {
            var actualAccuracy = GetAccuracy(attacker, target);

            if (Random.value <= actualAccuracy)
            {
                target.HitBy(this);
            }
        }
    }
}