using UnityEngine;

public class CircleProjectile : MonoBehaviour
{
    private Vector3 _center = Vector3.zero;
    private float _currentAngle;
    private BaseAttackItem _attackItem;

    private void FixedUpdate()
    {
        _currentAngle += Time.deltaTime * _attackItem.Speed;
        Vector3 direction = Quaternion.AngleAxis(_currentAngle, Vector3.forward) * Vector3.up;
        transform.position = _center + direction * _attackItem.Radius;
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
        if (item == null)
            throw new System.ArgumentNullException(nameof(item));

        _attackItem = item;
    }
}
