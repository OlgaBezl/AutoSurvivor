using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Attaker : BaseAttacker
{
    public override void Initialize(EnemyPool enemyPool)
    {
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(LevelUpItem.AttackValue);
        }
    }
}
