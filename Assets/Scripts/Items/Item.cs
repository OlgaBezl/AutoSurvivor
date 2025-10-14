using Scripts.Items.ScriptableObjects;
using UnityEngine;

namespace Scripts.Items
{
    public class Item
    {
        public const int MaxLevel = 8;
        public int Level { get; protected set; } = 0;
        public string NameWithNextLevel => $"{Data.Name} - {Level + 1}";
        public Attack.AttackType Type => _attackData.Type;
        public bool IsAttack => _attackData != null;
        public float Attack => _attackData.Attack * (1 + Level / 10f);
        public float Speed => _attackData.Speed;
        public float Radius => _attackData.Radius;
        public float RadiusVariation => _attackData.RadiusVariation;
        public float Height => _attackData.Height;
        public float HeightVariation => _attackData.HeightVariation;
        public float Distance => _attackData.Distance;
        public int SpawnInterval => _attackData.SpawnInterval;
        public int MaxCount => _attackData.MaxCount;
        public float LifeTime => _attackData.LifeTime;
        public Sprite Sprite => _attackData.Sprite;
        public bool CanTurn => _attackData.CanTurn;
        public LevelUpItemData Data => _attackData;

        public int ProjectileCount { get; private set; } = 1;


        private AttackItemData _attackData;

        public Item(AttackItemData data)
        {
            _attackData = data;
        }

        public void LevelUp()
        {
            if(Level + 1 < MaxLevel)
            {
                Level++;

                if (_attackData.AddProjectileWhenLevelingUp)
                {
                    ProjectileCount = Level;
                }
            }
            else
            {
                if (_attackData.IsBaseVersion)
                {

                }
            }
        }
    }
}
