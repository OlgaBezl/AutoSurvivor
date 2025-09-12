using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Hero _hero;
    [SerializeField] private LevelUpPanel _levelUpPanel;
    [SerializeField] private GameObject _gameUIPanel;
    [SerializeField] private Progress _progress;

    private void OnValidate()
    {
        if(_enemySpawner == null)
            throw new System.ArgumentNullException(nameof(_enemySpawner));

        if (_hero == null)
            throw new System.ArgumentNullException(nameof(_hero));

        if (_levelUpPanel == null)
            throw new System.ArgumentNullException(nameof(_levelUpPanel));

        if (_gameUIPanel == null)
            throw new System.ArgumentNullException(nameof(_gameUIPanel));

        if (_progress == null)
            throw new System.ArgumentNullException(nameof(_progress));
    }

    private void OnEnable()
    {
        _progress.LevelUpped += OpenLevelUpPanel;
    }

    private void OnDisable()
    {
        _progress.LevelUpped -= OpenLevelUpPanel;
    }

    public void StartLevel(LevelUpItem levelUpItem)
    {
        _gameUIPanel.SetActive(true);
        _levelUpPanel.Hide();
        _enemySpawner.Initialize();
        _hero.Initialize(levelUpItem);
    }

    private void OpenLevelUpPanel(int level)
    {
        _gameUIPanel.SetActive(false);
        _levelUpPanel.Show();
        _enemySpawner.Pause();
        _hero.Pause();
    }
}
