using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
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
                Spawn();
        }

        public void Initialize(EnemyPool enemyPool, BaseAttacker attacker)
        {
            if (enemyPool == null)
                throw new System.ArgumentNullException(nameof(enemyPool));

            if (attacker == null)
                throw new System.ArgumentNullException(nameof(attacker));

            _enemyPool = enemyPool;
            _attacker = attacker;
            _attackerPool = new List<BaseAttacker>();

            if (_attacker.AttackItem.Type == AttackType.Static)
            {
                _canSpawn = false;
                Spawn();
            }
        }

        private void Spawn()
        {
            _spawnCounter = 0;
            Enemy nearestEnemy = _enemyPool.GetNearest(transform.position);
            Vector3 direction = nearestEnemy == null ? Vector3.left : nearestEnemy.transform.position;

            BaseAttacker attacker = _attackerPool.FirstOrDefault(attack => !attack.gameObject.activeSelf && attack.AttackItem.Type == _attacker.AttackItem.Type);

            if(attacker == null)
                attacker = Instantiate(_attacker, transform.position, transform.rotation, transform);

            attacker.Initialize(direction);
        }
    }
}