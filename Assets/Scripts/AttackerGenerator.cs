using System.Linq;
using UnityEngine;

public class AttackerGenerator : MonoBehaviour
{
    [SerializeField] private Attaker[] _attackerPrefabs;

    public Attaker GetAttacker(LevelUpItem levelUpItem)
    {
        return _attackerPrefabs.FirstOrDefault(attacker => attacker.LevelUpItem == levelUpItem);
    }
}
