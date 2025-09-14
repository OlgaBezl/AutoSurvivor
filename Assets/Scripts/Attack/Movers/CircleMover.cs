using UnityEngine;

namespace Scripts.Attack.Movers
{
    public class CircleMover : BaseAttackMover
    {
        private Vector3 _center = Vector3.zero;
        private float _currentAngle;

        private void FixedUpdate()
        {
            _currentAngle += Time.fixedDeltaTime * AttackItem.Speed;
            Vector3 direction = Quaternion.AngleAxis(_currentAngle, Vector3.forward) * Vector3.up;
            transform.position = _center + direction * AttackItem.Radius;
        }
    }
}
