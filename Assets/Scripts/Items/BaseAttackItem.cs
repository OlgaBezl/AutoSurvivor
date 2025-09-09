using UnityEngine;

[CreateAssetMenu(fileName = "BaseAttackItem", menuName = "Scriptable Objects/BaseAttackItem")]
public class BaseAttackItem : LevelUpItem
{    
    [field: SerializeField] public AttackType Type { get; private set; }
    [field: SerializeField] public float Attack { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Radius { get; private set; }
    [field: SerializeField] public int SpawnInterval { get; private set; }    

}
