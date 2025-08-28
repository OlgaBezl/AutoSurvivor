using UnityEngine;

public class ProjectileGenerator : BaseAttacker
{
    [SerializeField] private Projectile _projectile; 
    [SerializeField] private float _spawnInterval = 60;

    private float _spawnCounter;
    private EnemyPool _enemyPool;


    private void FixedUpdate()
    {
        _spawnCounter++;

        if(_spawnCounter >= _spawnInterval)
        {
            Spawn();
        }
    }

    public override void Initialize(EnemyPool enemyPool)
    {
        _enemyPool = enemyPool;
    }

    private void Spawn()
    {
        _spawnCounter = 0;
        Enemy nearestEnemy = _enemyPool.GetNearest(transform.position);
        Vector3 direction = nearestEnemy == null ? Vector3.left : nearestEnemy.transform.position;

        Projectile projectile = Instantiate(_projectile, transform.position, transform.rotation, transform);
        projectile.Initialize(direction, LevelUpItem);
    }
}
