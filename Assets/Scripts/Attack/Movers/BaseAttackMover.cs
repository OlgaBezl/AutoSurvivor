using Scripts.Items;
using System;
using UnityEngine;

namespace Scripts.Attack.Movers
{
    public class BaseAttackMover : MonoBehaviour
    {
        public Item AttackItem { get; private set; }

        public event Action UnActived;

        public virtual void Initialize(Transform direction, Item item, int countItems = 0, Transform hero = null)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            AttackItem = item;
        }

        protected void Unactive()
        {
            UnActived?.Invoke();
        }
    }
}