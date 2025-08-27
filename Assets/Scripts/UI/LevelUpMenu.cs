using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private GameRoot _gameRoot;
    [SerializeField] private LevelUpItem _levelUpItem;
    [SerializeField] private Button _button;

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _image.sprite = _levelUpItem.Sprite;
        _text.text = _levelUpItem.Name;
    }

    public void SelectItem()
    {
        _gameRoot.StartLevel(_levelUpItem);
    }
}
