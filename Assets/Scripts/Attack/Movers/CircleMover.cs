using Scripts.Items;
using UnityEngine;

namespace Scripts.Attack.Movers
{
    public class CircleMover : BaseAttackMover
    {
        private float _currentAngle;
        private int _maxRoundCount = 3;
        private float _maxAngle;
        private Transform _hero;

        private void FixedUpdate()
        {
            if(_currentAngle < _maxAngle)
            {
                _currentAngle += Time.fixedDeltaTime * AttackItem.Speed;
                Vector3 direction = Quaternion.AngleAxis(_currentAngle, Vector3.forward) * Vector3.up;
                transform.position = _hero.position + direction * AttackItem.Radius;
            }
            else
            {
                Unactive();
            }
        }

        public override void Initialize(Transform direction, Item item, int countItems = 0, Transform hero = null)
        {
            base.Initialize(direction, item);
            _hero = hero;
            _currentAngle = 0;
            _maxAngle = _maxRoundCount * 360f;
        }
    }
}
