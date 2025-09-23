using Scripts.Heroes;
using System.Linq;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy[] _enemyPrefabs;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private int _interval = 100;
        [SerializeField] private Progress _progress;

        private int _counter = 0;
        private bool _isActive = false;
        private Hero _hero;

        private void OnValidate()
        {
            if (_enemyPrefabs == null || !_enemyPrefabs.Any())
                throw new System.ArgumentNullException(nameof(_enemyPrefabs));

            if (_enemyPool == null)
                throw new System.ArgumentNullException(nameof(_enemyPool));

            if (_spawnPoints == null || !_spawnPoints.Any())
                throw new System.ArgumentNullException(nameof(_spawnPoints));

            if (_interval <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(_interval));

            if (_progress == null)
                throw new System.ArgumentNullException(nameof(_progress));
        }

        public void Initialize(Hero hero)
        {
            _hero = hero;
            _hero.HeroDeath += Stop;
        }

        public void Play()
        {
            gameObject.SetActive(true);
            _isActive = true;
        }

        public void Pause()
        {
            gameObject.SetActive(false);
            _isActive = false;
        }

        private void FixedUpdate()
        {
            if (_hero == null || !_isActive)
            {
                return;
            }

            _counter++;

            if (_counter == _interval)
            {
                _counter = 0;
                int spawnPointIndex = Random.Range(0, _spawnPoints.Length);
                int enemyIndex = Random.Range(0, _enemyPrefabs.Length);

                Enemy enemy = _enemyPool.GetEnemy(_enemyPrefabs[enemyIndex], _spawnPoints[spawnPointIndex]);
                enemy.Initialize(_hero.transform);
                enemy.Deathed += EnemyDied;

                _enemyPool.Add(enemy);
            }
        }

        private void EnemyDied(Enemy enemy)
        {
            enemy.Deathed -= EnemyDied;
            _progress.Increase(enemy.EnemyItem.Points);
        }
        private void Stop()
        {
            _isActive = false;
            _enemyPool.StopAll();
        }
    }
}