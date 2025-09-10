using UnityEngine;

public class ProjectileSpawner : BaseAttacker
{
    [SerializeField] private SeekerProjectile _seekerProjectile;
    [SerializeField] private CircleProjectile _circleProjectile;
    [SerializeField] private int _maxCount;

    private float _spawnCounter;
    private EnemyPool _enemyPool;

    private void OnValidate()
    {
        if (_seekerProjectile == null && _circleProjectile == null)
            throw new System.ArgumentNullException($"{nameof(_seekerProjectile)} or {nameof(_circleProjectile)}");

        if(_maxCount <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(_maxCount));
    }

    private void FixedUpdate()
    {
        _spawnCounter++;

        if(_spawnCounter >= AttackItem.SpawnInterval)
        {
            Spawn();
        }
    }

    public override void Initialize(EnemyPool enemyPool)
    {
        if (enemyPool == null)
            throw new System.ArgumentNullException(nameof(enemyPool));

        _enemyPool = enemyPool;
    }

    private void Spawn()
    {
        _spawnCounter = 0;
        Enemy nearestEnemy = _enemyPool.GetNearest(transform.position);
        Vector3 direction = nearestEnemy == null ? Vector3.left : nearestEnemy.transform.position;

        if(AttackItem.Type == AttackType.Seeker)
        {
            SeekerProjectile projectile = Instantiate(_seekerProjectile, transform.position, transform.rotation, transform);
            projectile.Initialize(direction, AttackItem);
        }

        if (AttackItem.Type == AttackType.CircleProjectile)
        {
            CircleProjectile circleAttacker = Instantiate(_circleProjectile, transform.position, transform.rotation, transform);
            circleAttacker.Initialize(direction, AttackItem);
        }
    }
}