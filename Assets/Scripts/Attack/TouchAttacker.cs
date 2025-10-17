using Scripts.Items;
using Scripts.Items.ScriptableObjects;
using System;
using UnityEngine;

namespace Scripts.Attack
{
    [RequireComponent(typeof(Collider2D))]
    public class TouchAttacker : MonoBehaviour
    {
        [field: SerializeField] public AttackItemData AttackItemData { get; private set; }
        [SerializeField] private AttackSprite _attackSprite;

        private Collider2D _collider2D;

        public Item AttackItem { get; private set; }

        private void OnValidate()
        {
            if (AttackItemData == null)
                throw new System.ArgumentNullException(nameof(AttackItemData));
        }

        public virtual void Initialize(Transform direction, Item item, Transform hero, int index)
        {
            _collider2D = GetComponent<Collider2D>();

            AttackItem = item;
            AttackItem.Increased += Increase;

            gameObject.SetActive(true);
        }

        public void TryTurnAttacks(Vector2 direction)
        {
            if (AttackItemData.CanTurn)
            {
                Quaternion rotation = transform.rotation;
                rotation.y = direction.x > 0 ? 0 : 180;
                transform.rotation = rotation;
            }
        }

        public void Clear()
        {
            AttackItem.Increased -= Increase;
        }

        private void Increase(float percent)
        {
            if(_collider2D is CircleCollider2D circle)
            {
                circle.radius = circle.radius * (100f + percent) / 100f;
            }

            _attackSprite.Increase(percent);
        }
    }
}