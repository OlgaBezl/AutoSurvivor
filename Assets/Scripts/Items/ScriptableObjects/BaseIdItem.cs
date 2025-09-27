using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Scripts.Items.ScriptableObjects
{
    public class BaseIdItem : ScriptableObject, IEqualityComparer
    {
        private string _uid;

        public override bool Equals(object obj)
        {
            if (obj is BaseIdItem item)
                return _uid == item._uid;
            else
                return false;
        }

        public new bool Equals(object x, object y)
        {
            if (x is BaseIdItem xItem && y is BaseIdItem yItem)
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
}
