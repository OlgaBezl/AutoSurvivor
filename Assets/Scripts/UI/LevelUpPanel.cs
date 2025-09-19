using Scripts.Attack;
using Scripts.Items;
using UnityEngine;

namespace Scripts.UI
{
    public class LevelUpPanel : MonoBehaviour
    {
        [SerializeField] private GameRoot _gameRoot;
        [SerializeField] private LevelUpButton _levelUpMenu;
        [SerializeField] private AttackDictionary _attackSpawner;
        [SerializeField] private int _itemsCount;
        [SerializeField] private Transform _container;

        private void OnValidate()
        {
            if (_gameRoot == null)
                throw new System.ArgumentNullException(nameof(_gameRoot));

            if (_levelUpMenu == null)
                throw new System.ArgumentNullException(nameof(_levelUpMenu));

            if (_attackSpawner == null)
                throw new System.ArgumentNullException(nameof(_attackSpawner));

            if (_itemsCount <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(_itemsCount));
        }

        private void Start()
        {
            Show();
        }

        public void Show()
        {
            foreach (Item item in _attackSpawner.GetAll())
            {
                LevelUpButton menuItem = Instantiate(_levelUpMenu, _container);
                menuItem.Initialize(_gameRoot, item);
            }

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);

            while (_container.childCount > 0)
            {
                DestroyImmediate(_container.GetChild(0).gameObject);
            }
        }
    }
}
