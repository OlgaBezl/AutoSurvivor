using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] private GameRoot _gameRoot;
    [SerializeField] private LevelUpMenu _levelUpMenu;
    [SerializeField] private AttackerGenerator _attackerGenerator;

    private void Start()
    {
        foreach(BaseAttacker attacker in _attackerGenerator.GetAll())
        {
            LevelUpMenu menuItem = Instantiate(_levelUpMenu, transform);
            menuItem.Initialize(_gameRoot, attacker.AttackItem);
        }
    }
}
