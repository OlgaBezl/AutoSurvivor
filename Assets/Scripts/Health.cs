using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxValue { get; private set; }

    public event Action Deathed;
    public event Action<float> ChangeValue;

    private float _currentValue;

    public void Initialize(float maxValue)
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
