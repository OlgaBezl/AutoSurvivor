using Scripts.Items.ScriptableObjects;
using UnityEngine;

namespace Scripts.Items
{
    public class Item
    {
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
        public Sprite Sprite => _attackData.Sprite;

        public LevelUpItemData Data => _attackData;
        private BaseAttackItem _attackData;

        public Item(BaseAttackItem data)
        {
            _attackData = data;
        }

        public void LevelUp()
        {
            Level++;
        }
    }
}
