using Scripts.Attack.Movers;
using Scripts.Enemies;
using Scripts.Items;
using UnityEngine;

namespace Scripts.Attack
{
    [RequireComponent (typeof(BaseAttackMover))]
    public class Projectile : TouchAttacker
    {
        private BaseAttackMover _mover;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Damage(AttackItem.Attack);
                Unactive();
            }
        }

        public override void Initialize(Vector3 direction, Item item)
        {
            _mover = GetComponent<BaseAttackMover>();
            _mover.Initialize(direction, item);
            _mover.UnActived += Unactive;
            base.Initialize(direction, item);
        }

        private void Unactive()
        {
            _mover.UnActived -= Unactive;
            gameObject.SetActive(false);
        }
    }
}