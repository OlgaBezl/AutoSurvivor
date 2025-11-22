using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Scripts.Items.ScriptableObjects
{
    public class BaseIdItem : ScriptableObject, IEqualityComparer
    {
        private string _uid;

        public new bool Equals(object x, object y)
        {
            if (x is BaseIdItem xItem && y is BaseIdItem yItem)
                return xItem._uid == yItem._uid;
            else
                return false;
        }

        public int GetHashCode(object obj)
        {
            return _uid.GetHashCode();
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_uid))
            {
                _uid = GUID.Generate().ToString();
                //EditorUtility.SetDirty(this);
            }
        }
    }
}
