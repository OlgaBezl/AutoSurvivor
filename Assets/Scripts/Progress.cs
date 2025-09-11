using System;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [field: SerializeField] public float LevelUpValue { get; private set; } = 10f;


    public event Action<int> LevelUpped;
    public event Action<float> ChangeValue;

    private float _currentValue;
    private int _level;

    private void OnValidate()
    {

    }

    private void Awake()
    {
        _currentValue = 0;
        _level = 1;
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
        _level++;

        ChangeValue?.Invoke(_currentValue);
        LevelUpped?.Invoke(_level);
    }
}
