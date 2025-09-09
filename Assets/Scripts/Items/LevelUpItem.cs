using System.Collections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpItem", menuName = "Scriptable Objects/LevelUpItem")]
public class LevelUpItem : ScriptableObject, IEqualityComparer
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public bool IsAttack { get; protected set; } = true;

    public bool IsPassive => !IsAttack;

    private string _uid;

    public override bool Equals(object obj)
    {
        if (obj is LevelUpItem item)
            return _uid == item._uid;
        else
            return false;
    }

    public new bool Equals(object x, object y)
    {
        if (x is LevelUpItem xItem && y is LevelUpItem yItem)
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
