using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelUpItem", menuName = "Scriptable Objects/LevelUpItem")]
public class LevelUpItem : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float AttackValue { get; private set; }
    
}
