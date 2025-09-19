using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EnemyPool : MonoBehaviour
    {
        private List<Enemy> _enemies;

        private void Awake()
        {
            _enemies = new List<Enemy>();
        }

        public Enemy GetEnemy(Enemy prefab, Transform spawnPoint)
        {
            Enemy inactiveEnemy = _enemies.FirstOrDefault(enemy => !enemy.gameObject.activeSelf && enemy.EnemyItem.Equals(prefab.EnemyItem));

            if (inactiveEnemy == null)
            {
                return Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, transform);
            }
            else
            {
                inactiveEnemy.transform.position = spawnPoint.position;
                inactiveEnemy.transform.rotation = spawnPoint.rotation;
                inactiveEnemy.gameObject.SetActive(true);
                return inactiveEnemy;
            }
        }

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void StopAll()
        {
            _enemies.ForEach(enemy => enemy.Stop());
        }

        public Enemy GetNearest(Vector3 position)
        {
            float minDistance = float.MaxValue;
            Enemy nearestEnemy = null;

            foreach (Enemy enemy in _enemies.Where(e => e.gameObject.activeSelf))
            {
                float distance = Vector3.Distance(enemy.transform.position, position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            return nearestEnemy;
        }
    }
}
