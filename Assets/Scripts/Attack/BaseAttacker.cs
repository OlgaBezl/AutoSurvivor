using UnityEngine;

namespace Scripts.Attack
{
    public class BaseAttacker : MonoBehaviour
    {
        [field: SerializeField] public BaseAttackItem AttackItem { get; private set; }

        private void OnValidate()
        {
            if (AttackItem == null)
                throw new System.ArgumentNullException(nameof(AttackItem));
        }

        public virtual void Initialize(Vector3 direction)
        {
            gameObject.SetActive(true);
        }
    }
}