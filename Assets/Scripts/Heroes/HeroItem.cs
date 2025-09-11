using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroItem", menuName = "Scriptable Objects/HeroItem")]
public class HeroItem : ScriptableObject, IEqualityComparer
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    private string _uid;

    public override bool Equals(object obj)
    {
        if (obj is HeroItem item)
            return _uid == item._uid;
        else
            return false;
    }

    public new bool Equals(object x, object y)
    {
        if (x is HeroItem xItem && y is HeroItem yItem)
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
