using System.Linq;
using UnityEngine;

public class AttackerGenerator : MonoBehaviour
{
    [SerializeField] private BaseAttacker[] _attackerPrefabs;

    public BaseAttacker GetAttacker(LevelUpItem levelUpItem)
    {
        return _attackerPrefabs.FirstOrDefault(attacker => attacker.LevelUpItem == levelUpItem);
    }
}
