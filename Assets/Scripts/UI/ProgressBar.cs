using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Progress _progress;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnValidate()
    {
        if (_progress == null)
            throw new System.ArgumentNullException(nameof(_progress));

        if (_slider == null)
            throw new System.ArgumentNullException(nameof(_slider));

        if (_text == null)
            throw new System.ArgumentNullException(nameof(_text));
    }

    private void Awake()
    {
        _slider.value = 1;
        _progress.ChangeValue += ChangeValue;
        _progress.LevelUpped += ChangeLevel;
    }

    private void ChangeValue(float value)
    {
        _slider.value = value / _progress.LevelUpValue;
    }

    private void ChangeLevel(int level)
    {
        _text.text = $"Lvl {level}";
    }
}
