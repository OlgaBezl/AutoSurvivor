using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 _direction;
    private BaseAttackItem _attackItem;

    private void Update()
    {
        transform.position += _direction * _attackItem.Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(_attackItem.Attack);
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 direction, BaseAttackItem item)
    {
        _direction = direction.normalized;
        _attackItem = item;
    }

}
