using Scripts.Healths;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        private Slider _slider;
        private Health _health;

        private void OnDisable()
        {
            if (_health != null)
                _health.ChangeValue -= ChangeValue;
        }

        public void Initialize(Health health)
        {
            _slider = GetComponent<Slider>();
            _slider.value = 1;

            _health = health;
            _health.ChangeValue += ChangeValue;
        }

        private void ChangeValue(float value)
        {
            _slider.value = value / _health.MaxValue;
        }
    }
}
