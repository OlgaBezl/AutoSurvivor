using UnityEngine;

namespace Scripts.Items.ScriptableObjects
{
    public class BaseAttackItem : LevelUpItemData
    {
        [field: SerializeField] public Attack.AttackType Type;//{ get; private set; }
        [field: SerializeField] public float Attack;
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float LifeTime;
        [field: SerializeField] public float Radius;
        [field: SerializeField] public float RadiusVariation;
        [field: SerializeField] public float Height;
        [field: SerializeField] public float HeightVariation;
        [field: SerializeField] public float Distance;
        [field: SerializeField] public int SpawnInterval;
        [field: SerializeField] public int MaxCount;
        [field: SerializeField] public bool CanTurn;

        private void OnEnable()
        {
            IsAttack = true;
        }
    }
}