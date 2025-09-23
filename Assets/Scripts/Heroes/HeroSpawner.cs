using System.Linq;
using UnityEngine;

namespace Scripts.Heroes
{
    public class HeroSpawner : MonoBehaviour
    {
        [SerializeField] private Hero[] _heroPrefabs;

        private void OnValidate()
        {
            if (_heroPrefabs == null)
                throw new System.ArgumentNullException(nameof(_heroPrefabs));
        }

        public Hero[] GetAll()
        {
            return _heroPrefabs;
        }

        public Hero Spawn(HeroItem item)
        {
            Hero prefab = _heroPrefabs.FirstOrDefault(hero => hero.HeroItem.Equals(item));
            return Instantiate(prefab, transform);
        }
    }
}