using UnityEngine;

namespace Scripts.Items.ScriptableObjects
{
    public class LevelUpItemData : BaseIdItem
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public bool IsAttack { get; protected set; } = true;

        public bool IsPassive => !IsAttack;

        public override string ToString()
        {
            return Name;
        }
    }
}