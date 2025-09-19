using Scripts.Attack;
using Scripts.Enemies;
using Scripts.Items;
using Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Heroes
{
    public class Hero : BaseCharacter
    {
        [field: SerializeField] public HeroItem HeroItem { get; private set; }

        [SerializeField] private AttackDictionary _attackDictionary;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private AttackSpawner _spawner;

        List<AttackSpawner> _spawnres;

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

        public void Initialize()
        {
            Initialize(HeroItem.Health);
            _healthBar.Initialize(Health);
            _spawnres = new List<AttackSpawner>();
        }

        public void LevelUp(Item levelUpItem)
        {
            AttackSpawner sameSpawner = _spawnres.FirstOrDefault(spawner => spawner.LevelUpItemData.Equals(levelUpItem.Data));

            if (sameSpawner == null)
            {
                if (levelUpItem.Data.IsAttack)
                {
                    AttackSpawner spawner = Instantiate(_spawner, transform.position, Quaternion.identity, transform);
                    spawner.Initialize(_enemyPool, _attackDictionary.GetByItem(levelUpItem.Data), levelUpItem);
                    gameObject.SetActive(true);
                    _spawnres.Add(spawner);
                }
            }
            else
            {
                //sameSpawner.LevelUp();
            }

            gameObject.SetActive(true);
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
}