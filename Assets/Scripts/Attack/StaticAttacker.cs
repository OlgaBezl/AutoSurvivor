using UnityEngine;

namespace Scripts.Attack
{
    [RequireComponent(typeof(Collider2D))]
    public class StaticAttacker : BaseAttacker
    {
        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Damage(AttackItem.Attack);
            }
        }
    }
}