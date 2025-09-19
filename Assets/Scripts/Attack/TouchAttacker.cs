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

        public virtual void Initialize(Vector3 direction, Item item)
        {
            AttackItem = item;
            gameObject.SetActive(true);
        }
    }
}