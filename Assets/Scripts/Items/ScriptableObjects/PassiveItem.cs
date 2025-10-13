using UnityEngine;

namespace Scripts.Items.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PassiveItem", menuName = "Scriptable Objects/PassiveItem")]
    public class PassiveItem : LevelUpItemData
    {
        private void OnEnable()
        {
            IsAttack = false;
        }
    }
}