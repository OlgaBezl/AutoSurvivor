using Scripts.Heroes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class HeroSelectionPanel : MonoBehaviour
    {
        [SerializeField] private HeroSelectionButton _heroSelectionButton;
        [SerializeField] private HeroSpawner _heroSpawner;
        [SerializeField] private Transform _container;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        private List<HeroSelectionButton> _buttonsList;

        private void OnValidate()
        {
            if (_heroSelectionButton == null)
                throw new System.ArgumentNullException(nameof(_heroSelectionButton));

            if (_heroSpawner == null)
                throw new System.ArgumentNullException(nameof(_heroSpawner));

            if (_container == null)
                throw new System.ArgumentNullException(nameof(_container));

            if (_descriptionText == null)
                throw new System.ArgumentNullException(nameof(_descriptionText));
        }

        private void Start()
        {
            Show();
        }

        public void Show()
        {
            _buttonsList = new List<HeroSelectionButton>();

            foreach (Hero hero in _heroSpawner.GetAll())
            {
                HeroSelectionButton button = Instantiate(_heroSelectionButton, _container);
                button.Initialize(hero.HeroItem);
                button.HeroSelected += UpdateHeroInfo;

                _buttonsList.Add(button);
            }

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);

            foreach (HeroSelectionButton button in _buttonsList)
            {
                button.HeroSelected -= UpdateHeroInfo;
            }
        }

        private void UpdateHeroInfo(HeroItem heroItem)
        {
            _descriptionText.text = heroItem.Description;
        }
    }
}
