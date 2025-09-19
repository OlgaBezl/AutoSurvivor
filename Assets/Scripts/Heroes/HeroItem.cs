using Scripts.Items.ScriptableObjects;
using System.Collections;
using UnityEngine;

namespace Scripts.Heroes
{
    [CreateAssetMenu(fileName = "HeroItem", menuName = "Scriptable Objects/HeroItem")]
    public class HeroItem : BaseIdItem, IEqualityComparer
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}