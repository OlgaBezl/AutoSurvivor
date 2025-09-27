using UnityEngine;

namespace Scripts.Items.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AttackItem", menuName = "Scriptable Objects/AttackItem")]
    public class AttackItem : BaseAttackItem
    {
        [field: SerializeField] public SuperAttackItem SuperVersion { get; private set; }
        [field: SerializeField] public PassiveItem TupleItem { get; private set; }

    }
}
