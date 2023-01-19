using System;
using DefaultNamespace;
using DefaultNamespace.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private int _health = 10;
    [SerializeField] private Renderer _model;
    [SerializeField] private Collider2D _collider;
    
    private UnitManager _unitManager;
    
    private int _team = 0;

    public int team => _team;
    public bool isDead => _health <= 0;
    public Vector2 position => transform.position;

    private WeaponBase _weapon;

    public void Setup(UnitManager unitManager, int team, WeaponBase weapon)
    {
        _unitManager = unitManager;
        _team = team;
        _weapon = weapon;
    }

    public void HitBy(IDamageDealing item)
    {
        Damage(item.damage);
    }

    private void Damage(int damage)
    {
        _health -= damage;

        for (int i = 0; i < damage; i++)
        {
            Floor.instance.AddBloodSplat(Vector2Int.RoundToInt(position) + new Vector2Int(Random.Range(-1, 1), Random.Range(-1, 1)));
        }

        if (isDead)
        {
            _model.material.color = Color.red;
            _collider.enabled = false;
            _unitManager.UnitDead(this);
        }
    }

    private Unit _currentTarget = null;
    private void Update()
    {
        if (isDead)
        {
            return;
        }
        
        if (_currentTarget == null || _currentTarget.isDead)
        {
            _currentTarget = _unitManager.GetEnemy(_team);
        }

        if (_currentTarget == null)
        {
            return;
        }

        var distanceToTarget = Vector2.Distance(position, _currentTarget.position);

        if (distanceToTarget <= _weapon.range)
        {
            _weapon.Attack(this, _currentTarget);
        }
        else
        {
            Move();
        }

        void Move()
        {
            float moveDistance = _moveSpeed * Time.deltaTime;
            
            Vector2 direction = _currentTarget.position - position;
            direction.Normalize();
            
            if (distanceToTarget - _weapon.range < moveDistance)
            {
                moveDistance = distanceToTarget - _weapon.range;
            }
            
            transform.position = position + (direction * moveDistance);
        }
    }
}
