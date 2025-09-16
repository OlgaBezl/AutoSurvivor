using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Attack
{
    public class AttackSpawner: MonoBehaviour
    {
        private float _spawnCounter;
        private EnemyPool _enemyPool;
        private bool _canSpawn = true;
        private BaseAttacker _attacker;
        private List<BaseAttacker> _attackerPool;

        private void FixedUpdate()
        {
            if (!_canSpawn)
                return;

            _spawnCounter++;

            if (_spawnCounter >= _attacker.AttackItem.SpawnInterval)
                TrySpawn();
        }

        public void Initialize(EnemyPool enemyPool, BaseAttacker attacker)
        {
            if (enemyPool == null)
                throw new System.ArgumentNullException(nameof(enemyPool));

            if (attacker == null)
                throw new System.ArgumentNullException(nameof(attacker));

            gameObject.name = $"AttackSpawner_{attacker.AttackItem.Name}";

            _enemyPool = enemyPool;
            _attacker = attacker;
            _attackerPool = new List<BaseAttacker>();

            if (_attacker.AttackItem.Type == AttackType.Static)
            {
                _canSpawn = false;
                TrySpawn();
            }
        }

        private void TrySpawn()
        {
            _spawnCounter = 0;

            if (_attackerPool.Count(attack => attack.gameObject.activeSelf) >= _attacker.AttackItem.MaxCount)
            {
                return;
            }

            Enemy nearestEnemy = _enemyPool.GetNearest(transform.position);
            Vector3 direction = nearestEnemy == null ? Vector3.left : nearestEnemy.transform.position;

            BaseAttacker attacker = _attackerPool.FirstOrDefault(attack => !attack.gameObject.activeSelf);

            if (attacker == null)
            {
                attacker = Instantiate(_attacker, transform.position, transform.rotation, transform);
                _attackerPool.Add(attacker);
            }
            else
            {
                attacker.gameObject.SetActive(true);
            }
            
            attacker.Initialize(direction);
        }
    }
}