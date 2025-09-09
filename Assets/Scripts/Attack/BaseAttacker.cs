using UnityEngine;

public abstract class BaseAttacker: MonoBehaviour
{
    [field: SerializeField] public BaseAttackItem AttackItem { get; set; }

    public abstract void Initialize(EnemyPool enemyPool);
}
