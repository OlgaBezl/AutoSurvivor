using UnityEngine;

namespace Scripts.Attack
{
    [RequireComponent (typeof(BaseAttackMover))]
    public class Projectile : BaseAttacker
    {
        private BaseAttackMover _mover;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Damage(AttackItem.Attack);
                gameObject.SetActive(false);
            }
        }

        public override void Initialize(Vector3 direction)
        {
            _mover = GetComponent<BaseAttackMover>();
            _mover.Initialize(direction, AttackItem);
        }
    }
}