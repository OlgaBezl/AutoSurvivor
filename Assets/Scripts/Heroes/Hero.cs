using Scripts.Attack;
using Scripts.Enemies;
using Scripts.Items;
using Scripts.Items.ScriptableObjects;
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

        [SerializeField] private BaseAttackItem _defaultAttackItem;
        [SerializeField] private AttackSpawner _attackSpawner;

        public Item DefaultAttack { get; private set; }

        public event Action HeroDeath;

        private List<AttackSpawner> _spawnres; 
        private HealthBar _healthBar;
        private AttackDictionary _attackDictionary;
        private EnemyPool _enemyPool;
        
        private void Awake()
        {
            if (HeroItem == null)
                throw new ArgumentNullException(nameof(HeroItem));

            if (_defaultAttackItem == null)
                throw new ArgumentNullException(nameof(_defaultAttackItem));

            if (_attackSpawner == null)
                throw new ArgumentNullException(nameof(_attackSpawner));
        }

        public void Initialize(HealthBar healthBar, AttackDictionary attackDictionary, EnemyPool enemyPool)
        {
            _healthBar = healthBar;
            _attackDictionary = attackDictionary;
            _enemyPool = enemyPool;

            Initialize(HeroItem.Health);
            _healthBar.Initialize(Health);
            _spawnres = new List<AttackSpawner>();

            DefaultAttack = _attackDictionary.GetItemByItemData(_defaultAttackItem);
            gameObject.SetActive(true);
        }

        public void LevelUp(Item levelUpItem)
        {
            AttackSpawner sameSpawner = _spawnres.FirstOrDefault(spawner => spawner.LevelUpItemData.Equals(levelUpItem.Data));

            if (sameSpawner == null)
            {
                if (levelUpItem.Data.IsAttack)
                {
                    AttackSpawner spawner = Instantiate(_attackSpawner, transform.position, Quaternion.identity, transform);
                    spawner.Initialize(_enemyPool, _attackDictionary.GetByItemData(levelUpItem.Data), levelUpItem);
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