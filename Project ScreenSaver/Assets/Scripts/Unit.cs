using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private int _health = 10;
    [SerializeField] private float _attackDistance = 1;
    [SerializeField] private int _damage = 1;

    private UnitManager _unitManager;
    
    private int _team = 0;

    public int team => _team;
    public bool isDead => _health <= 0;
    public Vector2 position => transform.position;

    public void Setup(UnitManager unitManager, int team)
    {
        _unitManager = unitManager;
        
        _team = team;
    }

    public void Damage(int damage)
    {
        _health -= damage;
        Floor.instance.AddBloodSplat(Vector2Int.RoundToInt(position));

        for (int i = 0; i < damage; i++)
        {
            Floor.instance.AddBloodSplat(Vector2Int.RoundToInt(position) + new Vector2Int(Random.Range(-5, 5), Random.Range(-5, 5)));
        }

        if (isDead)
        {
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

        if (distanceToTarget <= _attackDistance)
        {
            _currentTarget.Damage(_damage);
        }
        else
        {
            float moveDistance = _moveSpeed;
            
            Vector2 direction = _currentTarget.position - position;
            direction.Normalize();
            
            if (distanceToTarget < _moveSpeed)
            {
                moveDistance = distanceToTarget - _attackDistance;
            }
            
            transform.position = position + (direction * moveDistance);
        }
    }
}
