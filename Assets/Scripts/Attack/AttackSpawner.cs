using Scripts.Enemies;
using Scripts.Items;
using Scripts.Items.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Scripts.Attack
{
    public class AttackSpawner: MonoBehaviour
    {
        public LevelUpItemData LevelUpItemData => _attacker.AttackItemData;

        private float _timeAfterSpawn;
        private EnemyPool _enemyPool;
        private bool _canSpawn = true;
        private TouchAttacker _attacker;
        private List<TouchAttacker> _attackerPool;
        private Item _item;
        private Transform _hero;

        private void FixedUpdate()
        {
            if (!_canSpawn)
                return;

            _timeAfterSpawn += Time.fixedDeltaTime;

            if (_timeAfterSpawn >= _attacker.AttackItemData.SpawnInterval)
                TrySpawn();
        }

        public void Initialize(EnemyPool enemyPool, TouchAttacker attacker, Item item, Transform hero)
        {
            if (enemyPool == null)
                throw new System.ArgumentNullException(nameof(enemyPool));

            if (attacker == null)
                throw new System.ArgumentNullException(nameof(attacker));

            if (item == null)
                throw new System.ArgumentNullException(nameof(item));

            if (hero == null)
                throw new System.ArgumentNullException(nameof(hero));

            gameObject.name = $"AttackSpawner_{attacker.AttackItemData.Name}";

            _enemyPool = enemyPool;
            _attacker = attacker;
            _item = item;
            _hero = hero;
            _attackerPool = new List<TouchAttacker>();

            if (_attacker.AttackItemData.Type == AttackType.Static)
            {
                _canSpawn = false;
                TrySpawn();
            }
        }

        public void Claer()
        {
            foreach (TouchAttacker attacker in _attackerPool)
            {
                attacker.Clear();
                Destroy(attacker.gameObject);
            }

            _attackerPool.Clear();
            Destroy(gameObject);
        }

        public void TryTurnAttacks(Vector2 direction)
        {
            foreach (TouchAttacker attacker in _attackerPool)
            {
                attacker.TryTurnAttacks(direction);
            }
        }

        private void TrySpawn()
        {
            _timeAfterSpawn = 0;

            //if (_attackerPool.Count(attack => attack.gameObject.activeSelf) >= _attacker.AttackItemData.MaxCount)
            //{
            //    return;
            //}

            List<Enemy> nearestEnemies = _item.Type == AttackType.SeekerProjectile 
                ? _enemyPool.GetNearests(_item.ProjectileCount)
                : new List<Enemy>();

            for (int i = 0; i < _item.ProjectileCount; i++)
            {
                Enemy nearestEnemy = i < nearestEnemies.Count ? nearestEnemies[i] : null;
                Transform target = nearestEnemy == null ? null : nearestEnemy.transform;

                TouchAttacker attacker = _attackerPool.FirstOrDefault(attack => !attack.gameObject.activeSelf);

                if (attacker == null)
                {
                    attacker = Instantiate(_attacker, _hero.position, _hero.rotation, transform);
                    _attackerPool.Add(attacker);
                }
                else
                {
                    attacker.transform.position = _hero.position;
                }

                attacker.Initialize(target, _item, _hero, i);
            }
        }
    }
}