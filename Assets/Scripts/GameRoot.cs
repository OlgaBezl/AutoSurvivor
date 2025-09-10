using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Hero _hero;
    [SerializeField] private LevelUpPanel _levelUpPanel;

    private void OnValidate()
    {
        if(_enemySpawner == null)
            throw new System.ArgumentNullException(nameof(_enemySpawner));

        if (_hero == null)
            throw new System.ArgumentNullException(nameof(_hero));

        if (_levelUpPanel == null)
            throw new System.ArgumentNullException(nameof(_levelUpPanel));
    }

    public void StartLevel(LevelUpItem levelUpItem)
    {
        _levelUpPanel.Hide();
        _enemySpawner.Initialize();
        _hero.Initialize(levelUpItem);
    }
}
