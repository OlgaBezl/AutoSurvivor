using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    private HeroItem _heroItem;

    public event Action<HeroItem> HeroSelected;

    private void OnValidate()
    {
        if (_image == null)
            throw new ArgumentNullException(nameof(_image));

        if (_text == null)
            throw new ArgumentNullException(nameof(_text));
    }

    public void Initialize(HeroItem heroItem)
    {
        _heroItem = heroItem;

        _image.sprite = _heroItem.Sprite;
        _text.text = _heroItem.Name;
    }

    public void SelectItem()
    {
        HeroSelected?.Invoke(_heroItem);
    }
}
