using Scripts.Heroes;
using System;
using UnityEngine;

namespace Scripts.Enemies
{
    public class Enemy : BaseCharacter
    {
        [field: SerializeField] public EnemyItem EnemyItem { get; private set; }

        public event Action<Enemy> Deathed;

        private Transform _target;
        private bool _isMove = false;
        private bool isOn = true;

        public void Initialize(Transform hero)
        {
            _target = hero;

            base.Initialize(EnemyItem.Health);
            Health.Deathed += Unactive;

            _isMove = true;
            isOn = true;
        }

        private void FixedUpdate()
        {
            if (_isMove)
            {
                transform.position += (_target.position - transform.position).normalized * EnemyItem.Speed * Time.fixedDeltaTime;
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
            if (isOn)
            {
                Health?.Damage(value);
            }
        }

        public void Stop()
        {
            _isMove = false;
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
    }
}
