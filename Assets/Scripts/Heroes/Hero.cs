using System;
using Scripts.Attack;
using UnityEngine;

public class Hero : BaseCharacter
{
    [field: SerializeField] public HeroItem HeroItem { get; private set; }

    [SerializeField] private AttackDictionary _attackDictionary;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private AttackSpawner _spawner;

    public event Action HeroDeath;

    private void Awake()
    {
        if (HeroItem == null)
            throw new ArgumentNullException(nameof(HeroItem));

        if (_attackDictionary == null)
            throw new ArgumentNullException(nameof(_attackDictionary));

        if (_enemyPool == null)
            throw new ArgumentNullException(nameof(_enemyPool));
    }

    public void Initialize(LevelUpItem levelUpItem)
    {
        Initialize(HeroItem.Health);

        _healthBar.Initialize(Health);

        if (levelUpItem.IsAttack)
        {
            AttackSpawner spawner = Instantiate(_spawner, transform.position, Quaternion.identity, transform);
            spawner.Initialize(_enemyPool, _attackDictionary.GetByItem(levelUpItem));
            gameObject.SetActive(true);
        }
    }

    public void Pause()
    {
        gameObject.SetActive(false);
    }

    public void Damage(float value)
    {
        Health.Damage(value);
    }

    protected override void Death()
    {
        base.Death();
        HeroDeath?.Invoke();
    }
}
