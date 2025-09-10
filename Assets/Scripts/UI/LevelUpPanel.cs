using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private GameRoot _gameRoot;
    [SerializeField] private LevelUpMenu _levelUpMenu;
    [SerializeField] private AttackSpawner _attackSpawner;
    [SerializeField] private int _itemsCount;

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
        foreach(BaseAttacker attacker in _attackSpawner.GetAll())
        {
            LevelUpMenu menuItem = Instantiate(_levelUpMenu, transform);
            menuItem.Initialize(_gameRoot, attacker.AttackItem);
        }
    }
}
