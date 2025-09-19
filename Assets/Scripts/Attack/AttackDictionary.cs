using Scripts.Items;
using Scripts.Items.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Attack
{
    public class AttackDictionary : MonoBehaviour
    {
        [SerializeField] private TouchAttacker[] _attackers;
        //[SerializeField] private PassiveItem[] _passivePrefabs;

        private List<Item> _items;

        private void OnValidate()
        {
            if (_attackers == null)
                throw new System.ArgumentNullException(nameof(_attackers));
        }

        private void Awake()
        {
            _items = new List<Item>();

            foreach (var dataItem in _attackers.Select(attack => attack.AttackItemData))
            {
                _items.Add(new Item(dataItem));
            }                
        }

        public IEnumerable<Item> GetAll()
        {
            return _items;
        }

        public TouchAttacker GetByItem(LevelUpItemData levelUpItem)
        {
            return _attackers.FirstOrDefault(item => item.AttackItemData.Equals(levelUpItem));
        }
    }
}
