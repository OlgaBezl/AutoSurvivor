using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthSprite : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Color _damageColor = Color.red;

    private Color _defaultColor = Color.white;
    private SpriteRenderer _spriteRenderer;
    private int _damageCounter;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health.ChangeValue += Damage;
    }

    private void Update()
    {
        _damageCounter--;

        if (_damageCounter == 0)
        {
            ColorSprite(false);
        }
    }

    public void Damage(float value)
    {
        ColorSprite(true);
        _damageCounter = 3;
    }

    private void ColorSprite(bool damage)
    {
        if(damage && _spriteRenderer.color != _damageColor)
            _spriteRenderer.color = _damageColor;

        if (!damage && _spriteRenderer.color != _defaultColor)
            _spriteRenderer.color = _defaultColor;
    }
}
