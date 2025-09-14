using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Attack
{
    public class AttackDictionary : MonoBehaviour
    {
        [SerializeField] private BaseAttacker[] _attackers;
        //[SerializeField] private PassiveItem[] _passivePrefabs;

        private IEnumerable<BaseAttackItem> _items;

        private void OnValidate()
        {
            if (_attackers == null)
                throw new System.ArgumentNullException(nameof(_attackers));
        }

        private void Awake()
        {
            _items = _attackers.Select(attack => attack.AttackItem);
        }

        public IEnumerable<BaseAttackItem> GetAll()
        {
            return _items;
        }

        public BaseAttacker GetByItem(LevelUpItem levelUpItem)
        {
            return _attackers.FirstOrDefault(item => item.AttackItem.Equals(levelUpItem));
        }
    }
}
