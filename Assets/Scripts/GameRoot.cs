using Scripts.Enemies;
using Scripts.Heroes;
using Scripts.Items;
using Scripts.UI;
using UnityEngine;

namespace Scripts
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private Hero _hero;
        [SerializeField] private LevelUpPanel _levelUpPanel;
        [SerializeField] private GameObject _gameUIPanel;
        [SerializeField] private Progress _progress;

        private bool _isNew = true;

        private void OnValidate()
        {
            if (_enemySpawner == null)
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

        public void StartLevel(Item levelUpItem)
        {
            _gameUIPanel.SetActive(true);
            _levelUpPanel.Hide();

            if (_isNew)
            {
                _isNew = false;
                _enemySpawner.Initialize();
                _hero.Initialize();
            }

            _enemySpawner.Play();
            _hero.LevelUp(levelUpItem);
        }

        private void OpenLevelUpPanel(int level)
        {
            _gameUIPanel.SetActive(false);
            _levelUpPanel.Show();
            _enemySpawner.Pause();
            _hero.Pause();
        }
    }
}