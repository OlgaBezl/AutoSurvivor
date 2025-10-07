using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundTilmapGenerator : MonoBehaviour
{
    [Header("Tilemap References")]
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private List<TileBase> _tiles;

    [Header("Generation Settings")]
    [SerializeField] private int _width = 100;
    [SerializeField] private int _height = 100;

    [Header("Height Influence")]
    [Range(0.1f, 3f)] [SerializeField] private float _heightWeight = 1f;

    [Header("Blob Settings")]
    [Range(0f, 0.2f)] [SerializeField] private float _blobChance = 0.05f;
    [SerializeField] private int _minBlobSize = 2;
    [SerializeField] private int _maxBlobSize = 6;

    private Dictionary<Vector2Int, TileBase> _blobTiles;
    private Vector2Int _startPosition;

    private void Start()
    {
        _startPosition = new Vector2Int(-_width/2, -_height/2);
        _blobTiles = new Dictionary<Vector2Int, TileBase>();

        GenerateTilemap();
    }

    [ContextMenu("Generate Tilemap")]
    public void GenerateTilemap()
    {
        if (_tilemap == null || _tiles.Count == 0)
        {
            Debug.LogError("Tilemap or tiles not assigned!");
            return;
        }

        ClearTilemap();
        _blobTiles.Clear();
        GenerateBlobs();
        FillTilemap();
        Debug.Log("Random tilemap generated successfully!");
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
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase selectedTile = GetTileForPosition(x, y);
                _tilemap.SetTile(tilePosition, selectedTile);
            }
        }
    }

    private TileBase GetTileForPosition(int x, int y)
    {
        Vector2Int pos = new Vector2Int(x, y);

        // Если есть пятно в этой позиции, используем его
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

    // Альтернативный вариант с весовой системой
    private TileBase GetWeightedRandomTile(int y)
    {
        if (_tiles.Count == 1)
            return _tiles[0];

        float normalizedHeight = (float)(y - _startPosition.y) / _height;

        // Вычисляем веса для каждого тайла
        float[] weights = new float[_tiles.Count];
        float totalWeight = 0f;

        for (int i = 0; i < _tiles.Count; i++)
        {
            // Базовый вес для всех тайлов
            float baseWeight = 1f;

            // Добавляем вес на основе высоты (чем выше, тем больше вес для поздних тайлов)
            float heightBonus = Mathf.Clamp01(normalizedHeight - (float)i / _tiles.Count) * _heightWeight;

            weights[i] = baseWeight + heightBonus;
            totalWeight += weights[i];
        }

        // Выбираем тайл на основе весов
        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        for (int i = 0; i < _tiles.Count; i++)
        {
            currentWeight += weights[i];
            if (randomValue <= currentWeight)
            {
                return _tiles[i];
            }
        }

        return _tiles[_tiles.Count - 1];
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

    // Быстрая генерация без пятен (чистая палитра)
    [ContextMenu("Generate Without Blobs")]
    public void GenerateWithoutBlobs()
    {
        if (_tilemap == null || _tiles.Count == 0) return;

        ClearTilemap();

        for (int x = _startPosition.x; x < _startPosition.x + _width; x++)
        {
            for (int y = _startPosition.y; y < _startPosition.y + _height; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase selectedTile = GetWeightedRandomTile(y);
                _tilemap.SetTile(tilePosition, selectedTile);
            }
        }

        Debug.Log("Tilemap without blobs generated!");
    }
}