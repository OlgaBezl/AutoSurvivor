using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Hero _hero;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _interval = 100;

    private int _counter = 0;

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if(_hero == null)
        {
            return;
        }

        _counter++;

        if(_counter == _interval)
        {
            int spawnPointIndex = Random.Range(0, _spawnPoints.Length - 1);
            int enemyIndex = Random.Range(0, _enemyPrefabs.Length - 1);

            _counter = 0;
            Enemy enemy = Instantiate(_enemyPrefabs[enemyIndex], _spawnPoints[spawnPointIndex].position, _spawnPoints[spawnPointIndex].rotation, transform);
            enemy.Initialize(_hero);
            _enemyPool.Add(enemy);
        }
    }
}
