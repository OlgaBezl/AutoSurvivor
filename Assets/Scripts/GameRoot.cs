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
        [SerializeField] private Progress _progress;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private AttackDictionary _attackDictionary;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private HeroMover _mover;

        [Header("Panels")]
        [SerializeField] private LevelUpPanel _levelUpPanel;
        [SerializeField] private HeroSelectionPanel _heroSelectionPanel;
        [SerializeField] private GameObject _gameUIPanel;
        [SerializeField] private DeathPanel _deathPanel;

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

            if (_mover == null)
                throw new System.ArgumentNullException(nameof(_mover));
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
            Debug.Log("---   GameRoot StartLevel   ---");
            Debug.Log($"hero: {hero?.gameObject?.name}");
            _hero = hero;
            _hero.HeroDeath += ShowDeathPanel;

            _gameUIPanel.SetActive(true);
            _heroSelectionPanel.Hide();

            _enemySpawner.Initialize(_hero);
            hero.Initialize(_healthBar, _attackDictionary, _enemyPool, _mover);

            _enemySpawner.Play();
            _hero.LevelUp(hero.DefaultAttack);

            _mover.StartMove();
        }

        public void ContinueLevel(Item levelUpItem)
        {
            Debug.Log("---   GameRoot ContinueLevel   ---");
            Debug.Log($"levelUpItem: {levelUpItem?.NameWithNextLevel}");
            _gameUIPanel.SetActive(true);
            _levelUpPanel.Hide();
            _enemySpawner.Play();
            _hero.LevelUp(levelUpItem);

            _mover.StartMove();
        }

        private void OpenLevelUpPanel(int level)
        {
            _mover.Stop();
            _gameUIPanel.SetActive(false);
            _levelUpPanel.Show();
            _enemySpawner.Pause();
            _hero.Pause();
        }

        private void ShowDeathPanel()
        {
            _hero.HeroDeath -= ShowDeathPanel;

            _mover.Stop();
            _gameUIPanel.SetActive(false);
            _deathPanel.Show();
            _enemySpawner.Pause();
            _hero.Pause();
        }
    }
}