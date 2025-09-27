using Scripts.Healths;
using System;
using UnityEngine;

namespace Scripts
{
    public class BaseCharacter : MonoBehaviour
    {
        [SerializeField] private HealthSprite _healthSprite;

        public Health Health { get; private set; }

        private void OnValidate()
        {
            if (_healthSprite == null)
                throw new ArgumentNullException(nameof(_healthSprite));
        }

        public void Initialize(float maxHealth)
        {
            Health = new Health(maxHealth);
            _healthSprite.Initialize(Health);
            _healthSprite.DeathAnimationFinished += Death;
            gameObject.SetActive(true);
        }

        protected virtual void Death()
        {
            _healthSprite.DeathAnimationFinished -= Death;
            gameObject.SetActive(false);
        }
    }
}
