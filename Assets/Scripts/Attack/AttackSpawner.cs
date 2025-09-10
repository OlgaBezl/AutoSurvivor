using System.Linq;
using UnityEngine;

public class AttackSpawner : MonoBehaviour
{
    [SerializeField] private BaseAttacker[] _attackerPrefabs;
    //[SerializeField] private PassiveItem[] _passivePrefabs;

    private void OnValidate()
    {
        if (_attackerPrefabs == null)
            throw new System.ArgumentNullException(nameof(_attackerPrefabs));
    }

    public BaseAttacker[] GetAll()
    {
        return _attackerPrefabs;
    }

    public BaseAttacker GetAttacker(LevelUpItem item)
    {
        return _attackerPrefabs.FirstOrDefault(attacker => attacker.AttackItem.Equals(item));
    }
}
