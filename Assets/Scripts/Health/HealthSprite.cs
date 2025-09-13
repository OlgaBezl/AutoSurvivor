using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthSprite : MonoBehaviour
{
    [SerializeField] private Color _damageColor = Color.red;

    public event Action DeathAnimationFinished;

    private Color _defaultColor = Color.white;
    private SpriteRenderer _spriteRenderer;
    private int _damageCounter;
    private Health _health;
    public void Initialize(Health health)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _damageCounter = -1;
        _health = health;
        _health.ChangeValue += Damage;
        _health.Deathed += Death;
        ColorSprite(false);
    }

    private void FixedUpdate()
    {
        if(_damageCounter > 0)
        {
            _damageCounter--;
        }
        else if (_damageCounter == 0)
        {
            if (_health.IsDead)
            {
                _damageCounter--;
                DeathAnimationFinished?.Invoke();
            }
            else
            {
                ColorSprite(false);
                _damageCounter--;
            }
        }
    }

    public void Damage(float value)
    {
        ColorSprite(true);
        _damageCounter = 5;
    }

    private void Death()
    {
        _health.ChangeValue -= Damage;
        _health.Deathed -= Death;

        _damageCounter = 10;
        _spriteRenderer.color = Color.black;
    }

    private void ColorSprite(bool damage)
    {
        if(damage && _spriteRenderer.color != _damageColor)
            _spriteRenderer.color = _damageColor;

        if (!damage && _spriteRenderer.color != _defaultColor)
            _spriteRenderer.color = _defaultColor;
    }
}
