using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxValue { get; private set; } = 100f;

    public event Action Death;
    public event Action<float> ChangeValue;

    private float _currentValue;

    private void Awake()
    {
        _currentValue = MaxValue;
    }

    public void Damage(float value)
    {
        _currentValue -= value;
        ChangeValue?.Invoke(_currentValue);

        if (_currentValue <= 0)
        {
            Death?.Invoke();
        }
    }
}
