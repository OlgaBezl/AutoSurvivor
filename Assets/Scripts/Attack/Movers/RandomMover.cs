using Scripts.Items;
using UnityEngine;

namespace Scripts.Attack.Movers
{
    public class RandomMover : BaseAttackMover
    {
        private Vector3 _directionNormalized;

        private void FixedUpdate()
        {
            if(_directionNormalized != Vector3.zero)
                transform.position += _directionNormalized * AttackItem.Speed * Time.fixedDeltaTime;
        }

        public override void Initialize(Transform direction, Item item, Transform hero, int index)
        {
            base.Initialize(direction, item);

            _directionNormalized = new Vector3(GetRandomCoordinate(), GetRandomCoordinate(), 0).normalized;
        }

        private float GetRandomCoordinate()
        {
            return Random.value > 0.5 ? Random.value : -Random.value;
        }
    }
}
