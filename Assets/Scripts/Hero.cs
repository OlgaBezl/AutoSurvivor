using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Hero : MonoBehaviour
{
    [SerializeField] private AttackSpawner _attackSpawner;
    [SerializeField] private EnemyPool _enemyPool;

    public event Action HeroDeath;

    private Health _health;

    private void Awake()
    {
        if (_attackSpawner == null)
            throw new ArgumentNullException(nameof(_attackSpawner));

        if (_enemyPool == null)
            throw new ArgumentNullException(nameof(_enemyPool));
    }

    public void Initialize(LevelUpItem levelUpItem)
    {
        _health = GetComponent<Health>();
        _health.Death += Death;

        if (levelUpItem.IsAttack)
        {
            BaseAttacker baseAttacker = Instantiate(_attackSpawner.GetAttacker(levelUpItem), transform.position, Quaternion.identity, transform);
            baseAttacker.Initialize(_enemyPool);
            gameObject.SetActive(true);
        }
    }

    public void Damage(float value)
    {
        _health.Damage(value);
    }

    private void Death()
    {
        _health.Death -= Death;
        HeroDeath?.Invoke();

        Destroy(gameObject);
    }
}
