using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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

        public List<Enemy> GetNearests(int count)
        { 
            List<Enemy> activeEnemies = _enemies.Where(enemy => enemy.gameObject.activeSelf).OrderBy(enemy => enemy.DistanceToHero).ToList();
                         
            return activeEnemies.Count < count ? activeEnemies : activeEnemies.Take(count).ToList();

            //float minDistance = float.MaxValue;
            //var nearestsEnemy = new List<Enemy>();

            //foreach (Enemy enemy in _enemies.Where(e => e.gameObject.activeSelf))
            //{
            //    float distance = Vector3.Distance(enemy.transform.position, position);

            //    if (distance < minDistance)
            //    {
            //        minDistance = distance;
            //        nearestEnemy = enemy;
            //    }
            //}

            //return nearestsEnemy;
        }

        private int _counter = 0;
    }
}
