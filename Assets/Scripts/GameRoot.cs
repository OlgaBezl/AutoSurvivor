using Scripts.Attack;
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
        [SerializeField] private LevelUpPanel _levelUpPanel;
        [SerializeField] private HeroSelectionPanel _heroSelectionPanel;
        [SerializeField] private GameObject _gameUIPanel;
        [SerializeField] private Progress _progress;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private AttackDictionary _attackDictionary;
        [SerializeField] private EnemyPool _enemyPool;

        private Hero _hero;

        private void OnValidate()
        {
            if (_enemySpawner == null)
                throw new System.ArgumentNullException(nameof(_enemySpawner));

            if (_levelUpPanel == null)
                throw new System.ArgumentNullException(nameof(_levelUpPanel));

            if (_heroSelectionPanel == null)
                throw new System.ArgumentNullException(nameof(_heroSelectionPanel));

            if (_gameUIPanel == null)
                throw new System.ArgumentNullException(nameof(_gameUIPanel));

            if (_progress == null)
                throw new System.ArgumentNullException(nameof(_progress));

            if (_healthBar == null)
                throw new System.ArgumentNullException(nameof(_healthBar));

            if (_enemyPool == null)
                throw new System.ArgumentNullException(nameof(_enemyPool));
        }

        private void OnEnable()
        {
            _progress.LevelUpped += OpenLevelUpPanel;
        }

        private void OnDisable()
        {
            _progress.LevelUpped -= OpenLevelUpPanel;
        }

        public void StartLevel(Hero hero)
        {
            _hero = hero;

            _gameUIPanel.SetActive(true);
            _heroSelectionPanel.Hide();

            _enemySpawner.Initialize(_hero);
            hero.Initialize(_healthBar, _attackDictionary, _enemyPool);

            _enemySpawner.Play();
            _hero.LevelUp(hero.DefaultAttack);
        }

        public void StartLevel(Item levelUpItem)
        {
            _gameUIPanel.SetActive(true);
            _levelUpPanel.Hide();
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