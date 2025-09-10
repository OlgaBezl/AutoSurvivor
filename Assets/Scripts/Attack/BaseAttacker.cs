using UnityEngine;

public abstract class BaseAttacker: MonoBehaviour
{
    [field: SerializeField] public BaseAttackItem AttackItem { get; set; }

    private void OnValidate()
    {
        if (AttackItem == null)
            throw new System.ArgumentNullException(nameof(AttackItem));
    }

    public abstract void Initialize(EnemyPool enemyPool);
}
