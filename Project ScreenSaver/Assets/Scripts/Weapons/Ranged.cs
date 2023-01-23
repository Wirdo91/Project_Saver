namespace DefaultNamespace.Weapons
{
    using UnityEngine;

    public class Ranged : WeaponBase
    {
        public Projectile projectile;
        // Sway based on accuracy
        public float swayDegrees = 5;
        
        // Chance for miss on melee
        // Unit accuracy also, average of the two actual?  

        protected override void MakeAttack(Unit attacker, Unit target)
        {
            var actualAccuracy = GetAccuracy(attacker, target);

            var actualSway = Mathf.Lerp(swayDegrees, 0, actualAccuracy);
            actualSway *= Random.value > 0.5 ? 1 : -1;

            var direction = target.position - attacker.position;
            direction.Normalize();

            direction = Quaternion.Euler(0, 0, Random.value * actualSway) * direction;
            
            var projectileInstance = Instantiate(projectile);
            projectileInstance.Shoot(attacker, direction);

        }
    }
}