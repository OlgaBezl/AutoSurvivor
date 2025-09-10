using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    private GameRoot _gameRoot;
    private LevelUpItem _levelUpItem;

    private void OnValidate()
    {
        if (_image == null)
            throw new System.ArgumentNullException(nameof(_image));

        if (_text == null)
            throw new System.ArgumentNullException(nameof(_text));
    }

    public void Initialize(GameRoot gameRoot, LevelUpItem levelUpItem)
    {
        _gameRoot = gameRoot;
        _levelUpItem = levelUpItem;

        _image.sprite = _levelUpItem.Sprite;
        _text.text = _levelUpItem.Name;
    }

    public void SelectItem()
    {
        _gameRoot.StartLevel(_levelUpItem);
    }
}
