using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    private Health _health;
    private Vector3 _target;
    private Hero _hero;

    private bool _isMove = false;
    private bool isOn = true;

    public void Initialize(Hero hero)
    {
        _hero = hero;

        _target = _hero.transform.position;
        _hero.HeroDeath += Stop;

        _health = GetComponent<Health>();
        _health.Death += Death;

        _isMove = true;
        isOn = true;
    }

    private void Update()
    {
        if(_isMove)
        {
            transform.position += (_target - transform.position).normalized * _speed * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Hero hero))
        {
            hero.Damage(5);
        }
    }

    public void Damage(float value)
    {
        if(isOn)
        {
            _health?.Damage(value);
        }
    }

    private void Death()
    {
        isOn = false;
        _health.Death -= Death;

        //анимаци€ уменьшени€, красный цвет, вернуть в пул
        gameObject.SetActive(false);
    }

    private void Stop()
    {
        _isMove = false;
        _hero.HeroDeath -= Stop;
    }
}
