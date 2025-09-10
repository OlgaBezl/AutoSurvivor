using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    private void OnValidate()
    {
        if (_health == null)
            throw new System.ArgumentNullException(nameof(_health));

        if (_slider == null)
            throw new System.ArgumentNullException(nameof(_slider));
    }

    private void Awake()
    {
        _slider.value = 1;
        _health.ChangeValue += ChangeValue;
    }

    private void ChangeValue(float value)
    {
        _slider.value = value / _health.MaxValue;
    }
}
