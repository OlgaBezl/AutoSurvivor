using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Hero : MonoBehaviour
{
    [SerializeField] private AttackerGenerator _attackerGenerator;
    [SerializeField] private EnemyPool _enemyPool;

    public event Action HeroDeath;

    private Health _health;

    public void Initialize(LevelUpItem levelUpItem)
    {
        _health = GetComponent<Health>();
        _health.Death += Death;

        BaseAttacker baseAttacker = Instantiate(_attackerGenerator.GetAttacker(levelUpItem), transform.position, Quaternion.identity, transform);
        baseAttacker.Initialize(_enemyPool);
        gameObject.SetActive(true);
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
