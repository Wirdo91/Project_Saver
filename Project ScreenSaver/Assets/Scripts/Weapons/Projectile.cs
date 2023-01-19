namespace DefaultNamespace.Weapons
{
    using System;
    using UnityEngine;
    public class Projectile : MonoBehaviour, IDamageDealing
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private bool _piercing;

        public int damage => _damage;
        
        private Unit _origin;
        
        private void LateUpdate()
        {
            transform.position += transform.up * _moveSpeed * Time.deltaTime;
            //Max life

            if (Mathf.Abs(transform.position.x) > Screen.width / Settings.zoom ||
                Mathf.Abs(transform.position.y) > Screen.height / Settings.zoom)
            {
                Destroy(gameObject);
            }
        }

        public void Shoot(Unit origin, Vector2 direction)
        {
            _origin = origin;

            transform.position = origin.position;
            transform.up = direction;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var unitHit = other.gameObject.GetComponent<Unit>();

            if (unitHit != null)
            {
                if (unitHit.team != _origin.team)
                {
                    //FIXME If piercing and all damage not expended, continue flight
                    unitHit.HitBy(this);
                    
                    Destroy(gameObject);
                }
            }
        }
    }
}