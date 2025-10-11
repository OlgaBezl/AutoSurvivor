using UnityEngine;

namespace Scripts.Environment
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private int _width = 200;
        [SerializeField] private int _height = 200;
        [SerializeField] private int _border = 12;

        [SerializeField] private BackgroundTilmapGenerator _generator;
        [SerializeField] private DeathBorder _deathBorder;

        private void OnValidate()
        {
            if (_generator == null)
                throw new System.ArgumentNullException(nameof(_generator));

            if (_deathBorder == null)
                throw new System.ArgumentNullException(nameof(_deathBorder));
        }

        private void Start()
        {
            _generator.GenerateTilemap(_width, _height, _border);

            DeathBorder leftBorder = Instantiate(_deathBorder, transform);
            leftBorder.transform.localScale = new Vector3(_border * 2, _height + _border * 2, 0);
            leftBorder.transform.position = new Vector3(-_width / 2, 0, 0);

            DeathBorder righrBorder = Instantiate(_deathBorder, transform);
            righrBorder.transform.localScale = new Vector3(_border * 2, _height + _border * 2, 0);
            righrBorder.transform.position = new Vector3(_width / 2, 0, 0);

            DeathBorder topBorder = Instantiate(_deathBorder, transform);
            topBorder.transform.localScale = new Vector3(_width + _border * 2, _border * 2, 0);
            topBorder.transform.position = new Vector3(0, _height / 2, 0);

            DeathBorder bottomBorder = Instantiate(_deathBorder, transform);
            bottomBorder.transform.localScale = new Vector3(_width + _border * 2, _border * 2, 0);
            bottomBorder.transform.position = new Vector3(0, -_height / 2, 0);
        }
    }
}
