using System;
using UnityEngine;

public class Hero : BaseCharacter
{
    [field: SerializeField] public HeroItem HeroItem { get; private set; }

    [SerializeField] private AttackSpawner _attackSpawner;
    [SerializeField] private EnemyPool _enemyPool;

    public event Action HeroDeath;

    private void Awake()
    {
        if (HeroItem == null)
            throw new ArgumentNullException(nameof(HeroItem));

        if (_attackSpawner == null)
            throw new ArgumentNullException(nameof(_attackSpawner));

        if (_enemyPool == null)
            throw new ArgumentNullException(nameof(_enemyPool));

        Health = new Health(HeroItem.Health);
        Health.Deathed += Death;
    }

    public void Initialize(LevelUpItem levelUpItem)
    {
        if (levelUpItem.IsAttack)
        {
            BaseAttacker baseAttacker = Instantiate(_attackSpawner.GetAttacker(levelUpItem), transform.position, Quaternion.identity, transform);
            baseAttacker.Initialize(_enemyPool);
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

    private void Death()
    {
        Health.Deathed -= Death;
        HeroDeath?.Invoke();

        Destroy(gameObject);
    }
}
