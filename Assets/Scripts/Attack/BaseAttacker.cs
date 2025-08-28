using UnityEngine;

public abstract class BaseAttacker: MonoBehaviour
{
    [field: SerializeField] public LevelUpItem LevelUpItem { get; set; }

    public abstract void Initialize(EnemyPool enemyPool);
}
