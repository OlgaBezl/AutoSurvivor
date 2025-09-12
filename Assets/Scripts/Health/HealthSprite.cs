using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthSprite : MonoBehaviour
{
    [SerializeField] private Color _damageColor = Color.red;

    private Color _defaultColor = Color.white;
    private SpriteRenderer _spriteRenderer;
    private int _damageCounter;
    private BaseCharacter _character;
    private Health _health;

    private void OnValidate()
    {
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _character = GetComponentInParent<BaseCharacter>();

        _health = _character.Health;
        _health.ChangeValue += Damage;
    }

    private void FixedUpdate()
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
