using System;

namespace Scripts.Healths
{
    public class Health
    {
        public float MaxValue { get; private set; }
        public bool IsDead => _currentValue <= 0;

        public event Action Deathed;
        public event Action<float> ChangeValue;

        private float _currentValue;

        public Health(float maxValue)
        {
            MaxValue = maxValue;
            _currentValue = maxValue;
        }

        public void Damage(float value)
        {
            _currentValue -= value;
            ChangeValue?.Invoke(_currentValue);

            if (_currentValue <= 0)
            {
                Deathed?.Invoke();
            }
        }
    }
}