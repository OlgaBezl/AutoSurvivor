using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.Environment
{
    public class BackgroundTilmapGenerator : MonoBehaviour
    {
        [Header("Tilemap References")]
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private List<TileBase> _tiles;
        [SerializeField] private TileBase _borderTile;

        [Header("Height Influence")]
        [Range(0.1f, 3f)][SerializeField] private float _heightWeight = 1f;

        [Header("Blob Settings")]
        [Range(0f, 0.2f)][SerializeField] private float _blobChance = 0.05f;
        [SerializeField] private int _minBlobSize = 2;
        [SerializeField] private int _maxBlobSize = 6;

        private Dictionary<Vector2Int, TileBase> _blobTiles;
        private Vector2Int _startPosition;
        private int _width;
        private int _height;
        private int _border;
        
        private void OnValidate()
        {
            if (_tilemap == null)
                throw new System.ArgumentNullException(nameof(_tilemap));

            if (_tiles == null || _tiles?.Count == 0)
                throw new System.ArgumentNullException(nameof(_tiles));

            if (_borderTile == null)
                throw new System.ArgumentNullException(nameof(_borderTile));
        }

        public void GenerateTilemap(int width, int height, int border)
        {
            _width = width;
            _height = height;
            _border = border;

            _startPosition = new Vector2Int(-_width / 2, -_height / 2);
            _blobTiles = new Dictionary<Vector2Int, TileBase>();

            if (_tilemap == null || _tiles.Count == 0)
            {
                return;
            }

            ClearTilemap();
            _blobTiles.Clear();
            GenerateBlobs();
            FillTilemap();
            GenerateBorder();
        }

        private void GenerateBlobs()
        {
            for (int x = _startPosition.x; x < _startPosition.x + _width; x++)
            {
                for (int y = _startPosition.y; y < _startPosition.y + _height; y++)
                {
                    if (Random.value < _blobChance)
                    {
                        CreateBlob(new Vector2Int(x, y));
                    }
                }
            }
        }

        private void CreateBlob(Vector2Int center)
        {
            int blobSize = Random.Range(_minBlobSize, _maxBlobSize + 1);
            int blobTileIndex = GetWeightedTileIndex(center.y);

            // Создаем органическое пятно
            for (int dx = -blobSize; dx <= blobSize; dx++)
            {
                for (int dy = -blobSize; dy <= blobSize; dy++)
                {
                    // Органическая форма с шумом
                    float distance = Mathf.Sqrt(dx * dx + dy * dy);
                    float maxDistance = blobSize * (0.7f + Random.value * 0.3f);

                    if (distance <= maxDistance && Random.value > 0.3f)
                    {
                        Vector2Int pos = new Vector2Int(center.x + dx, center.y + dy);
                        if (IsInBounds(pos))
                        {
                            _blobTiles[pos] = _tiles[blobTileIndex];
                        }
                    }
                }
            }
        }

        private bool IsInBounds(Vector2Int pos)
        {
            return pos.x >= _startPosition.x && pos.x < _startPosition.x + _width &&
                   pos.y >= _startPosition.y && pos.y < _startPosition.y + _height;
        }

        private void FillTilemap()
        {
            for (int x = _startPosition.x; x < _startPosition.x + _width; x++)
            {
                for (int y = _startPosition.y; y < _startPosition.y + _height; y++)
                {
                    TileBase selectedTile = GetTileForPosition(x, y);
                    _tilemap.SetTile(new Vector3Int(x, y, 0), selectedTile);
                }
            }
        }

        private void GenerateBorder()
        {
            for (int x = _startPosition.x - _border; x < _startPosition.x + _width + _border; x++)
            {
                for (int y = _startPosition.y - _border; y < _startPosition.y + _height + _border; y++)
                {
                    if ((x < _startPosition.x || x >= _startPosition.x + _width) ||
                       (y < _startPosition.y || y >= _startPosition.y + _width))
                    {
                        _tilemap.SetTile(new Vector3Int(x, y, 0), _borderTile);
                    }
                    else if ((x < _startPosition.x + _border || x >= _startPosition.x + _width - _border) ||
                       (y < _startPosition.y + _border || y >= _startPosition.y + _width - _border))
                    {
                        int randomIndex = Random.Range(0, 2);
                        if (randomIndex > 0)
                        {
                            _tilemap.SetTile(new Vector3Int(x, y, 0), _borderTile);
                        }
                    }

                }
            }
        }

        private TileBase GetTileForPosition(int x, int y)
        {
            Vector2Int pos = new Vector2Int(x, y);

            if (_blobTiles.ContainsKey(pos))
            {
                return _blobTiles[pos];
            }

            return GetRandomTileWithHeightWeight(y);
        }

        private TileBase GetRandomTileWithHeightWeight(int y)
        {
            if (_tiles.Count == 1)
                return _tiles[0];

            // Базовый случайный индекс
            int randomIndex = Random.Range(0, _tiles.Count);

            // Добавляем влияние высоты
            float normalizedHeight = (float)(y - _startPosition.y) / _height;

            // Вычисляем "предпочтительный" индекс на основе высоты
            int preferredIndex = Mathf.FloorToInt(normalizedHeight * (_tiles.Count - 1));

            // Смещаем случайный индекс в сторону предпочтительного
            float blend = Mathf.Clamp01(_heightWeight * normalizedHeight);
            int finalIndex = BlendTileIndices(randomIndex, preferredIndex, blend);

            return _tiles[Mathf.Clamp(finalIndex, 0, _tiles.Count - 1)];
        }

        private int BlendTileIndices(int randomIndex, int preferredIndex, float blendStrength)
        {
            // Чем выше, тем больше шансов сместиться к концу списка
            if (Random.value < blendStrength)
            {
                // Смещаемся в сторону preferredIndex с некоторой случайностью
                int direction = preferredIndex > randomIndex ? 1 : -1;
                int shift = Random.Range(0, Mathf.Abs(preferredIndex - randomIndex) + 1);
                return randomIndex + (direction * shift);
            }

            return randomIndex;
        }

        private int GetWeightedTileIndex(int y)
        {
            float normalizedHeight = (float)(y - _startPosition.y) / _height;

            // Упрощенная версия для пятен - немного смещаем вероятность
            float randomValue = Random.value;
            float heightInfluence = normalizedHeight * _heightWeight;

            int index = Mathf.FloorToInt((randomValue + heightInfluence) * 0.5f * _tiles.Count);
            return Mathf.Clamp(index, 0, _tiles.Count - 1);
        }

        [ContextMenu("Clear Tilemap")]
        public void ClearTilemap()
        {
            if (_tilemap != null)
            {
                _tilemap.ClearAllTiles();
                _blobTiles.Clear();
                Debug.Log("Tilemap cleared!");
            }
        }
    }
}