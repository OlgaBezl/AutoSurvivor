using UnityEngine;

public class BaseAttackMover : MonoBehaviour
{
    public BaseAttackItem AttackItem { get; private set; }

    public virtual void Initialize(Vector3 direction, BaseAttackItem item)
    {
        if (item == null)
            throw new System.ArgumentNullException(nameof(item));

        AttackItem = item;
    }
}
