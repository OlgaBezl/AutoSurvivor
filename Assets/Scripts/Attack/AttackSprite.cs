using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AttackSprite : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Increase(float percent)
    {
        _spriteRenderer.transform.localScale = _spriteRenderer.transform.localScale * (100f + percent) / 100f;
    }
}
