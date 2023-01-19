namespace DefaultNamespace.Weapons
{
    using UnityEngine;

    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxAccuracy;
        [SerializeField] private float _minAccuracy;
        [SerializeField] private float _maxRange;
        [SerializeField] private float _optimalRange;

        public float range => _maxRange;
        
        private float _attackTimer;
        
        public void Attack(Unit attacker, Unit target)
        {
            //Check timer
            if (_attackTimer <= 0)
            {
                MakeAttack(attacker, target);
                _attackTimer = _speed;
            }

            _attackTimer -= Time.deltaTime;
        }

        protected float GetAccuracy(Unit attacker, Unit target)
        {
            float distance = Vector2.Distance(attacker.position, target.position);

            float actualAccuracy = 1;
            if (distance > _maxRange)
            {
                actualAccuracy = 0;
            }
            else if (distance <= _optimalRange)
            {
                actualAccuracy = _maxAccuracy;
            }
            else
            {
                var rangeDif = _maxRange - _optimalRange;
                var dif = distance - _optimalRange;
                actualAccuracy = Mathf.Lerp(_minAccuracy, _maxAccuracy, dif / rangeDif);
            }

            return actualAccuracy;
        }
        
        protected abstract void MakeAttack(Unit attacker, Unit target);
    }
}