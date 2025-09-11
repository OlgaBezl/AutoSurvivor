using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyItem", menuName = "Scriptable Objects/EnemyItem")]
public class EnemyItem : ScriptableObject, IEqualityComparer
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Points { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }


    private string _uid;

    public override bool Equals(object obj)
    {
        if (obj is EnemyItem item)
            return _uid == item._uid;
        else
            return false;
    }

    public new bool Equals(object x, object y)
    {
        if (x is EnemyItem xItem && y is EnemyItem yItem)
            return xItem._uid == yItem._uid;
        else
            return false;
    }

    public override int GetHashCode()
    {
        return _uid.GetHashCode();
    }

    public int GetHashCode(object obj)
    {
        return _uid.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(_uid))
        {
            _uid = GUID.Generate().ToString();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
