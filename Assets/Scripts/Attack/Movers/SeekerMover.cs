using Scripts.Items;
using UnityEngine;

namespace Scripts.Attack.Movers
{
    public class SeekerMover : BaseAttackMover
    {
        private Vector3 _direction;
        private Vector3 _directionNormalized;

        private void FixedUpdate()
        {
            if(_direction != Vector3.zero)
                transform.position += _directionNormalized * AttackItem.Speed * Time.fixedDeltaTime;
        }

        public override void Initialize(Transform direction, Item item, int countItems = 0, Transform hero = null)
        {
            base.Initialize(direction, item);
            _direction = direction == null ? Random.insideUnitSphere : direction.position;
            _directionNormalized = (_direction - transform.position).normalized;
        }
    }
}
