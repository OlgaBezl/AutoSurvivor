using Scripts.Items;
using UnityEngine;

namespace Scripts.Attack.Movers
{
    public class BrunchMover : BaseAttackMover
    {
        private Vector3 _startPosition;
        private Vector3 _direction;
        private float _progress = 0f;
        private float _arcHeight;
        private bool _isActive = false;

        public override void Initialize(Transform direction, Item item, int countItems = 0, Transform hero = null)
        {
            base.Initialize(direction, item);

            _startPosition = transform.position;

            float randomSpread = Random.Range(-item.Radius - item.RadiusVariation, item.Radius + item.RadiusVariation);
            _direction = Quaternion.Euler(0f, randomSpread, 0f) * transform.forward;

            // Случайная высота дуги
            _arcHeight = Random.Range(item.Height - item.HeightVariation, item.Height - item.HeightVariation);
            _arcHeight = Mathf.Max(0.1f, _arcHeight);

            _progress = 0f;
            _isActive = true;
        }

        void FixedUpdate()
        {
            if (!_isActive)
            {
                return;
            }

            if (_progress < 1f)
            {
                _progress += AttackItem.Speed * Time.fixedDeltaTime / AttackItem.Distance;
                Vector3 currentPos = CalculateParabolicPosition(_progress);
                transform.position = currentPos;
            }
            else
            {
                Unactive();
            }
        }

        Vector3 CalculateParabolicPosition(float progress)
        {
            Vector3 linearPos = _startPosition + _direction * (AttackItem.Distance * progress);
            float parabola = -4f * _arcHeight * (progress - 0.5f) * (progress - 0.5f) + _arcHeight;
            Vector3 parabolicPos = linearPos + Vector3.up * parabola;

            return parabolicPos;
        }
    }
}
