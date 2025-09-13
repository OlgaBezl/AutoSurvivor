using System;
using UnityEngine;

public class Enemy : BaseCharacter
{
    [field: SerializeField] public EnemyItem EnemyItem { get; private set; }

    public event Action<Enemy> Deathed;

    private Vector3 _target;
    private Hero _hero;
    private bool _isMove = false;
    private bool isOn = true;

    public void Initialize(Hero hero)
    {
        _hero = hero;

        _target = _hero.transform.position;
        _hero.HeroDeath += Stop;

        Health = new Health(EnemyItem.Health);
        Health.Deathed += Unactive;

        base.Initialize();

        _isMove = true;
        isOn = true;
    }

    private void FixedUpdate()
    {
        if(_isMove)
        {
            transform.position += (_target - transform.position).normalized * EnemyItem.Speed * Time.deltaTime;
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
            Health?.Damage(value);
        }
    }

    protected override void Death()
    {
        base.Death();
        Deathed?.Invoke(this);
    }

    private void Unactive()
    {
        isOn = false;
        _isMove = false;
        Health.Deathed -= Unactive;
    }

    private void Stop()
    {
        _isMove = false;
        _hero.HeroDeath -= Stop;
    }
}
