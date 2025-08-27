using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Attaker : MonoBehaviour
{
    [field: SerializeField] public LevelUpItem LevelUpItem { get; private set; }
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(LevelUpItem.AttackValue);
        }
    }
}
