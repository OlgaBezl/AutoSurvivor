using Scripts.Items.ScriptableObjects;
using UnityEngine;

namespace Scripts.Enemies
{
    [CreateAssetMenu(fileName = "EnemyItem", menuName = "Scriptable Objects/EnemyItem")]
    public class EnemyItem : BaseIdItem
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Points { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
