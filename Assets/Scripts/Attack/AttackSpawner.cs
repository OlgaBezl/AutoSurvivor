using Scripts.Enemies;
using Scripts.Items;
using Scripts.Items.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Attack
{
    public class AttackSpawner: MonoBehaviour
    {
        public LevelUpItemData LevelUpItemData => _attacker.AttackItemData;

        private float _spawnCounter;
        private EnemyPool _enemyPool;
        private bool _canSpawn = true;
        private TouchAttacker _attacker;
        private List<TouchAttacker> _attackerPool;
        private Item _item;

        private void FixedUpdate()
        {
            if (!_canSpawn)
                return;

            _spawnCounter++;

            if (_spawnCounter >= _attacker.AttackItemData.SpawnInterval)
                TrySpawn();
        }

        public void Initialize(EnemyPool enemyPool, TouchAttacker attacker, Item item)
        {
            if (enemyPool == null)
                throw new System.ArgumentNullException(nameof(enemyPool));

            if (attacker == null)
                throw new System.ArgumentNullException(nameof(attacker));

            if (item == null)
                throw new System.ArgumentNullException(nameof(item));

            gameObject.name = $"AttackSpawner_{attacker.AttackItemData.Name}";

            _enemyPool = enemyPool;
            _attacker = attacker;
            _item = item;
            _attackerPool = new List<TouchAttacker>();

            if (_attacker.AttackItemData.Type == AttackType.Static)
            {
                _canSpawn = false;
                TrySpawn();
            }
        }

        private void TrySpawn()
        {
            _spawnCounter = 0;

            if (_attackerPool.Count(attack => attack.gameObject.activeSelf) >= _attacker.AttackItemData.MaxCount)
            {
                return;
            }

            Enemy nearestEnemy = _enemyPool.GetNearest(transform.position);
            Vector3 direction = nearestEnemy == null ? Vector3.left : nearestEnemy.transform.position;

            TouchAttacker attacker = _attackerPool.FirstOrDefault(attack => !attack.gameObject.activeSelf);

            if (attacker == null)
            {
                attacker = Instantiate(_attacker, transform.position, transform.rotation, transform);
                _attackerPool.Add(attacker);
            }
            else
            {
                attacker.transform.position = Vector3.zero;
                attacker.gameObject.SetActive(true);
            }
            
            attacker.Initialize(direction, _item);
        }
    }
}