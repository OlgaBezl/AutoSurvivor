using System;
using UnityEngine;

public class BaseAttackMover : MonoBehaviour
{
    public BaseAttackItem AttackItem { get; private set; }

    public event Action UnActived;

    public virtual void Initialize(Vector3 direction, BaseAttackItem item, int countItems = 0)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        AttackItem = item;
    }

    protected void Unactive()
    {
        UnActived?.Invoke();
    }
}
