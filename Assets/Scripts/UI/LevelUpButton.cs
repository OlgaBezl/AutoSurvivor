using Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class LevelUpButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        private GameRoot _gameRoot;
        private Item _levelUpItem;

        private void OnValidate()
        {
            if (_image == null)
                throw new System.ArgumentNullException(nameof(_image));

            if (_text == null)
                throw new System.ArgumentNullException(nameof(_text));
        }

        public void Initialize(GameRoot gameRoot, Item levelUpItem)
        {
            _gameRoot = gameRoot;
            _levelUpItem = levelUpItem;

            _image.sprite = _levelUpItem.Sprite;
            _text.text = $"{_levelUpItem.NameWithNextLevel}";
        }

        public void SelectItem()
        {
            _levelUpItem.LevelUp();
            _gameRoot.StartLevel(_levelUpItem);
        }
    }
}
