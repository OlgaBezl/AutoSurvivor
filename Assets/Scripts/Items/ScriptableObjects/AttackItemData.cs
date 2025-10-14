using Scripts.Attack;
using UnityEngine;

namespace Scripts.Items.ScriptableObjects
{
    public class AttackItemData : LevelUpItemData
    {
        [SerializeField] private AttackType _type;
        [SerializeField] private float _attack;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _radius;
        [SerializeField] private float _radiusVariation;
        [SerializeField] private float _height;
        [SerializeField] private float _heightVariation;
        [SerializeField] private float _distance;
        [SerializeField] private int _spawnInterval;
        [SerializeField] private int _maxCount;
        [SerializeField] private bool _canTurn;
        [SerializeField] private bool _addProjectileWhenLevelingUp;
        [SerializeField] private bool _isBaseVersion;
        [SerializeField] private AttackItemData _superVersion;
        [SerializeField] private PassiveItem _tuplePassiveItem;

        public AttackType Type => _type;
        public float Attack => _attack;
        public float Speed => _speed;
        public float LifeTime => _lifeTime;
        public float Radius =>_radius;
        public float RadiusVariation=> _radiusVariation;
        public float Height => _height;
        public float HeightVariation => _heightVariation;
        public float Distance => _distance;
        public int SpawnInterval => _spawnInterval;
        public int MaxCount => _maxCount;
        public bool CanTurn => _canTurn;
        public bool AddProjectileWhenLevelingUp => _addProjectileWhenLevelingUp;
        public bool IsBaseVersion => _isBaseVersion;
        public AttackItemData SuperVersion => _superVersion;
        public PassiveItem PassiveItem => _tuplePassiveItem;

        private void OnEnable()
        {
            IsAttack = true;
        }
    }
}