using System;
using UnityEngine;

namespace Scripts
{
    public class Progress : MonoBehaviour
    {
        [field: SerializeField] public float LevelUpValue { get; private set; } = 10f;

        public int Level { get; private set; } = 1;

        public event Action<int> LevelUpped;
        public event Action<float> ChangeValue;

        private float _currentValue = 0;

        private void OnValidate()
        {

        }

        public void Increase(float value)
        {
            _currentValue += value;
            ChangeValue?.Invoke(_currentValue);

            if (_currentValue >= LevelUpValue)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _currentValue = 0;
            Level++;

            ChangeValue?.Invoke(_currentValue);
            LevelUpped?.Invoke(Level);
        }
    }
}