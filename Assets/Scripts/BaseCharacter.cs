using System;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] private HealthSprite _healthSprite;

    public Health Health { get; protected set; }

    private void OnValidate()
    {
        if (_healthSprite == null)
            throw new ArgumentNullException(nameof(_healthSprite));
    }

    public void Initialize()
    {
        _healthSprite.Initialize(Health);
        _healthSprite.DeathAnimationFinished += Death;
    }

    protected virtual void Death()
    {
        _healthSprite.DeathAnimationFinished -= Death;
        gameObject.SetActive(false);
    }
}
