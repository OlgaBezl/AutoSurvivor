using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private List<Enemy> _enemies;

    private void Awake()
    {
        _enemies = new List<Enemy>();
    }

    public void Add(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public Enemy GetNearest(Vector3 position)
    {
        float minDistance = float.MaxValue;
        Enemy nearestEnemy = null;

        foreach(Enemy enemy in _enemies.Where(e => e.gameObject.activeSelf))
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
