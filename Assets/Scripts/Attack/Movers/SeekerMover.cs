using UnityEngine;

namespace Scripts.Attack.Movers
{
    public class SeekerMover : BaseAttackMover
    {
        private Vector3 _direction;

        private void FixedUpdate()
        {
            transform.position += _direction * AttackItem.Speed * Time.fixedDeltaTime;
        }

        public override void Initialize(Vector3 direction, BaseAttackItem item)
        {
            base.Initialize(direction, item);
            _direction = direction.normalized;
        }
    }
}
