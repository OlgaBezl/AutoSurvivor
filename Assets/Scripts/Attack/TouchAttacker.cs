using Scripts.Items;
using Scripts.Items.ScriptableObjects;
using UnityEngine;

namespace Scripts.Attack
{
    public class TouchAttacker : MonoBehaviour
    {
        [field: SerializeField] public BaseAttackItem AttackItemData { get; private set; }
        public Item AttackItem { get; private set; }

        private void OnValidate()
        {
            if (AttackItemData == null)
                throw new System.ArgumentNullException(nameof(AttackItemData));
        }

        public virtual void Initialize(Transform direction, Item item, Transform hero)
        {
            AttackItem = item;
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
    }
}