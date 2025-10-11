using Scripts.Heroes;
using UnityEngine;

namespace Scripts.Environment
{
    [RequireComponent(typeof(Collider2D))]
    public class DeathBorder : MonoBehaviour
    {
        [SerializeField] private float _damage = 10;

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out Hero hero))
            {
                hero.Damage(_damage);
            }
        }
    }
}