using Scripts.Attack.Movers;
using Scripts.Enemies;
using Scripts.Items;
using System.Collections;
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

        public override void Initialize(Transform direction, Item item, Transform hero, int index)
        {
            _mover = GetComponent<BaseAttackMover>();
            _mover.Initialize(direction, item, hero, index);
            _mover.UnActived += Unactive;
            base.Initialize(direction, item, hero, index);

            if(AttackItem.LifeTime > 0)
                StartCoroutine(UnactiveAfterTime());
        }

        private IEnumerator UnactiveAfterTime()
        {
            yield return new WaitForSeconds(AttackItem.LifeTime);
            Unactive();
        }

        private void Unactive()
        {
            _mover.UnActived -= Unactive;
            gameObject.SetActive(false);
        }
    }
}