using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;

    private Vector3 _direction;
    private LevelUpItem _levelUpItem;

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(_levelUpItem.AttackValue);
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 direction, LevelUpItem levelUpItem)
    {
        _direction = direction.normalized;
        _levelUpItem = levelUpItem;
    }

}
