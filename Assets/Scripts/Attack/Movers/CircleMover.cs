using Scripts.Items;
using UnityEngine;

namespace Scripts.Attack.Movers
{
    public class CircleMover : BaseAttackMover
    {
        private float _currentAngle;
        //private int _maxRoundCount = 3;
        //private float _maxAngle;
        private Transform _hero;
        private float _time;

        private void FixedUpdate()
        {
            if(_time < AttackItem.LifeTime)
            {
                _time += Time.fixedDeltaTime;
                _currentAngle += Time.fixedDeltaTime * AttackItem.Speed;
                Vector3 direction = Quaternion.AngleAxis(_currentAngle, Vector3.forward) * Vector3.up;
                transform.position = _hero.position + direction * AttackItem.Radius;
                transform.rotation = Quaternion.AngleAxis(_currentAngle, Vector3.forward);
            }
            else
            {
                Unactive();
            }
        }

        public override void Initialize(Transform direction, Item item, Transform hero, int index)
        {
            base.Initialize(direction, item);
            _hero = hero;
            _time = 0;
            _currentAngle = 360f / item.ProjectileCount * index;
            //_maxAngle = _maxRoundCount * 360f;
        }
    }
}
