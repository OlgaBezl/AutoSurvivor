using UnityEngine;

namespace Scripts.Attack.Movers
{
    public class CircleMover : BaseAttackMover
    {
        private float _currentAngle;
        private int _maxRoundCount = 3;
        private float _maxAngle;
        private Vector3 _center = Vector3.zero;

        private void FixedUpdate()
        {
            if(_currentAngle < _maxAngle)
            {
                _currentAngle += Time.fixedDeltaTime * AttackItem.Speed;
                Vector3 direction = Quaternion.AngleAxis(_currentAngle, Vector3.forward) * Vector3.up;
                transform.position = _center + direction * AttackItem.Radius;
            }
            else
            {
                Unactive();
            }
        }

        public override void Initialize(Vector3 direction, BaseAttackItem item, int countItems = 0)
        {
            base.Initialize(direction, item);
            _currentAngle = 0;
            _maxAngle = _maxRoundCount * 360f;
        }
    }
}
