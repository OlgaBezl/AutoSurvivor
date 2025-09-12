using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private BaseCharacter _character;
    [SerializeField] private Slider _slider;

    private Health _health;

    private void OnValidate()
    {
        if (_character == null)
            throw new System.ArgumentNullException(nameof(_character));

        if (_slider == null)
            throw new System.ArgumentNullException(nameof(_slider));
    }

    private void Start()
    {
        _slider.value = 1;

        _health = _character.Health;
        _health.ChangeValue += ChangeValue;
    }

    private void ChangeValue(float value)
    {
        _slider.value = value / _health.MaxValue;
    }
}
