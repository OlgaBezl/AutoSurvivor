using UnityEngine;

namespace Scripts.Items.ScriptableObjects
{
    public class LevelUpItemData : BaseIdItem
    {
        [field: SerializeField] public Sprite Sprite;
        [field: SerializeField] public string Name;

        public bool IsAttack { get; protected set; }
        public bool IsPassive => !IsAttack;

        public override string ToString()
        {
            return Name;
        }
    }
}