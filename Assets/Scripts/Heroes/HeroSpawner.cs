using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private Hero[] _heroPrefabs;

    private void OnValidate()
    {
        if (_heroPrefabs == null)
            throw new System.ArgumentNullException(nameof(_heroPrefabs));
    }

    public Hero[] GetAll()
    {
        return _heroPrefabs;
    }
}
